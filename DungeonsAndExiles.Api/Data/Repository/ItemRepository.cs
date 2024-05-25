using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly IBackpackRepository _backpackRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly AppDbContext _appDbContext;

        public ItemRepository(IBackpackRepository backpackRepository, IEquipmentRepository equipmentRepository)
        {
            _backpackRepository = backpackRepository;
            _equipmentRepository = equipmentRepository;
        }
        public async Task<Item> GetItemById(Guid itemId)
        {
            var item = await _appDbContext.Items.FindAsync(itemId);
            if(item == null) { throw new NotFoundException($"Item with ID {itemId} not found"); }

            return item;
        }

        public async Task<bool> AddItemToBackpack(Guid backpackId, Guid itemId)
        {
            var backpack = await _backpackRepository.GetBackpackByIdAsync(backpackId);
            var item = await GetItemById(itemId);
            backpack.Items.Add(item);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddItemToEquipment(Guid equipmentId, Guid itemId)
        {
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId);
            var item = await GetItemById(itemId);
            equipment.Items.Add(item);

            _appDbContext.Equipments.Update(equipment);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveItemFromBackpack(Guid backpackId, Guid itemId)
        {
            var backpack = await _backpackRepository.GetBackpackByIdAsync(backpackId);
            var item = await GetItemById(itemId);
            backpack.Items.Remove(item);

            _appDbContext.Backpacks.Update(backpack);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveItemFromEquipment(Guid equipmentId, Guid itemId)
        {
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId);
            var item = await GetItemById(itemId);
            equipment.Items.Remove(item);

            _appDbContext.Equipments.Update(equipment);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
