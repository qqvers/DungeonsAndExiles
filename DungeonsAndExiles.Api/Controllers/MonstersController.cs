using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonstersController : ControllerBase
    {

        private readonly IMonsterRepository _monsterRepository;
        public MonstersController(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonstersList()
        {
            var monstersList = await _monsterRepository.MonstersList();
            return Ok(monstersList);
        }
    }
}
