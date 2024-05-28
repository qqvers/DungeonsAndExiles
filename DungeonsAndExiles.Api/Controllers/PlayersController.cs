using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(IPlayerRepository playerRepository, IMapper mapper, ILogger<PlayersController> logger)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to get player with ID {PlayerId}", playerId);

            try
            {
                var player = await _playerRepository.GetPlayerByIdAsync(playerId);
                var playerVM = _mapper.Map<PlayerVM>(player);
                return Ok(playerVM);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Player with ID {PlayerId} not found", playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersList()
        {
            _logger.LogInformation("Attempting to get players list");

            try
            {
                var playersList = await _playerRepository.GetPlayerListAsync();
                if (playersList == null)
                {
                    _logger.LogWarning("No players found");
                    return NotFound("No players created");
                }
                var playerVMList = _mapper.Map<List<PlayerVM>>(playersList);
                return Ok(playerVMList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting players list");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{playerId}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to delete player with ID {PlayerId}", playerId);

            try
            {
                await _playerRepository.DeletePlayerAsync(playerId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Player with ID {PlayerId} not found", playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while deleting player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{playerId}/backpacks/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            _logger.LogInformation("Attempting to delete item with ID {ItemId} from backpack for player with ID {PlayerId}", itemId, playerId);

            try
            {
                await _playerRepository.RemoveItemFromBackpackAsync(playerId, itemId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Item with ID {ItemId} not found in backpack for player with ID {PlayerId}", itemId, playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while deleting item with ID {ItemId} from backpack for player with ID {PlayerId}", itemId, playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{playerId}/items/{itemId}")]
        public async Task<IActionResult> EquipItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            _logger.LogInformation("Attempting to equip item with ID {ItemId} for player with ID {PlayerId}", itemId, playerId);

            try
            {
                await _playerRepository.EquipItemAsync(playerId, itemId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Item with ID {ItemId} not found for player with ID {PlayerId}", itemId, playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while equipping item with ID {ItemId} for player with ID {PlayerId}", itemId, playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{playerId}/monsters/{monsterId}")]
        public async Task<IActionResult> PlayerCombatWithMonster([FromRoute] Guid playerId, [FromRoute] Guid monsterId)
        {
            _logger.LogInformation("Attempting to start combat between player with ID {PlayerId} and monster with ID {MonsterId}", playerId, monsterId);

            try
            {
                var result = await _playerRepository.CombatWithMonsterAsync(playerId, monsterId);
                var message = result ? "You won" : "You lost";
                return Ok(message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Monster with ID {MonsterId} not found for player with ID {PlayerId}", monsterId, playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while starting combat between player with ID {PlayerId} and monster with ID {MonsterId}", playerId, monsterId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{playerId}/backpacks/items")]
        public async Task<IActionResult> GetPlayerBackpackItems([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to get backpack items for player with ID {PlayerId}", playerId);

            try
            {
                var itemList = await _playerRepository.GetPlayerBackpackItemsListAsync(playerId);
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Backpack items not found for player with ID {PlayerId}", playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting backpack items for player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{playerId}/equipments/items")]
        public async Task<IActionResult> GetPlayerEquipmentItems([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to get equipment items for player with ID {PlayerId}", playerId);

            try
            {
                var itemList = await _playerRepository.GetPlayerEquipmentItemsListAsync(playerId);
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Equipment items not found for player with ID {PlayerId}", playerId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting equipment items for player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
