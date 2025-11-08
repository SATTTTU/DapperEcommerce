using EcommerceApp.DTOs;
using EcommerceApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            try
            {
                var createdUser = await _authService.RegisterUserAsync(userDto);

                return Ok(new
                {
                    message = "User registered successfully.",
                    user = createdUser
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred during registration.",
                    error = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loggedInUser = await _authService.LoginUserAsync(loginDto);

                return Ok(new
                {
                    message = "User logged in successfully.",
                    user = loggedInUser
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred during login.",
                    error = ex.Message
                });
            }
        }
    }

}
