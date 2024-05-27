using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid playerId)
        {
            try
            {
                var player = await _playerRepository.GetPlayerByIdAsync(playerId);
                return Ok(player);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //currently not needed
        /*[HttpPost("{playerId}")]
                public async Task<IActionResult> UpdatePlayer([FromRoute] Guid playerId,[FromBody] PlayerUpdateDto playerUpdateDto)
                {
                    var player = await _playerRepository.UpdatePlayerAsync(playerId,playerUpdateDto);
                    return Created("", player);
                }*/

        [HttpGet]
        public async Task<IActionResult> GetPlayersList()
        {
            try
            {
                var playersList = await _playerRepository.GetPlayerListAsync();
                if (playersList == null) { return NotFound("No players created"); }
                return Ok(playersList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{playerId}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid playerId)
        {
            try
            {
                await _playerRepository.DeletePlayerAsync(playerId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{playerId}/backpacks/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            try
            {
                await _playerRepository.RemoveItemFromBackpackAsync(playerId, itemId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{playerId}/items/{itemId}")]
        public async Task<IActionResult> EquipItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            try
            {
                await _playerRepository.EquipItemAsync(playerId, itemId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{playerId}/monsters/{monsterId}")]
        public async Task<IActionResult> PlayerCombatWithMonster([FromRoute] Guid playerId, [FromRoute] Guid monsterId)
        {
            try
            {
                var player = await _playerRepository.CombatWithMonsterAsync(playerId, monsterId);
                return Ok(player);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{playerId}/backpacks/items")]
        public async Task<IActionResult> GetPlayerBackpackItems([FromRoute] Guid playerId)
        {
            try
            {
                var items = await _playerRepository.GetPlayerBackpackItemsListAsync(playerId);
                return Ok(items);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{playerId}/equipments/items")]
        public async Task<IActionResult> GetPlayerEquipmentItems([FromRoute] Guid playerId)
        {
            try
            {
                var items = await _playerRepository.GetPlayerEquipmentItemsListAsync(playerId);
                return Ok(items);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
