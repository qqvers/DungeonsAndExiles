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
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    /// <summary>
    /// Handle operations related to players.
    /// </summary>
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

        /// <summary>
        /// Gets a player by their ID.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>The requested player</returns>
        /// <response code="200">Returns the requested player</response>
        /// <response code="204">If the no requested data was found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        [HttpGet("{playerId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
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
                _logger.LogInformation(ex, "Player with ID {PlayerId} not found", playerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a list of all players.
        /// </summary>
        /// <returns>A list of players</returns>
        /// <response code="200">Returns the list of players</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
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

        /// <summary>
        /// Deletes a player by their ID.
        /// </summary>
        /// <param name="playerId">The ID of the player to delete</param>
        /// <response code="204">If the player is successfully deleted or not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        /// <response code="403">If the authenticated user does not have permission to update the specified user's data</response>
        [HttpDelete("{playerId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to delete player with ID {PlayerId}", playerId);
            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);
            var userId = player.UserId;

            if (loggedInUserId != userId.ToString())
            {
                _logger.LogWarning("User does not have permission");
                return Forbid();
            }
            try
            {
                await _playerRepository.DeletePlayerAsync(playerId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation(ex, "Player with ID {PlayerId} not found", playerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while deleting player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an item from a player's backpack by the item's ID.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="itemId">The ID of the item to delete</param>
        /// <response code="204">If the item is successfully deleted or not found in backpack</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        /// <response code="403">If the authenticated user does not have permission to update the specified user's data</response>
        [HttpDelete("{playerId:Guid}/backpacks/items/{itemId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            _logger.LogInformation("Attempting to delete item with ID {ItemId} from backpack for player with ID {PlayerId}", itemId, playerId);
            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);
            var userId = player.UserId;

            if (loggedInUserId != userId.ToString())
            {
                _logger.LogWarning("User does not have permission");
                return Forbid();
            }
            try
            {
                await _playerRepository.RemoveItemFromBackpackAsync(playerId, itemId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation(ex, "Item with ID {ItemId} not found in backpack for player with ID {PlayerId}", itemId, playerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while deleting item with ID {ItemId} from backpack for player with ID {PlayerId}", itemId, playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Equips an item for a player.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="itemId">The ID of the item to equip</param>
        /// <response code="204">If the item is successfully equipped or not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        /// <response code="403">If the authenticated user does not have permission to update the specified user's data</response>
        [HttpPost("{playerId:Guid}/items/{itemId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EquipItem([FromRoute] Guid playerId, [FromRoute] Guid itemId)
        {
            _logger.LogInformation("Attempting to equip item with ID {ItemId} for player with ID {PlayerId}", itemId, playerId);
            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);
            var userId = player.UserId;

            if (loggedInUserId != userId.ToString())
            {
                _logger.LogWarning("User does not have permission");
                return Forbid();
            }
            try
            {
                await _playerRepository.EquipItemAsync(playerId, itemId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation(ex, "Item with ID {ItemId} not found for player with ID {PlayerId}", itemId, playerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while equipping item with ID {ItemId} for player with ID {PlayerId}", itemId, playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Initiates combat between a player and a monster.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="monsterId">The ID of the monster</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Result of the combat</returns>
        /// <response code="200">If the combat result is successfully returned</response>
        /// <response code="204">If the player or monster is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        /// <response code="400">If the player cannot start combat</response>
        /// <response code="499">If the request is cancelled by the client</response>
        /// <response code="403">If the authenticated user does not have permission to update the specified user's data</response>
        [HttpPost("{playerId:Guid}/monsters/{monsterId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(499)]
        public async Task<IActionResult> PlayerCombatWithMonster([FromRoute] Guid playerId, [FromRoute] Guid monsterId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to start combat between player with ID {PlayerId} and monster with ID {MonsterId}", playerId, monsterId);
            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);
            var userId = player.UserId;

            if (loggedInUserId != userId.ToString())
            {
                _logger.LogWarning("User does not have permission");
                return Forbid();
            }
            try
            {
                var result = await _playerRepository.CombatWithMonsterAsync(playerId, monsterId, cancellationToken);
                var message = result ? "You won" : "You lost";
                return Ok(message);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Request was cancelled by the client.");
                return StatusCode(499, "Request cancelled."); 
            }
            catch (PlayerCombatValidationException ex)
            {
                _logger.LogWarning(ex, "Player can not start a combat with 0 stamina or 0 space in backpack");
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation(ex, "Monster with ID {MonsterId} not found for player with ID {PlayerId}", monsterId, playerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while starting combat between player with ID {PlayerId} and monster with ID {MonsterId}", playerId, monsterId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the list of items in a player's backpack.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>The list of items in the player's backpack</returns>
        /// <response code="200">Returns the list of items</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{playerId:Guid}/backpacks/items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlayerBackpackItems([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to get backpack items for player with ID {PlayerId}", playerId);

            try
            {
                var itemList = await _playerRepository.GetPlayerBackpackItemsListAsync(playerId);
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting backpack items for player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the list of items in a player's equipment.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>The list of items in the player's equipment</returns>
        /// <response code="200">Returns the list of items</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{playerId:Guid}/equipments/items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlayerEquipmentItems([FromRoute] Guid playerId)
        {
            _logger.LogInformation("Attempting to get equipment items for player with ID {PlayerId}", playerId);

            try
            {
                var itemList = await _playerRepository.GetPlayerEquipmentItemsListAsync(playerId);
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting equipment items for player with ID {PlayerId}", playerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
