using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> CreatePlayerAsync(PlayerDto playerDto, Guid userId);
        Task<Player> GetPlayerByIdAsync(Guid playerId);
        Task<List<Player>?> GetPlayerListAsync();
        Task<Player> UpdatePlayerAsync(Guid playerId, PlayerUpdateDto playerUpdateDto);
        Task DeletePlayerAsync(Guid playerId);
        Task AddItemToBackpackAsync(Guid playerId, Guid itemId);
        Task RemoveItemFromBackpackAsync(Guid playerId, Guid itemId);
        Task EquipItemAsync(Guid playerId, Guid itemId);
        Task<bool> CombatWithMonsterAsync(Guid playerId, Guid monsterId, CancellationToken cancellationToken);
        Task<List<Item>?> GetPlayerEquipmentItemsListAsync(Guid playerId);
        Task<List<Item>?> GetPlayerBackpackItemsListAsync(Guid playerId);
        Task<List<Player>> GetPlayersByUserIdAsync(Guid userId);
    }
}
