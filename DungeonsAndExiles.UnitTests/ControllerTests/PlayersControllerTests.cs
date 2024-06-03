using AutoMapper;
using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Models.Profiles;
using DungeonsAndExiles.Api.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DungeonsAndExiles.UnitTests.ControllerTests
{
    public class PlayersControllerTests
    {
        private readonly PlayersController _controller;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayersController> _logger;

        public PlayersControllerTests()
        {
            _playerRepository = A.Fake<IPlayerRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
            _logger = A.Fake<ILogger<PlayersController>>();
            _controller = new PlayersController(_playerRepository, _mapper, _logger);
        }

        [Fact]
        public async Task GetPlayer_ReturnsOkResult_WhenPlayerExists()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var player = new Player { Id = playerId };
            A.CallTo(() => _playerRepository.GetPlayerByIdAsync(playerId)).Returns(Task.FromResult(player));
            var playerVM = _mapper.Map<PlayerVM>(player);

            // Act
            var result = await _controller.GetPlayer(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(playerVM);
        }

        [Fact]
        public async Task GetPlayersList_ReturnsOkResult_WithPlayersList()
        {
            // Arrange
            var playersList = new List<Player> { new() { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerListAsync()).Returns(Task.FromResult<List<Player>?>(playersList));
            var playersListVM = _mapper.Map<List<PlayerVM>>(playersList);

            // Act
            var result = await _controller.GetPlayersList();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(playersListVM);
        }

        [Fact]
        public async Task GetPlayersList_ReturnsNotFound_WhenNoPlayers()
        {
            // Arrange
            A.CallTo(() => _playerRepository.GetPlayerListAsync()).Returns(Task.FromResult<List<Player>?>(null));

            // Act
            var result = await _controller.GetPlayersList();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("No players created");
        }

        [Fact]
        public async Task DeletePlayer_ReturnsNoContent_WhenPlayerDeleted()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            A.CallTo(() => _playerRepository.DeletePlayerAsync(playerId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeletePlayer(playerId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteItem_ReturnsNoContent_WhenItemDeleted()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            A.CallTo(() => _playerRepository.RemoveItemFromBackpackAsync(playerId, itemId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeleteItem(playerId, itemId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task EquipItem_ReturnsNoContent_WhenItemEquipped()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            A.CallTo(() => _playerRepository.EquipItemAsync(playerId, itemId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.EquipItem(playerId, itemId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task PlayerCombatWithMonster_ReturnsOkResult_WhenResultTrue()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var monsterId = Guid.NewGuid();
            var player = new Player { Id = playerId };
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            A.CallTo(() => _playerRepository.CombatWithMonsterAsync(playerId, monsterId, token)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.PlayerCombatWithMonster(playerId, monsterId, token);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be("You won");
        }

        [Fact]
        public async Task GetPlayerBackpackItems_ReturnsOkResult_WithItemsList()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var itemsList = new List<Item> { new() { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerBackpackItemsListAsync(playerId)).Returns(Task.FromResult(itemsList));
            var itemsListVM = _mapper.Map<List<Item>>(itemsList);

            // Act
            var result = await _controller.GetPlayerBackpackItems(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(itemsListVM, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetPlayerEquipmentItems_ReturnsOkResult_WithItemsList()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var itemsList = new List<Item> { new() { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerEquipmentItemsListAsync(playerId)).Returns(Task.FromResult(itemsList));
            var itemsListVM = _mapper.Map<List<Item>>(itemsList);

            // Act
            var result = await _controller.GetPlayerEquipmentItems(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(itemsListVM, options => options.ExcludingMissingMembers());
        }
    }
}
