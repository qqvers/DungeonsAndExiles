using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly IBackpackRepository _backpackRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(
            AppDbContext appDbContext,
            IBackpackRepository backpackRepository,
            IEquipmentRepository equipmentRepository,
            ILogger<ItemRepository> logger)
        {
            _appDbContext = appDbContext;
            _backpackRepository = backpackRepository;
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        public async Task<Item?> GetItemById(Guid itemId)
        {
            _logger.LogInformation($"Attempting to find item by ID");
            var item = await _appDbContext.Items.FindAsync(itemId);
            if (item == null)
            {
                var message = $"Item with ID {itemId} not found";
                throw new NotFoundException(message);
            }
            _logger.LogInformation($"Item successfully found by ID");
            return item;
        }

        public async Task<List<Item>> GetItemList()
        {
            _logger.LogInformation($"Attempting to find item list");
            var itemList = await _appDbContext.Items.ToListAsync();
            if(itemList == null)
            {
                var message = $"Item list not found";
                throw new NotFoundException(message);
            }
            _logger.LogInformation($"Item list successfully found");
            return itemList;
        }

        public async Task<bool> AddItemToBackpack(Guid backpackId, Guid itemId)
        {
            _logger.LogInformation($"Attempting to add item to backpack");
            var backpack = await _backpackRepository.GetBackpackByIdAsync(backpackId);
            var item = await GetItemById(itemId);
            backpack.Items.Add(item!);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation($"Item successfully added to backpack");
            return true;
        }

        public async Task<bool> AddItemToEquipment(Guid equipmentId, Guid itemId)
        {
            _logger.LogInformation($"Attempting to add item to equipment");
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId);
            var item = await GetItemById(itemId);
            equipment.Items.Add(item!);

            _appDbContext.Equipments.Update(equipment);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation($"Item successfully added to equipment");
            return true;
        }

        public async Task<bool> RemoveItemFromBackpack(Guid backpackId, Guid itemId)
        {
            _logger.LogInformation($"Attempting to remove item from backpack");
            var backpack = await _backpackRepository.GetBackpackByIdAsync(backpackId);
            var item = await GetItemById(itemId);
            backpack.Items.Remove(item!);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation($"Item successfully removed from backpack");
            return true;
        }

        public async Task<bool> RemoveItemFromEquipment(Guid equipmentId, Guid itemId)
        {
            _logger.LogInformation($"Attempting to remove item from equipment");
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId);
            var item = await GetItemById(itemId);
            equipment.Items.Remove(item!);

            _appDbContext.Equipments.Update(equipment);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation($"Item successfully removed from equipment");
            return true;
        }
    }
}
