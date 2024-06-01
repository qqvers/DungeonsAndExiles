using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    /// <summary>
    /// Handle operations related to items.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SignedInOnly")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemRepository itemRepository, IMapper mapper, ILogger<ItemsController> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets an item by its ID.
        /// </summary>
        /// <param name="itemId">The ID of the item</param>
        /// <returns>The requested item</returns>
        /// <response code="200">Returns the requested item</response>
        /// <response code="404">If the item is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        [HttpGet("{itemId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GetItem([FromRoute] Guid itemId)
        {
            _logger.LogInformation("Attempting to get item with ID {ItemId}", itemId);
            try
            {
                var item = await _itemRepository.GetItemById(itemId);
                if (item == null)
                {
                    _logger.LogWarning("Item with ID {ItemId} not found", itemId);
                    return NotFound(new { message = $"Item with ID {itemId} not found." });
                }
                var itemVM = _mapper.Map<ItemVM>(item);
                _logger.LogInformation("Item with ID {ItemId} retrieved successfully", itemId);
                return Ok(itemVM);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Item with ID {ItemId} not found", itemId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting item with ID {ItemId}", itemId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a list of all items.
        /// </summary>
        /// <returns>A list of items</returns>
        /// <response code="200">Returns the list of items</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GetItemsList()
        {
            _logger.LogInformation("Attempting to get items list");
            try
            {
                var itemList = await _itemRepository.GetItemList();
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                _logger.LogInformation("Items list retrieved successfully");
                return Ok(itemVMList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting items list");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
