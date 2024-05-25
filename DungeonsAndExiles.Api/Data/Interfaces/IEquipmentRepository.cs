using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<Equipment> GetEquipmentByIdAsync(Guid equipmentId);
    }
}
