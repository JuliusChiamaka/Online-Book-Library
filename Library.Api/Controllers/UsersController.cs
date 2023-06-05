using Library.Application.Interfaces;
using Library.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<UserResponseDto>> AddUser(RegisterDto user)
        {
            var _user = await _userService.AddUser(user);
            return Ok(_user);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<UserResponseDto>> UpdateUser([FromBody] UserUpdateDto user)
        {
            var _user = await _userService.UpdateUser(user);
            return Ok(_user);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> SearchUsers(string? email, string? userName)
        {
            var users = await _userService.Search(email, userName);
            return Ok(users);
        }
    }
}
