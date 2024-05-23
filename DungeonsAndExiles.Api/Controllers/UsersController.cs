using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndExiles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            var jwtService = new JwtService();
            var token = jwtService.GenerateToken(user.Id.ToString());
            
            return Ok(new { Token = token });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id can not be empty");
            var user = await _userRepository.FindUserByIdAsync(id);

            if (user == null) return NotFound("User with that Id does not exist");

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]Guid id, [FromBody] UserUpdateDto updatedUser)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (id == Guid.Empty)
            {
                return BadRequest("Id cannot be empty");
            }

            bool isUpdated = await _userRepository.UpdateUserAsync(id, updatedUser);

            if (!isUpdated)
            {
                return NotFound("User not found or could not be updated");
            }

            return Ok("User updated");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id can not be empty");

            bool isDeleted = await _userRepository.DeleteUserAsync(id);
            if(!isDeleted) return NotFound("User not found or could not be deleted");

            return NoContent();
        }




    }
}
