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
            _logger.LogInformation($"Attempting to find item in backpack");

            var backpack = await GetBackpackByIdAsync(backpackId);
            var item = backpack.Items.Find(item => item.Id == itemId);
            if (item == null)
            {
                string message = $"Item with ID {itemId} not found in backpack with ID {backpackId}";
                throw new NotFoundException(message);
            }

            _logger.LogInformation($"Item successfully found in backpack");
            return true;
        }

        public async Task<Backpack> GetBackpackByIdAsync(Guid backpackId)
        {
            _logger.LogInformation($"Attempting to find backpack by ID");
            var backpack = await _appDbContext.Backpacks.FindAsync(backpackId);
            if (backpack == null)
            {
                string message = $"Backpack with ID {backpackId} not found";
                throw new NotFoundException(message);
            }
            _logger.LogInformation($"Backpack successfully found by ID");
            return backpack;
        }
    }
}
