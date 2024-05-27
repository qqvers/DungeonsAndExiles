using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid itemId)
        {
            try
            {
                var item = await _itemRepository.GetItemById(itemId);
                if (item == null)
                {
                    return NotFound(new { message = $"Item with ID {itemId} not found." });
                }
                var itemVM = _mapper.Map<ItemVM>(item);
                return Ok(itemVM);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsList()
        {
            try
            {
                var itemList = await _itemRepository.GetItemList();
                var itemVMList = _mapper.Map<List<ItemVM>>(itemList);
                return Ok(itemVMList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
