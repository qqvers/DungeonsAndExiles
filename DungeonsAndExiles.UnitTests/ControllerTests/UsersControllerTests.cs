using AutoMapper;
using Castle.Core.Logging;
using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Models.Profiles;
using DungeonsAndExiles.Api.Services.Jwt;
using DungeonsAndExiles.Api.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DungeonsAndExiles.UnitTests.ControllerTests
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        private readonly Guid _playerId;
        private readonly Guid _itemId;
        private readonly Guid _backpackId;
        private readonly Guid _equipmentId;
        private readonly Guid _userId;
        private readonly Guid _roleId;
        private readonly Player _player;
        private readonly User _user;
        private readonly Role _role;

        public UsersControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _jwtService = A.Fake<IJwtService>();
            _playerRepository = A.Fake<IPlayerRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
            _logger = A.Fake<ILogger<UsersController>>();
            _usersController = new UsersController(_userRepository, _jwtService, _playerRepository, _mapper, _logger);

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

            _usersController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };
        }

        [Fact]
        public async Task Create_ReturnsCreated_WhenUserIsRegistered()
        {
            // Arrange
            var userRegisterDto = new UserRegisterDto { Email = "test@example.com", Password = "Password123" };
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
            A.CallTo(() => _userRepository.RegisterUserAsync(userRegisterDto)).Returns(Task.FromResult(user));
            var userVM = _mapper.Map<UserVM>(user);

            // Act
            var result = await _usersController.Create(userRegisterDto);

            // Assert
            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;
            createdResult?.Value.Should().BeEquivalentTo(userVM);
        }

        [Fact]
        public async Task Login_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "Password123" };
            A.CallTo(() => _userRepository.FindUserInDatabase(userLoginDto)).Throws<NotFoundException>();

            // Act
            var result = await _usersController.Login(userLoginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenPasswordIsIncorrect()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "WrongPassword" };
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123") };
            A.CallTo(() => _userRepository.FindUserInDatabase(userLoginDto)).Returns(Task.FromResult<User?>(user));

            // Act
            var result = await _usersController.Login(userLoginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult?.Value.Should().Be("Incorrect password");
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "test@example.com" };
            A.CallTo(() => _userRepository.FindUserByIdAsync(userId)).Returns(Task.FromResult<User?>(user));
            var userVM = _mapper.Map<UserVM>(user);

            // Act
            var result = await _usersController.GetUserById(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(userVM);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            A.CallTo(() => _userRepository.FindUserByIdAsync(userId)).Throws<NotFoundException>();

            // Act
            var result = await _usersController.GetUserById(userId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task UpdateUser_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userUpdateDto = new UserUpdateDto { Email = "updated@example.com", Password = "UpdatedPassword123" };
            A.CallTo(() => _userRepository.UpdateUserAsync(userId, userUpdateDto)).Throws<NotFoundException>();

            // Act
            var result = await _usersController.UpdateUser(userId, userUpdateDto);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task DeleteUser_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            var userId = Guid.NewGuid();
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.FromResult(true));

            // Act
            var result = await _usersController.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task CreatePlayer_ReturnsForbid_WhenNoClaims()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var playerDto = new PlayerDto { Name = "Player1" };
            var player = new Player { Id = Guid.NewGuid(), Name = "Player1", Level = 1 };
            A.CallTo(() => _playerRepository.CreatePlayerAsync(playerDto, userId)).Returns(Task.FromResult(player));
            var playerVM = _mapper.Map<PlayerVM>(player);


            // Act
            var result = await _usersController.CreatePlayer(userId, playerDto);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task GetListOfUserPlayers_ReturnsOk_WithListOfPlayers()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var players = new List<Player>
            {
                new() { Id = Guid.NewGuid(), UserId = userId, Name = "Player1" },
                new() { Id = Guid.NewGuid(), UserId = userId, Name = "Player2" }
            };
            A.CallTo(() => _playerRepository.GetPlayersByUserIdAsync(userId)).Returns(players);

            // Act
            var result = await _usersController.GetListOfUserPlayers(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(_mapper.Map<List<PlayerVM>>(players));
        }

        [Fact]
        public async Task GetListOfUserPlayers_ReturnsBadRequest_WhenUserIdIsEmpty()
        {
            // Act
            var result = await _usersController.GetListOfUserPlayers(Guid.Empty);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}
