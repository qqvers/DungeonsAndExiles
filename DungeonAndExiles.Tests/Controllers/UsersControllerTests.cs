﻿using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Services.Jwt;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DungeonAndExiles.Tests.Controller
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPlayerRepository _playerRepository;

        public UsersControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _jwtService = A.Fake<IJwtService>();
            _playerRepository = A.Fake<IPlayerRepository>();
            _usersController = new UsersController(_userRepository, _jwtService, _playerRepository);
        }

        [Fact]
        public async Task Create_ReturnsCreated_WhenUserIsRegistered()
        {
            // Arrange
            var userRegisterDto = new UserRegisterDto { Email = "test@example.com", Password = "Password123" };
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
            A.CallTo(() => _userRepository.RegisterUserAsync(userRegisterDto)).Returns(Task.FromResult(user));

            // Act
            var result = await _usersController.Create(userRegisterDto);

            // Assert
            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;
            createdResult.Value.Should().Be(user);
        }

        [Fact]
        public async Task Login_ReturnsOkWithToken_WhenCredentialsAreValid()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "Password123" };
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123") };
            var token = "fake-jwt-token";
            A.CallTo(() => _userRepository.FindUserInDatabase(userLoginDto)).Returns(Task.FromResult(user));
            A.CallTo(() => _jwtService.GenerateToken(user.Id.ToString())).Returns(token);

            // Act
            var result = await _usersController.Login(userLoginDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(new { User = user, Token = token });
        }

        [Fact]
        public async Task Login_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "Password123" };
            A.CallTo(() => _userRepository.FindUserInDatabase(userLoginDto)).Returns(Task.FromResult<User>(null));

            // Act
            var result = await _usersController.Login(userLoginDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("Provided email does not exist in database");
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenPasswordIsIncorrect()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "WrongPassword" };
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123") };
            A.CallTo(() => _userRepository.FindUserInDatabase(userLoginDto)).Returns(Task.FromResult(user));

            // Act
            var result = await _usersController.Login(userLoginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().Be("Incorrect password");
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "test@example.com" };
            A.CallTo(() => _userRepository.FindUserByIdAsync(userId)).Returns(Task.FromResult(user));

            // Act
            var result = await _usersController.GetUserById(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(user);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            A.CallTo(() => _userRepository.FindUserByIdAsync(userId)).Returns(Task.FromResult<User>(null));

            // Act
            var result = await _usersController.GetUserById(userId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("User with that Id does not exist");
        }

        [Fact]
        public async Task UpdateUser_ReturnsOk_WhenUserIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userUpdateDto = new UserUpdateDto { Email = "updated@example.com", Password = "UpdatedPassword123" };
            A.CallTo(() => _userRepository.UpdateUserAsync(userId, userUpdateDto)).Returns(Task.FromResult(true));

            // Act
            var result = await _usersController.UpdateUser(userId, userUpdateDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("User updated");
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userUpdateDto = new UserUpdateDto { Email = "updated@example.com", Password = "UpdatedPassword123" };
            A.CallTo(() => _userRepository.UpdateUserAsync(userId, userUpdateDto)).Returns(Task.FromResult(false));

            // Act
            var result = await _usersController.UpdateUser(userId, userUpdateDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("User not found or could not be updated");
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.FromResult(true));

            // Act
            var result = await _usersController.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.FromResult(false));

            // Act
            var result = await _usersController.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("User not found or could not be deleted");
        }

        [Fact]
        public async Task CreatePlayer_ReturnsCreated_WhenPlayerIsCreated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var playerDto = new PlayerDto { Name = "Player1"};
            var player = new Player { Id = Guid.NewGuid(), Name = "Player1", Level = 1 };
            A.CallTo(() => _playerRepository.CreatePlayerAsync(playerDto, userId)).Returns(Task.FromResult(player));

            // Act
            var result = await _usersController.CreatePlayer(userId, playerDto);

            // Assert
            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;
            createdResult.Value.Should().Be(player);
        }
    }
}