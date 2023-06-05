using AutoMapper;
using Library.Application.Interfaces;
using Library.Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> AddUser(RegisterDto user)
        {
            var _user = _mapper.Map<IdentityUser>(user);
            var result = await _userManager.CreateAsync(_user, user.Password);

            if (!result.Succeeded)
            {
                return null;
            }
            var roleResult = await _userManager.AddToRoleAsync(_user, user.Role);
            var userResponse = _mapper.Map<UserResponseDto>(_user);
            return userResponse;
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.DeleteAsync(user);
        }

        public async Task<UserResponseDto> GetUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var _user = _mapper.Map<UserResponseDto>(user);
            return _user;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var _users = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return _users;
        }

        public async Task<IEnumerable<UserResponseDto>> Search(string email, string userName)
        {
            var users = await _userManager.Users
                .Where(u => (string.IsNullOrEmpty(email) || u.Email.Contains(email))
                    && (string.IsNullOrEmpty(userName) || u.UserName.Contains(userName)))
                .ToListAsync();

            var _users = _mapper.Map<IEnumerable<UserResponseDto>>(users);

            return _users;
        }


        public async Task<UserResponseDto> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (user == null)
            {
                return null;
            }

            var _user = _mapper.Map<IdentityUser>(userUpdateDto);

            var result = await _userManager.UpdateAsync(_user);
            if (!result.Succeeded)
            {
                return null;
            }

            var updatedUser = _mapper.Map<UserResponseDto>(_user);

            return updatedUser;
        }
    }
}
