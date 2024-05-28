using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<MonsterRepository> _logger;
        public MonsterRepository(AppDbContext appDbContext, ILogger<MonsterRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<List<Monster>> MonstersList()
        {
            _logger.LogInformation("Attempting to retrieve monster list");
            var monsterList = await _appDbContext.Monsters.ToListAsync();
            if(monsterList.Count == 0) { throw new NotFoundException("No monster found"); }

            return monsterList;
        }
    }
}
