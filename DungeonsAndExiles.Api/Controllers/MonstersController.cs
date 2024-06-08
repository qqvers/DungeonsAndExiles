using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    /// <summary>
    /// Handle operations related to monsters.
    /// </summary>
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


        /// <summary>
        /// Retrieves the list of all monsters.
        /// </summary>
        /// <returns>A list of monsters</returns>
        /// <response code="200">Returns the list of monsters</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        /// <response code="204">If the no requested data was found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetMonstersList()
        {
            _logger.LogInformation("Attempting to get monsters list");
            try
            {
                var monstersList = await _monsterRepository.MonstersList();
                _logger.LogInformation("Monsters list retrieved successfully");
                return Ok(monstersList);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting monsters list");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
