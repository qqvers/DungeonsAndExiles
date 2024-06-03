using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> GetItemById(Guid itemId);
        Task<List<Item>> GetItemList(); 
        Task AddItemToBackpack(Guid backpackId, Guid itemId);
        Task RemoveItemFromBackpack(Guid backpackId, Guid itemId);
        Task AddItemToEquipment(Guid equipmentId, Guid itemId);
        Task RemoveItemFromEquipment(Guid equipmentId, Guid itemId);
    }
}
