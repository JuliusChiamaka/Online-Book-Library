using Library.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetUsers();
        Task<UserResponseDto> GetUser(Guid userId);
        Task<UserResponseDto> UpdateUser(UserUpdateDto user);

        Task<UserResponseDto> AddUser(RegisterDto user);
        Task DeleteUser(Guid userId);

        Task<IEnumerable<UserResponseDto>> Search(string email, string userName);
    }
}
