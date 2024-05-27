using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Services;
using DungeonsAndExiles.Api.Services.Jwt;
using DungeonsAndExiles.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IJwtService jwtService, IPlayerRepository playerRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userRepository.RegisterUserAsync(userRegisterDto);
                var userVM = _mapper.Map<UserVM>(user);
                return Created("", userVM);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userRepository.FindUserInDatabase(userLoginDto);

                if (user == null)
                {
                    return NotFound("Provided email does not exist in database");
                }

                if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
                {
                    return Unauthorized("Incorrect password");
                }

                var userVM = _mapper.Map<UserVM>(user);
                var token = _jwtService.GenerateToken(user.Id.ToString());

                return Ok(new { User = userVM, Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            try
            {
                if (userId == Guid.Empty) return BadRequest("Id can not be empty");
                var user = await _userRepository.FindUserByIdAsync(userId);

                if (user == null) return NotFound("User with that Id does not exist");
                var userVM = _mapper.Map<UserVM>(user);
                return Ok(userVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserUpdateDto updatedUser)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                if (userId == Guid.Empty)
                {
                    return BadRequest("Id cannot be empty");
                }

                bool isUpdated = await _userRepository.UpdateUserAsync(userId, updatedUser);

                if (!isUpdated)
                {
                    return NotFound("User not found or could not be updated");
                }

                return Ok("User updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            try
            {
                if (userId == Guid.Empty) return BadRequest("Id can not be empty");

                bool isDeleted = await _userRepository.DeleteUserAsync(userId);
                if (!isDeleted) return NotFound("User not found or could not be deleted");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{userId}/create-player")]
        public async Task<IActionResult> CreatePlayer([FromRoute] Guid userId, [FromBody] PlayerDto playerDto)
        {
            try
            {
                var player = await _playerRepository.CreatePlayerAsync(playerDto, userId);
                var playerVM = _mapper.Map<PlayerVM>(player);
                return Created("", playerVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
