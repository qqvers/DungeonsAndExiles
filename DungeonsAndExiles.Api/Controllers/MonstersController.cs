using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SignedInOnly")]
    public class MonstersController : ControllerBase
    {
        private readonly IMonsterRepository _monsterRepository;
        private readonly ILogger<MonstersController> _logger;

        public MonstersController(IMonsterRepository monsterRepository, ILogger<MonstersController> logger)
        {
            _monsterRepository = monsterRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GetMonstersList()
        {
            _logger.LogInformation("Attempting to get monsters list");
            try
            {
                var monstersList = await _monsterRepository.MonstersList();
                _logger.LogInformation("Monsters list retrieved successfully");
                return Ok(monstersList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting monsters list");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
