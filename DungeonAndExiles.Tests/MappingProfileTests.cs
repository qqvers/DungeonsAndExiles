using AutoMapper;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Models.Profiles;
using DungeonsAndExiles.Api.ViewModels;
using FluentAssertions;

namespace DungeonAndExiles.Tests
{

    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_ItemToItemVM_ShouldMapCorrectly()
        {
            // Arrange
            var item = new Item { Id = Guid.NewGuid(), Name = "Sword", Type = "Weapon", Damage = 30, Defence = 0 };

            // Act
            var itemVM = _mapper.Map<ItemVM>(item);

            // Assert
            itemVM.Should().NotBeNull();
            itemVM.Id.Should().Be(item.Id);
            itemVM.Name.Should().Be(item.Name);
            itemVM.Type.Should().Be(item.Type);
            itemVM.Damage.Should().Be(item.Damage);
            itemVM.Defence.Should().Be(item.Defence);
        }

        [Fact]
        public void Map_ItemListToItemVMList_ShouldMapCorrectly()
        {
            // Arrange
            var items = new List<Item>
        {
            new Item { Id = Guid.NewGuid(), Name = "Sword", Type = "Weapon", Damage = 30, Defence = 0 },
            new Item { Id = Guid.NewGuid(), Name = "Shield", Type = "Armor", Damage = 0, Defence = 20 },
            new Item { Id = Guid.NewGuid(), Name = "Bow", Type = "Weapon", Damage = 20, Defence = 0 }
        };

            // Act
            var itemVMs = _mapper.Map<List<ItemVM>>(items);

            // Assert
            itemVMs.Should().NotBeNull();
            itemVMs.Should().HaveCount(3);
        }
    }

}