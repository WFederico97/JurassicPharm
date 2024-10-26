using BCrypt.Net;
using JurassicPharm.Services.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _authService;

        public AuthController(JwtService jwtService)
        {
            _authService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string Email, [FromQuery] string Password )
        {
            if (await ValidateUserAsync(Email, Password)) 
            {
                //string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 13);
                //bool passwordReveal = BCrypt.Net.BCrypt.EnhancedVerify(Password, passwordHash);

                var token = _authService.GenerateJwtToken(Email, "Admin"); // Puedes asignar roles aquí
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private async Task<bool> ValidateUserAsync(string Email, string password)
        {
            await Task.Delay(100);
            return Email == "412501@gmail.com" && password == "password"; 
        }

    }
}
