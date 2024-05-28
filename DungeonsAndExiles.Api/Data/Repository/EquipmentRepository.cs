using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<EquipmentRepository> _logger;

        public EquipmentRepository(AppDbContext appDbContext, ILogger<EquipmentRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Equipment> GetEquipmentByIdAsync(Guid equipmentId)
        {
            var equipment = await _appDbContext.Equipments.FindAsync(equipmentId);
            if (equipment == null)
            {
                _logger.LogWarning($"Equipment with ID {equipmentId} not found");
                throw new NotFoundException($"Equipment with ID {equipmentId} not found");
            }
            return equipment;
        }
    }
}
