using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.Models.Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DungeonsAndExiles.Api.Tests.Controllers
{
    public class PlayersControllerTests
    {
        private readonly PlayersController _controller;
        private readonly IPlayerRepository _playerRepository;

        public PlayersControllerTests()
        {
            _playerRepository = A.Fake<IPlayerRepository>();
            _controller = new PlayersController(_playerRepository);
        }

        [Fact]
        public async Task GetPlayer_ReturnsOkResult_WhenPlayerExists()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var player = new Player { Id = playerId };
            A.CallTo(() => _playerRepository.GetPlayerByIdAsync(playerId)).Returns(Task.FromResult(player));

            // Act
            var result = await _controller.GetPlayer(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(player);
        }

        [Fact]
        public async Task GetPlayersList_ReturnsOkResult_WithPlayersList()
        {
            // Arrange
            var playersList = new List<Player> { new Player { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerListAsync()).Returns(Task.FromResult(playersList));

            // Act
            var result = await _controller.GetPlayersList();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(playersList);
        }

        [Fact]
        public async Task GetPlayersList_ReturnsNotFound_WhenNoPlayers()
        {
            // Arrange
            A.CallTo(() => _playerRepository.GetPlayerListAsync()).Returns(Task.FromResult<List<Player>>(null));

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
        public async Task PlayerCombatWithMonster_ReturnsOkResult_WithUpdatedPlayer()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var monsterId = Guid.NewGuid();
            var player = new Player { Id = playerId };
            A.CallTo(() => _playerRepository.CombatWithMonsterAsync(playerId, monsterId)).Returns(Task.FromResult(player));

            // Act
            var result = await _controller.PlayerCombatWithMonster(playerId, monsterId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(player);
        }

        [Fact]
        public async Task GetPlayerBackpackItems_ReturnsOkResult_WithItemsList()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var itemsList = new List<Item> { new Item { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerBackpackItemsListAsync(playerId)).Returns(Task.FromResult(itemsList));

            // Act
            var result = await _controller.GetPlayerBackpackItems(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(itemsList);
        }

        [Fact]
        public async Task GetPlayerEquipmentItems_ReturnsOkResult_WithItemsList()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var itemsList = new List<Item> { new Item { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerEquipmentItemsListAsync(playerId)).Returns(Task.FromResult(itemsList));

            // Act
            var result = await _controller.GetPlayerEquipmentItems(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(itemsList);
        }
    }
}
