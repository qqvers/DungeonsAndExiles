using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Models.Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DungeonAndExiles.Tests.Controller
{
    public class ItemsControllerTests
    {
        private readonly ItemsController _itemsController;
        private readonly IItemRepository _itemRepository;

        public ItemsControllerTests()
        {
            _itemRepository = A.Fake<IItemRepository>();
            _itemsController = new ItemsController(_itemRepository);
        }

        [Fact]
        public async Task GetItem_ReturnsItem_WhenItemExists()
        {
            // Arrange
            var sampleItem = new Item { Id = Guid.NewGuid(), Name = "Sword", Type = "Weapon", Damage = 30, Defence = 0 };
            A.CallTo(() => _itemRepository.GetItemById(sampleItem.Id)).Returns(Task.FromResult(sampleItem));

            // Act
            var result = await _itemsController.GetItem(sampleItem.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(sampleItem);
        }

        [Fact]
        public async Task GetItem_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var nonExistentItemId = Guid.NewGuid();
            A.CallTo(() => _itemRepository.GetItemById(nonExistentItemId)).Returns(Task.FromResult<Item>(null));

            // Act
            var result = await _itemsController.GetItem(nonExistentItemId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().BeEquivalentTo(new { message = $"Item with ID {nonExistentItemId} not found." });
        }

        [Fact]
        public async Task GetItemsList_ReturnsListOfItems_WhenCalled()
        {
            // Arrange
            var sampleItems = GetSampleItems();
            A.CallTo(() => _itemRepository.GetItemList()).Returns(Task.FromResult(sampleItems));

            // Act
            var result = await _itemsController.GetItemsList();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<List<Item>>();
            var returnValue = okResult.Value as List<Item>;
            returnValue.Should().HaveCount(3);
        }

        private List<Item> GetSampleItems()
        {
            return new List<Item>
            {
                new Item { Id = Guid.NewGuid(), Name = "Sword", Type = "Weapon", Damage = 30, Defence = 0 },
                new Item { Id = Guid.NewGuid(), Name = "Shield", Type = "Armor", Damage = 0, Defence = 20 },
                new Item { Id = Guid.NewGuid(), Name = "Potion", Type = "Consumable", Damage = 0, Defence = 0 }
            };
        }
    }
}
