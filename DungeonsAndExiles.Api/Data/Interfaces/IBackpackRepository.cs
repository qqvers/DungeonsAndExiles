using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IBackpackRepository
    {
        Task<Backpack> GetBackpackByIdAsync(Guid backpackId);
        Task FindItemInBackpackByIdAsync(Guid backpackId, Guid itemId);
    }
}
