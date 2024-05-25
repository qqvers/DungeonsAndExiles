using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
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

        public PlayerRepository(IMapper mapper, AppDbContext appDbContext,IItemRepository itemRepository, IBackpackRepository backpackRepository, IEquipmentRepository equipmentRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _itemRepository = itemRepository;
            _backpackRepository = backpackRepository;
            _equipmentRepository = equipmentRepository;
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
            Guid backpackId = player.BackpackId;
            var backpack = await _backpackRepository.GetBackpackByIdAsync(backpackId);
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(backpackId);

            var currentEquippedItem = equipment.Items.FirstOrDefault(i => i.Type == item.Type);
            var currentItemInBackpack = await _backpackRepository.FindItemInBackpackByIdAsync(backpackId, itemId);

            //equipment logic
            if (currentEquippedItem == null)
            {
                backpack.Items.Remove(item);
                equipment.Items.Add(item);
            }
            else
            {
                backpack.Items.Remove(item);
                backpack.Items.Add(currentEquippedItem);
                equipment.Items.Remove(currentEquippedItem);
                equipment.Items.Add(item);
            }

            _appDbContext.Backpacks.Update(backpack);
            _appDbContext.Equipments.Update(equipment);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public Task<Player> FightWithMonsterAsync(Guid playerId, Guid monsterId)
        {
            throw new NotImplementedException();
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

        public async Task<bool> RemoveItemFromBackpackAsync(Guid playerId, Guid itemId)
        {
            var player = await GetPlayerByIdAsync(playerId);
            var item = await _itemRepository.GetItemById(itemId);
            var backpack = await _backpackRepository.GetBackpackByIdAsync(player.BackpackId);

            //check if item exist
            await _backpackRepository.FindItemInBackpackByIdAsync(player.BackpackId, itemId);

            backpack.Items.Remove(item);

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
    }
}
