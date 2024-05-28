using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SignedInOnly")]
    public class PlayersController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }


        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid playerId)
        {
            try
            {
                var player = await _playerRepository.GetPlayerByIdAsync(playerId);
                var playerVM = _mapper.Map<PlayerVM>(player);
                return Ok(playerVM);
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
                var playerVMList = _mapper.Map<List<PlayerVM>>(playersList);
                return Ok(playerVMList);
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
                var result = await _playerRepository.CombatWithMonsterAsync(playerId, monsterId);
                var message = result ? "You won" : "You lost";
                return Ok(message);
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
                var itemList = await _playerRepository.GetPlayerBackpackItemsListAsync(playerId);
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
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
                var itemList = await _playerRepository.GetPlayerEquipmentItemsListAsync(playerId);
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
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
