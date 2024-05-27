using AutoMapper;
using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Models.Profiles;
using DungeonsAndExiles.Api.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndExiles.Tests.Controllers
{
    public class ItemsControllerTests
    {
        private readonly ItemsController _itemsController;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsControllerTests()
        {
            _itemRepository = A.Fake<IItemRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
            _itemsController = new ItemsController(_itemRepository, _mapper);
        }

        [Fact]
        public async Task GetItem_ReturnsItem_WhenItemExists()
        {
            // Arrange
            var sampleItem = new Item { Id = Guid.NewGuid(), Name = "Sword", Type = "Weapon", Damage = 30, Defence = 0 };
            var sampleItemVM = _mapper.Map<ItemVM>(sampleItem); 
            A.CallTo(() => _itemRepository.GetItemById(sampleItem.Id)).Returns(Task.FromResult(sampleItem));


            // Act
            var result = await _itemsController.GetItem(sampleItem.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(sampleItemVM);
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
            var sampleItemsVM = _mapper.Map<List<ItemVM>>(sampleItems);

            // Act
            var result = await _itemsController.GetItemsList();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<List<ItemVM>>();
            var returnValue = okResult.Value as List<ItemVM>;
            returnValue.Should().HaveCount(3);
            returnValue.Should().BeEquivalentTo(sampleItemsVM);
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
