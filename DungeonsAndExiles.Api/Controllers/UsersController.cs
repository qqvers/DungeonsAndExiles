using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Data.Repository;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Services;
using DungeonsAndExiles.Api.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndExiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPlayerRepository _playerRepository;

        public UsersController(IUserRepository userRepository, IJwtService jwtService, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _playerRepository = playerRepository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userRepository.RegisterUserAsync(userRegisterDto);

            return Created("",user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
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

            var token = _jwtService.GenerateToken(user.Id.ToString());


            return Ok(new { User = user, Token = token });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest("Id can not be empty");
            var user = await _userRepository.FindUserByIdAsync(userId);

            if (user == null) return NotFound("User with that Id does not exist");

            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute]Guid userId, [FromBody] UserUpdateDto updatedUser)
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

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest("Id can not be empty");

            bool isDeleted = await _userRepository.DeleteUserAsync(userId);
            if(!isDeleted) return NotFound("User not found or could not be deleted");

            return NoContent();
        }

        [HttpPost("{userId}/create-player")]
        public async Task<IActionResult> CreatePlayer([FromRoute] Guid userId,[FromBody] PlayerDto playerDto)
        {
            var player = await _playerRepository.CreatePlayerAsync(playerDto, userId);
            return Created("", player);
        }


    }
}
