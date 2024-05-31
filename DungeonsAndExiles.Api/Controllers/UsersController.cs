using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.Services;
using DungeonsAndExiles.Api.Services.Jwt;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DungeonsAndExiles.Api.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using BCrypt.Net;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, IJwtService jwtService, 
            IPlayerRepository playerRepository, IMapper mapper, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _playerRepository = playerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto userRegisterDto)
        {
            _logger.LogInformation("Attempting to register a new user");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for user registration");
                    return BadRequest(ModelState);
                }

                var user = await _userRepository.RegisterUserAsync(userRegisterDto);
                var userVM = _mapper.Map<UserVM>(user);
                _logger.LogInformation("User registered successfully");
                return Created("", userVM);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflict during user registration");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error during user registration");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            _logger.LogInformation("Attempting to log in user with email {Email}", userLoginDto.Email);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for user login");
                    return BadRequest(ModelState);
                }

                var user = await _userRepository.FindUserInDatabase(userLoginDto);
                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} not found", userLoginDto.Email);
                    return NotFound("Provided email does not exist in database");
                }

                if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
                {
                    _logger.LogWarning("Incorrect password for user with email {Email}", userLoginDto.Email);
                    return Unauthorized("Incorrect password");
                }

                var role = await _userRepository.GetUserRole(user.RoleId);
                var token = _jwtService.GenerateToken(user, role);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);

                await _userRepository.UpdateUserToken(user);

                var userVM = _mapper.Map<UserVM>(user);

                _logger.LogInformation("User with email {Email} logged in successfully", userLoginDto.Email);
                return Ok(new { User = userVM, Token = token, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error during user login");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId:Guid}")]
        [Authorize(Policy = "SignedInOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            _logger.LogInformation("Attempting to get user with ID {UserId}", userId);
            try
            {
                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("User ID is empty");
                    return BadRequest("Id can not be empty");
                }

                var user = await _userRepository.FindUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return NotFound("User with that Id does not exist");
                }

                var userVM = _mapper.Map<UserVM>(user);
                _logger.LogInformation("User with ID {UserId} retrieved successfully", userId);
                return Ok(userVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userId:Guid}")]
        [Authorize(Policy = "SignedInOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserUpdateDto updatedUser)
        {
            _logger.LogInformation("Attempting to update user with ID {UserId}", userId);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for updating user with ID {UserId}", userId);
                    return BadRequest(ModelState);
                }

                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("User ID is empty");
                    return BadRequest("Id cannot be empty");
                }

                bool isUpdated = await _userRepository.UpdateUserAsync(userId, updatedUser);
                if (!isUpdated)
                {
                    _logger.LogWarning("User with ID {UserId} not found or could not be updated", userId);
                    return NotFound("User not found or could not be updated");
                }

                _logger.LogInformation("User with ID {UserId} updated successfully", userId);
                return Ok("User updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while updating user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId:Guid}")]
        [Authorize(Policy = "SignedInOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            _logger.LogInformation("Attempting to delete user with ID {UserId}", userId);
            try
            {
                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("User ID is empty");
                    return BadRequest("Id can not be empty");
                }

                bool isDeleted = await _userRepository.DeleteUserAsync(userId);
                if (!isDeleted)
                {
                    _logger.LogWarning("User with ID {UserId} not found or could not be deleted", userId);
                    return NotFound("User not found or could not be deleted");
                }

                _logger.LogInformation("User with ID {UserId} deleted successfully", userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while deleting user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{userId:Guid}/create-player")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> CreatePlayer([FromRoute] Guid userId, [FromBody] PlayerDto playerDto)
        {
            _logger.LogInformation("Attempting to create player for user with ID {UserId}", userId);
            try
            {
                var player = await _playerRepository.CreatePlayerAsync(playerDto, userId);
                var playerVM = _mapper.Map<PlayerVM>(player);
                _logger.LogInformation("Player created successfully for user with ID {UserId}", userId);
                return Created("", playerVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while creating player for user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId:Guid}/players")]
        [Authorize(Policy = "SignedInOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GetListOfUserPlayers([FromRoute] Guid userId)
        {
            _logger.LogInformation("Attempting to get list of players for user with ID {UserId}", userId);
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("User ID is empty");
                return BadRequest("User ID cannot be empty.");
            }

            try
            {
                var listOfUserPlayers = await _playerRepository.GetPlayersByUserIdAsync(userId);
                if (listOfUserPlayers == null || !listOfUserPlayers.Any())
                {
                    _logger.LogWarning("No players found for user with ID {UserId}", userId);
                    return NotFound($"No players found for user with ID {userId}.");
                }

                var listOfUserPlayersM = _mapper.Map<List<PlayerVM>>(listOfUserPlayers);
                _logger.LogInformation("List of players for user with ID {UserId} retrieved successfully", userId);
                return Ok(listOfUserPlayersM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting list of players for user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{userId:Guid}/refresh")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Refresh([FromRoute] Guid userId, [FromBody] RefreshModel model)
        {
            _logger.LogInformation("Refresh called");

            if (userId == Guid.Empty)
            {
                _logger.LogWarning("User ID is empty");
                return BadRequest("User ID cannot be empty.");
            }
            try
            {
            var user = await _userRepository.FindUserByIdAsync(userId);

            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized();

            var role = await _userRepository.GetUserRole(user.RoleId);
            var tokenResult = _jwtService.GenerateToken(user, role);

            _logger.LogInformation("Refresh succeeded");

            return Ok(new
            {
                JwtToken = tokenResult.Token,
                Expiration = tokenResult.Expiration,
                RefreshToken = model.RefreshToken
            });

            }catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while refreshing token for user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

    }
}
