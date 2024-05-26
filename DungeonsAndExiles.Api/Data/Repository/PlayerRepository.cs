using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Numerics;

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

        public PlayerRepository(IMapper mapper, AppDbContext appDbContext,IItemRepository itemRepository, 
            IBackpackRepository backpackRepository, IEquipmentRepository equipmentRepository, ICombatService combatService, IMonsterRepository monsterRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _itemRepository = itemRepository;
            _backpackRepository = backpackRepository;
            _equipmentRepository = equipmentRepository;
            _combatService = combatService;
            _monsterRepository = monsterRepository;
        }

        public async Task<bool> AddItemToBackpackAsync(Guid playerId, Guid itemId)
        {
            var player = await GetPlayerByIdAsync(playerId);
            var item = await _itemRepository.GetItemById(itemId);
            var backpack = await _backpackRepository.GetBackpackByIdAsync(player.BackpackId);

            backpack.Items.Add(item);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();


            return true;
        }

        public async Task<Player> CreatePlayerAsync(PlayerDto playerDto, Guid userId)
        {
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
            player.BackpackId = backpack.Id;
            player.EquipmentId = equipment.Id;
            player.UserId = userId;

            await _appDbContext.Equipments.AddAsync(equipment);
            await _appDbContext.Backpacks.AddAsync(backpack);
            await _appDbContext.Players.AddAsync(player);
            await _appDbContext.SaveChangesAsync();
            return player;
        }

        public async Task<bool> DeletePlayerAsync(Guid playerId)
        {
            var user = await _appDbContext.Players.FindAsync(playerId);

            if (user == null) return false;

            _appDbContext.Players.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EquipItemAsync(Guid playerId, Guid itemId)
        {
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

            var currentEquippedItem = equipment.Items.FirstOrDefault(i => i.Type == item.Type);
            var currentItemInBackpack = backpack.Items.FirstOrDefault(i => i.Id == itemId);

            if (currentItemInBackpack == null)
            {
                throw new NotFoundException($"Item with ID {itemId} not found in backpack.");
            }


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

            return true;
        }



        public async Task<Player> CombatWithMonsterAsync(Guid playerId, Guid monsterId)
        {
            var player = await GetPlayerByIdAsync(playerId);
            var monstersList = await _monsterRepository.MonstersList();
            var monster = monstersList.FirstOrDefault(m => m.Id == monsterId);
            var itemList = await _itemRepository.GetItemList();
            var playerEq = await _equipmentRepository.GetEquipmentByIdAsync(player.EquipmentId);

            if (monster == null) { throw new NotFoundException($"Monster with ID {monsterId} does not exist"); }
            
            int equipmentStrength = 1;
            if (playerEq.Items != null)
            {
                equipmentStrength = playerEq.Items.Sum(item => (item.Damage + item.Defence));
            }

            int playerStrength = player.Level * equipmentStrength;
            int monsterStrength = (monster.Health + monster.Defence) * monster.Level;

            bool combatResult = _combatService.SimulateCombat(playerStrength, monsterStrength);

            if (combatResult) {
                Item? dropResult = _combatService.SimulateDropAfterCombat(itemList);

                if(player.Experience < 4)
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

            await _appDbContext.SaveChangesAsync();
            return player;
        }

        public async Task<Player> GetPlayerByIdAsync(Guid playerId)
        {
            var player = await _appDbContext.Players.FindAsync(playerId);

            if (player == null)
            {
                throw new NotFoundException($"Player with ID {playerId} not found.");
            }

            return player;
        }

        public async Task<List<Player>> GetPlayerListAsync()
        {
            var playerList = await _appDbContext.Players.ToListAsync();
            
            //can return null
            return playerList;
        }

        public async Task<bool> RemoveItemFromBackpackAsync(Guid playerId, Guid itemId)
        {
            var player = await GetPlayerByIdAsync(playerId);
            var item = await _itemRepository.GetItemById(itemId);
            var backpack = await _appDbContext.Backpacks
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == player.BackpackId);

            if (backpack == null)
            {
                throw new NotFoundException($"Backpack with ID {player.BackpackId} not found.");
            }

            var itemInBackpack = backpack.Items.FirstOrDefault(i => i.Id == itemId);
            if (itemInBackpack == null)
            {
                throw new NotFoundException($"Item with ID {itemId} not found in backpack with ID {backpack.Id}");
            }

            backpack.Items.Remove(itemInBackpack);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<Player> UpdatePlayerAsync(Guid playerId, PlayerUpdateDto playerUpdateDto)
        {
            var newUser = _mapper.Map<Player>(playerUpdateDto);
            newUser.Id = playerId;

            _appDbContext.Players.Update(newUser);
            await _appDbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<List<Item>> GetPlayerEquipmentItemsListAsync(Guid playerId)
        {
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

            if (equipmentItems == null)
            {
                throw new NotFoundException($"Equipment for player with ID {playerId} not found.");
            }

            return equipmentItems;
        }



        public async Task<List<Item>> GetPlayerBackpackItemsListAsync(Guid playerId)
        {
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

            if (backpackItems == null)
            {
                throw new NotFoundException($"Backpack for player with ID {playerId} not found.");
            }

            return backpackItems;
        }


    }
}
