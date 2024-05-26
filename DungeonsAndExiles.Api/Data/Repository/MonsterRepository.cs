using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly AppDbContext _appDbContext;
        public MonsterRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Monster>> MonstersList()
        {
            var monsterList = await _appDbContext.Monsters.ToListAsync();

            return monsterList;
        }
    }
}
