using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public EquipmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Equipment> GetEquipmentByIdAsync(Guid equipmentId)
        {
            var equipment = await _appDbContext.Equipments.FindAsync(equipmentId);
            if (equipment == null) { throw new NotFoundException($"Equipment with ID {equipmentId} not found"); }
            return equipment;
        }
    }
}
