using DungeonsAndExiles.Api.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid itemId)
        {
            var item = await _itemRepository.GetItemById(itemId);
            if (item == null)
            {
                return NotFound(new { message = $"Item with ID {itemId} not found." });
            }
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsList()
        {
            var itemList = await _itemRepository.GetItemList();
            return Ok(itemList);
        }
    }
}
