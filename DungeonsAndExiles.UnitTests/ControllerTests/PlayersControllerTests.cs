using AutoMapper;
using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Models.Profiles;
using DungeonsAndExiles.Api.Services.Jwt;
using DungeonsAndExiles.Api.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DungeonsAndExiles.UnitTests.ControllerTests
{
    public class PlayersControllerTests
    {
        private readonly PlayersController _controller;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayersController> _logger;

        private readonly Guid _playerId;
        private readonly Guid _itemId;
        private readonly Guid _backpackId;
        private readonly Guid _equipmentId;
        private readonly Guid _userId;
        private readonly Guid _roleId;
        private readonly Player _player;
        private readonly User _user;
        private readonly Role _role;
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

            _playerId = Guid.NewGuid();
            _itemId = Guid.NewGuid();
            _backpackId = Guid.NewGuid();
            _equipmentId = Guid.NewGuid();
            _userId = Guid.NewGuid();
            _roleId = Guid.NewGuid();

            _player = new Player
            {
                Id = _playerId,
                Name = "Player",
                BackpackId = _backpackId,
                Damage = 1,
                Defence = 1,
                Experience = 0,
                Health = 100,
                Level = 1,
                Stamina = 20,
                EquipmentId = _equipmentId,
                UserId = _userId
            };
            _user = new User { Id = _userId, Email = "email@email.com", Name = "John", Password = "password", Players = new List<Player>() { _player } };
            _role = new Role { Id = _roleId, Name = "User" };

            var fakeClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim(ClaimTypes.Role, _role.Name)
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };
        }

        [Fact]
        public async Task GetPlayer_ReturnsOkResult_WhenPlayerExists()
        {
            // Arrange
            A.CallTo(() => _playerRepository.GetPlayerByIdAsync(_playerId)).Returns(Task.FromResult(_player));
            var playerVM = _mapper.Map<PlayerVM>(_player);

            // Act
            var result = await _controller.GetPlayer(_playerId);

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
        public async Task DeletePlayer_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            A.CallTo(() => _playerRepository.DeletePlayerAsync(_playerId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeletePlayer(_playerId);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task DeleteItem_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            A.CallTo(() => _playerRepository.RemoveItemFromBackpackAsync(_playerId, _itemId)).Returns(null);

            // Act
            var result = await _controller.DeleteItem(_playerId, _itemId) as ForbidResult;

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task EquipItem_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            A.CallTo(() => _playerRepository.EquipItemAsync(_playerId, _itemId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.EquipItem(_playerId, _itemId);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task PlayerCombatWithMonster_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            var monsterId = Guid.NewGuid();
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            A.CallTo(() => _playerRepository.CombatWithMonsterAsync(_playerId, monsterId, token)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.PlayerCombatWithMonster(_playerId, monsterId, token);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task GetPlayerBackpackItems_ReturnsOkResult_WithItemsList()
        {
            // Arrange
            var itemsList = new List<Item> { new() { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerBackpackItemsListAsync(_playerId)).Returns(Task.FromResult(itemsList));
            var itemsListVM = _mapper.Map<List<Item>>(itemsList);

            // Act
            var result = await _controller.GetPlayerBackpackItems(_playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(itemsListVM, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetPlayerEquipmentItems_ReturnsOkResult_WithItemsList()
        {
            // Arrange
            var itemsList = new List<Item> { new() { Id = Guid.NewGuid() } };
            A.CallTo(() => _playerRepository.GetPlayerEquipmentItemsListAsync(_playerId)).Returns(Task.FromResult(itemsList));
            var itemsListVM = _mapper.Map<List<Item>>(itemsList);

            // Act
            var result = await _controller.GetPlayerEquipmentItems(_playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(itemsListVM, options => options.ExcludingMissingMembers());
        }
    }
}
