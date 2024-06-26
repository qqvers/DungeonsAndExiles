﻿using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Services;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IItemRepository _itemRepository;
        private readonly IBackpackRepository _backpackRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMonsterRepository _monsterRepository;
        private readonly ICombatService _combatService;
        private readonly ILogger<PlayerRepository> _logger;

        public PlayerRepository(
            IMapper mapper,
            AppDbContext appDbContext,
            IItemRepository itemRepository,
            IBackpackRepository backpackRepository,
            IEquipmentRepository equipmentRepository,
            ICombatService combatService,
            IMonsterRepository monsterRepository,
            ILogger<PlayerRepository> logger)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _itemRepository = itemRepository;
            _backpackRepository = backpackRepository;
            _equipmentRepository = equipmentRepository;
            _combatService = combatService;
            _monsterRepository = monsterRepository;
            _logger = logger;
        }

        public async Task AddItemToBackpackAsync(Guid playerId, Guid itemId)
        {
            _logger.LogInformation("Attempting to add item with ID {ItemId} to backpack for player with ID {PlayerId}", itemId, playerId);

            var player = await GetPlayerByIdAsync(playerId);
            var item = await _itemRepository.GetItemById(itemId);
            var backpack = await _backpackRepository.GetBackpackByIdAsync(player.BackpackId);

            backpack.Items.Add(item!);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Successfully added item with ID {ItemId} to backpack for player with ID {PlayerId}", itemId, playerId);
        }

        public async Task<Player> CreatePlayerAsync(PlayerDto playerDto, Guid userId)
        {
            _logger.LogInformation("Attempting to create a new player for user with ID {UserId}", userId);

            var playerInDatabase = _appDbContext.Players.Any(p => p.Name == playerDto.Name);
            if (playerInDatabase)
            {
                throw new ArgumentException("Player with that name already exists");
            }


            var player = _mapper.Map<Player>(playerDto);
            player.Id = Guid.NewGuid();

            var backpack = new Backpack
            {
                Id = Guid.NewGuid(),
                Capacity = 20,
                PlayerId = player.Id
            };

            var equipment = new Equipment
            {
                Id = Guid.NewGuid(),
                PlayerId = player.Id
            };

            player.Level = 1;
            player.Health = 100;
            player.Damage = 1;
            player.Defence = 0;
            player.Experience = 0;
            player.Stamina = 20;
            player.BackpackId = backpack.Id;
            player.EquipmentId = equipment.Id;
            player.UserId = userId;

            await _appDbContext.Equipments.AddAsync(equipment);
            await _appDbContext.Backpacks.AddAsync(backpack);
            await _appDbContext.Players.AddAsync(player);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Player created successfully with ID {PlayerId}", player.Id);
            return player;
        }

        public async Task DeletePlayerAsync(Guid playerId)
        {
            _logger.LogInformation("Attempting to delete player with ID {PlayerId}", playerId);

            var user = await GetPlayerByIdAsync(playerId);

            _appDbContext.Players.Remove(user);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Player with ID {PlayerId} deleted successfully", playerId);
        }

        public async Task EquipItemAsync(Guid playerId, Guid itemId)
        {
            _logger.LogInformation("Attempting to equip item with ID {ItemId} for player with ID {PlayerId}", itemId, playerId);

            var player = await GetPlayerByIdAsync(playerId);
            var item = await _itemRepository.GetItemById(itemId);
            var backpack = await _appDbContext.Backpacks
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == player.BackpackId);

            var equipment = await _appDbContext.Equipments
                .Include(e => e.Items)
                .FirstOrDefaultAsync(e => e.Id == player.EquipmentId);

            if (backpack == null || equipment == null)
            {
                throw new NotFoundException("Backpack or equipment not found.");
            }

            var currentEquippedItem = equipment.Items.FirstOrDefault(i => i.Type == item!.Type);
            var currentItemInBackpack = backpack.Items.FirstOrDefault(i => i.Id == itemId) ?? throw new NotFoundException($"Item with ID {itemId} not found in backpack.");
            if (currentEquippedItem == null)
            {
                backpack.Items.Remove(currentItemInBackpack);
                equipment.Items.Add(currentItemInBackpack);
            }
            else
            {
                backpack.Items.Remove(currentItemInBackpack);
                backpack.Items.Add(currentEquippedItem);
                equipment.Items.Remove(currentEquippedItem);
                equipment.Items.Add(currentItemInBackpack);
            }

            _appDbContext.Backpacks.Update(backpack);
            _appDbContext.Equipments.Update(equipment);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Item with ID {ItemId} equipped successfully for player with ID {PlayerId}", itemId, playerId);
        }

        public async Task<bool> CombatWithMonsterAsync(Guid playerId, Guid monsterId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to start combat between player with ID {PlayerId} and monster with ID {MonsterId}", playerId, monsterId);

            var player = await GetPlayerByIdAsync(playerId);
            var playerBackpack = await _backpackRepository.GetBackpackByIdAsync(player.BackpackId);

            if(player.Stamina == 0)
            {
                throw new PlayerCombatValidationException($"Player can not start a combat with 0 stamina.");
            }

            if (playerBackpack.Capacity == playerBackpack.Items.Count)
            {
                throw new PlayerCombatValidationException("Player can not start a combat with no space in backpack");
            }

            //operation gonna take 10s to find a monster, also simulates long request
            await Task.Delay(10000, cancellationToken);

            var monstersList = await _monsterRepository.MonstersList();
            var monster = monstersList.FirstOrDefault(m => m.Id == monsterId);
            var itemList = await _itemRepository.GetItemList();
            var playerEq = await _equipmentRepository.GetEquipmentByIdAsync(player.EquipmentId);

            if (monster == null)
            {
                throw new NotFoundException($"Monster with ID {monsterId} does not exist");
            }

            int equipmentStrength = 1;
            if (playerEq.Items != null)
            {
                equipmentStrength = playerEq.Items.Sum(item => (item.Damage + item.Defence));
            }

            int playerStrength = player.Level * equipmentStrength;
            int monsterStrength = (monster.Health + monster.Defence) * monster.Level;

            bool combatResult = _combatService.SimulateCombat(playerStrength, monsterStrength);

            if (combatResult)
            {
                player.Stamina -= 1;
                Item? dropResult = _combatService.SimulateDropAfterCombat(itemList);

                if (player.Experience < 4)
                {
                    player.Experience += 1;
                }
                else
                {
                    //level up
                    player.Experience = 0;
                    player.Level += 1;
                    player.Health += 5;
                }

                _appDbContext.Players.Update(player);

                if (dropResult != null)
                {
                    await _itemRepository.AddItemToBackpack(player.BackpackId, dropResult.Id);
                }
            }
            else
            {
                return false;
            }

            await _appDbContext.SaveChangesAsync(CancellationToken.None);
            _logger.LogInformation("Combat between player with ID {PlayerId} and monster with ID {MonsterId} completed. Result: {Result}", playerId, monsterId, combatResult);
            return true;
        }

        public async Task<Player> GetPlayerByIdAsync(Guid playerId)
        {
            _logger.LogInformation("Attempting to find player with ID {PlayerId}", playerId);

            var player = await _appDbContext.Players.FindAsync(playerId) ?? throw new NotFoundException($"Player with ID {playerId} not found.");
            _logger.LogInformation("Player with ID {PlayerId} successfully found.", playerId);
            return player;
        }

        public async Task<List<Player>?> GetPlayerListAsync()
        {
            _logger.LogInformation("Attempting to retrieve list of all players");

            var playerList = await _appDbContext.Players.ToListAsync();

            _logger.LogInformation("List of all players successfully retrieved.");
            return playerList;
        }

        public async Task RemoveItemFromBackpackAsync(Guid playerId, Guid itemId)
        {
            _logger.LogInformation("Attempting to remove item with ID {ItemId} from backpack for player with ID {PlayerId}", itemId, playerId);

            var player = await GetPlayerByIdAsync(playerId);
            var item = await _itemRepository.GetItemById(itemId);
            var backpack = await _appDbContext.Backpacks
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == player.BackpackId) ?? throw new NotFoundException($"Backpack with ID {player.BackpackId} not found.");
            var itemInBackpack = backpack.Items.FirstOrDefault(i => i.Id == itemId) ?? throw new NotFoundException($"Item with ID {itemId} not found in backpack with ID {backpack.Id}");
            backpack.Items.Remove(itemInBackpack);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Item with ID {ItemId} removed successfully from backpack for player with ID {PlayerId}", itemId, playerId);
        }

        public async Task<Player> UpdatePlayerAsync(Guid playerId, PlayerUpdateDto playerUpdateDto)
        {
            _logger.LogInformation("Attempting to update player with ID {PlayerId}", playerId);

            var newUser = _mapper.Map<Player>(playerUpdateDto);
            newUser.Id = playerId;

            _appDbContext.Players.Update(newUser);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Player with ID {PlayerId} updated successfully", playerId);
            return newUser;
        }

        public async Task<List<Item>?> GetPlayerEquipmentItemsListAsync(Guid playerId)
        {
            _logger.LogInformation("Attempting to retrieve equipment items list for player with ID {PlayerId}", playerId);

            var equipmentItems = await _appDbContext.Equipments
                .Where(e => e.PlayerId == playerId)
                .SelectMany(e => e.Items)
                .Select(i => new Item
                {
                    Id = i.Id,
                    Name = i.Name,
                    Type = i.Type,
                    Damage = i.Damage,
                    Defence = i.Defence
                })
                .ToListAsync();

            return equipmentItems;
        }

        public async Task<List<Item>?> GetPlayerBackpackItemsListAsync(Guid playerId)
        {
            _logger.LogInformation("Attempting to retrieve backpack items list for player with ID {PlayerId}", playerId);

            var backpackItems = await _appDbContext.Backpacks
                .Where(e => e.PlayerId == playerId)
                .SelectMany(e => e.Items)
                .Select(i => new Item
                {
                    Id = i.Id,
                    Name = i.Name,
                    Type = i.Type,
                    Damage = i.Damage,
                    Defence = i.Defence
                })
                .ToListAsync();
            return backpackItems;
        }

        public async Task<List<Player>> GetPlayersByUserIdAsync(Guid userId)
        {
            return await _appDbContext.Players.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
