using Library.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IAuthService
    {
        string CreateToken(string email, string role);
        Task<bool> DoesUserExist(string email);
        Task<AuthResponseDto> Register(RegisterDto user);
        Task<AuthResponseDto> Login(LoginDto user);

        Task Logout();
    }
}
