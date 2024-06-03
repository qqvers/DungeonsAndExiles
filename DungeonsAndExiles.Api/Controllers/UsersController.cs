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
    /// <summary>
    /// Handle operations related to users.
    /// </summary>
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userRegisterDto">User registration data</param>
        /// <returns>The created user</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user registration data is invalid</response>
        /// <response code="409">If there is a conflict during user registration</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="429">If the request limit is exceeded</response>
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

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userLoginDto">User login data</param>
        /// <returns>A token and refresh token for the logged in user</returns>
        /// <response code="200">Returns the token and refresh token</response>
        /// <response code="400">If the user login data is invalid</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="429">If the request limit is exceeded</response>
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

                if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user!.Password))
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
            }catch(NotFoundException ex)
            {
                _logger.LogWarning("User with email {Email} not found", userLoginDto.Email);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error during user login");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve</param>
        /// <returns>The user with the specified ID</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="400">If the user ID is invalid</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
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

                var userVM = _mapper.Map<UserVM>(user);
                _logger.LogInformation("User with ID {UserId} retrieved successfully", userId);
                return Ok(userVM);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="userId">The ID of the user to update</param>
        /// <param name="updatedUser">The updated user data</param>
        /// <returns>A message indicating the result of the update</returns>
        /// <response code="200">Returns a message indicating the user was updated</response>
        /// <response code="400">If the user update data is invalid</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
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

                await _userRepository.UpdateUserAsync(userId, updatedUser);

                _logger.LogInformation("User with ID {UserId} updated successfully", userId);
                return Ok("User updated");
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("User with ID {UserId} not found or could not be updated", userId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while updating user with ID {UserId}", userId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the user was deleted successfully</response>
        /// <response code="400">If the user ID is invalid</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
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

        /// <summary>
        /// Creates a new player for a user.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="playerDto">The player data</param>
        /// <returns>The created player</returns>
        /// <response code="201">Returns the newly created player</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
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

        /// <summary>
        /// Retrieves the list of players for a user.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A list of players for the user</returns>
        /// <response code="200">Returns the list of players</response>
        /// <response code="400">If the user ID is invalid</response>
        /// <response code="404">If no players are found for the user</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="429">If the request limit is exceeded</response>
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

        /// <summary>
        /// Refreshes a user's token.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="model">The refresh token model</param>
        /// <returns>A new token and refresh token for the user</returns>
        /// <response code="200">Returns the new token and refresh token</response>
        /// <response code="400">If the user ID is invalid</response>
        /// <response code="401">If the refresh token is invalid or expired</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="429">If the request limit is exceeded</response>
        /// <response code="404">If no player is found</response>
        [HttpPost("{userId:Guid}/refresh")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            if (user!.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized();

            var role = await _userRepository.GetUserRole(user.RoleId);
            var tokenResult = _jwtService.GenerateToken(user, role);

            _logger.LogInformation("Refresh succeeded");

            return Ok(new
            {
                JwtToken = tokenResult.Token,
                tokenResult.Expiration,
                model.RefreshToken
            });

            }
            catch(NotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
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
