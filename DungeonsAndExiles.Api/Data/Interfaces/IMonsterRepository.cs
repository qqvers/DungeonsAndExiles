using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IMonsterRepository
    {
        Task<List<Monster>> MonstersList();
    }
}
