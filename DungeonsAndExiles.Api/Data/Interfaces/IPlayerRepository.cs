using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> CreatePlayerAsync(PlayerDto playerDto, Guid userId);
        Task<Player> GetPlayerByIdAsync(Guid playerId);
        Task<List<Player>> GetPlayerListAsync();
        Task<Player> UpdatePlayerAsync(Guid playerId, PlayerUpdateDto playerUpdateDto);
        Task<bool> DeletePlayerAsync(Guid playerId);
        Task<bool> AddItemToBackpackAsync(Guid playerId, Guid itemId);
        Task<bool> RemoveItemFromBackpackAsync(Guid playerId, Guid itemId);
        Task<bool> EquipItemAsync(Guid playerId, Guid itemId);
        Task<Player> CombatWithMonsterAsync(Guid playerId, Guid monsterId);
        Task<List<Item>> GetPlayerEquipmentItemsListAsync(Guid playerId);
        Task<List<Item>> GetPlayerBackpackItemsListAsync(Guid playerId);

    }
}
