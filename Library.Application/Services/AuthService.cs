using AutoMapper;
using Library.Application.Interfaces;
using Library.Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configure;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;

        public AuthService(IConfiguration configure, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            _configure = configure;
            _mapper = mapper;
            _userManager = userManager;
            _signinManager = signinManager;
        }
        public string CreateToken(string Email, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("sub", Email),
                new Claim("role", role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configure.GetSection("Security:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<bool> DoesUserExist(string email)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            return userExists != null;
        }
        public async Task<AuthResponseDto?> Register(RegisterDto user)
        {
            var _user = _mapper.Map<IdentityUser>(user);
            var result = await _userManager.CreateAsync(_user, user.Password);

            if (!result.Succeeded)
            {
                //_logger.LogError($"An error occured while creating user with email: {user.Email}");
                return null;
            }
            var roleResult = await _userManager.AddToRoleAsync(_user, user.Role);
            var authUser = _mapper.Map<AuthResponseDto>(_user);

            authUser.Jwt = CreateToken(_user.Email, user.Role);
            return authUser;

        }

        public async Task<AuthResponseDto?> Login(LoginDto user)
        {
            var userFromDb = await _userManager.FindByEmailAsync(user.Email);

            var loginResult = await _signinManager.PasswordSignInAsync(userFromDb.UserName, user.Password, user.RememberMe, false);

            if (!loginResult.Succeeded) return null;

            var authUser = _mapper.Map<AuthResponseDto>(userFromDb);
            var roles = await _userManager.GetRolesAsync(userFromDb);

            authUser.Jwt = CreateToken(user.Email, roles[0]);
            return authUser;
        }

        public async Task Logout()
        {
            await _signinManager.SignOutAsync();
        }
    }
}
