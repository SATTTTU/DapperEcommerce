using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EcommerceApp.DTOs;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;
using EcommerceApp.Services.Interfaces;

namespace EcommerceApp.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepositories _authRepo;
        private readonly IConfiguration _config;

        public AuthServices(IAuthRepositories authRepo, IConfiguration config)
        {
            _authRepo = authRepo;
            _config = config;
        }

        public async Task<RegisterResponseDto?> RegisterUserAsync(RegisterUserDto userDto)
        {
           

            try

            {
                if (string.IsNullOrWhiteSpace(userDto.Name) ||
               string.IsNullOrWhiteSpace(userDto.Email) ||
               string.IsNullOrWhiteSpace(userDto.Password))
                {
                    throw new ArgumentException("Name, email, and password are required.");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                var user = new Users
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = hashedPassword,
                    Phone = userDto.Phone,
                    Role = Enums.UserRole.Customer,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var savedUser = await _authRepo.RegisterUserAsync(user);

                if (savedUser == null)
                    throw new Exception("User registration failed. Please try again.");

                return new RegisterResponseDto
                {
                    Id = savedUser.Id.ToString(),
                    Name = savedUser.Name,
                    Email = savedUser.Email,
                    Phone = savedUser.Phone
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}");
            }
        }

        public async Task<LoginResponseDto?> LoginUserAsync(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ArgumentException("Email and password are required.");

            try
            {
                var user = new Users
                {
                    Email = loginDto.Email,
                    Password = loginDto.Password
                };

                var existingUser = await _authRepo.LoginUserAsync(user);

                if (existingUser == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, existingUser.Password))
                    throw new UnauthorizedAccessException("Invalid email or password.");

                var token = GenerateJwtToken(existingUser);

                return new LoginResponseDto
                {
                    Id = existingUser.Id.ToString(),
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    Role = existingUser.Role.ToString(),
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}");
            }
        }

        private string GenerateJwtToken(Users user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpiresInHours"] ?? "3")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
