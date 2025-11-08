using EcommerceApp.DTOs;
using EcommerceApp.Models;

namespace EcommerceApp.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<RegisterResponseDto?> RegisterUserAsync(RegisterUserDto userDto);
        Task <LoginResponseDto?> LoginUserAsync(LoginDto loginDto);

    }
}
