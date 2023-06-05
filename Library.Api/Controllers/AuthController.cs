using Library.Application.Interfaces;
using Library.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto?>> RegisterUser(RegisterDto user)
        {
            var userExists = await _authService.DoesUserExist(user.Email);

            if (userExists) return BadRequest("User already exists");

            var userOrNull = await _authService.Register(user);
            return userOrNull == null ? BadRequest("Unable to create user") : Ok(userOrNull);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto?>> LoginUser(LoginDto user)
        {
            var userExists = await _authService.DoesUserExist(user.Email);
            if (!userExists) return BadRequest("Invalid credentials");

            var result = await _authService.Login(user);

            return result == null ? BadRequest("Login attempt failed") : Ok(result);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _authService.Logout();
            return NoContent();
        }
    }
}
