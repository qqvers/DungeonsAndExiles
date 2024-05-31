using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetItemById(Guid itemId);
        Task<List<Item>> GetItemList(); 
        Task<bool> AddItemToBackpack(Guid backpackId, Guid itemId);
        Task<bool> RemoveItemFromBackpack(Guid backpackId, Guid itemId);
        Task<bool> AddItemToEquipment(Guid equipmentId, Guid itemId);
        Task<bool> RemoveItemFromEquipment(Guid equipmentId, Guid itemId);
    }
}
