using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetPlayer([FromRoute] Guid playerId) {
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);

            return Ok(player);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersList()
        {
            var playersList = await _playerRepository.GetPlayerListAsync();
            if (playersList == null) { return NotFound("No players created"); }
            return Ok(playersList);
        }

        //currently not needed
        /*        [HttpPost("{playerId}")]
                public async Task<IActionResult> UpdatePlayer([FromRoute] Guid playerId,[FromBody] PlayerUpdateDto playerUpdateDto)
                {
                    var player = await _playerRepository.UpdatePlayerAsync(playerId,playerUpdateDto);
                    return Created("", player);
                }*/


        [HttpDelete("{playerId}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid playerId)
        {
            await _playerRepository.DeletePlayerAsync(playerId);

            return NoContent();
        }

        [HttpDelete("{playerId}/backpacks/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            await _playerRepository.RemoveItemFromBackpackAsync(playerId, itemId);

            return NoContent();
        }

        [HttpPost("{playerId}/items/{itemId}")]
        public async Task<IActionResult> EquipItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            await _playerRepository.EquipItemAsync(playerId, itemId);

            return NoContent();
        }

        [HttpPost("{playerId}/monsters/{monsterId}")]
        public async Task<IActionResult> PlayerCombatWithMonster([FromRoute] Guid playerId, [FromRoute] Guid monsterId)
        {
            var player = await _playerRepository.CombatWithMonsterAsync(playerId, monsterId);

            return Ok(player);
        }

        [HttpGet("{playerId}/backpacks/items")]
        public async Task<IActionResult> GetPlayerBackpackItems([FromRoute] Guid playerId)
        {
            var items = await _playerRepository.GetPlayerBackpackItemsListAsync(playerId);
            return Ok(items);
        }
        [HttpGet("{playerId}/equipments/items")]
        public async Task<IActionResult> GetPlayerEquipmentItems([FromRoute] Guid playerId)
        {
            var items = await _playerRepository.GetPlayerEquipmentItemsListAsync(playerId);
            return Ok(items);
        }
    }
}
