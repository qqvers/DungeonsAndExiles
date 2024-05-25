using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class BackpackRepository : IBackpackRepository
    {
        private readonly AppDbContext _appDbContext;
        public BackpackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> FindItemInBackpackByIdAsync(Guid backpackId, Guid itemId)
        {
            var backpack = await GetBackpackByIdAsync(backpackId);
            var item = backpack.Items.Find(item => item.Id == itemId);
            if (backpack == null) { throw new NotFoundException($"Item with ID {itemId} not found in backpack with ID {backpackId}"); }
            return true;
        }

        public async Task<Backpack> GetBackpackByIdAsync(Guid backpackId)
        {
            var backpack = await _appDbContext.Backpacks.FindAsync(backpackId);
            if(backpack == null) { throw new NotFoundException($"Backpack with ID {backpackId} not found"); }
            return backpack;
        }
    }
}
