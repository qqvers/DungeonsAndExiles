using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class BackpackRepository : IBackpackRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<BackpackRepository> _logger;

        public BackpackRepository(AppDbContext appDbContext, ILogger<BackpackRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<bool> FindItemInBackpackByIdAsync(Guid backpackId, Guid itemId)
        {
            var backpack = await GetBackpackByIdAsync(backpackId);
            var item = backpack.Items.Find(item => item.Id == itemId);
            if (item == null)
            {
                _logger.LogWarning($"Item with ID {itemId} not found in backpack with ID {backpackId}");
                throw new NotFoundException($"Item with ID {itemId} not found in backpack with ID {backpackId}");
            }
            return true;
        }

        public async Task<Backpack> GetBackpackByIdAsync(Guid backpackId)
        {
            var backpack = await _appDbContext.Backpacks.FindAsync(backpackId);
            if (backpack == null)
            {
                _logger.LogWarning($"Backpack with ID {backpackId} not found");
                throw new NotFoundException($"Backpack with ID {backpackId} not found");
            }
            return backpack;
        }
    }
}
