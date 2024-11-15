using JurassicPharm.DTOs.Personnel;
using JurassicPharm.Models;
using JurassicPharm.Services.JWT;
using JurassicPharm.Services.Personnel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _authService;
        private readonly IPersonnelService _personnelService;

        public AuthController(JwtService jwtService, IPersonnelService personnelService)
        {
            _authService = jwtService;
            _personnelService = personnelService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPersonnelDTO personnel)
        {
            try
            {
                Empleado employee = await _personnelService.GetByEmail(personnel.CorreoElectronico);
                if (employee == null)
                {
                    return BadRequest(new { message = "Ese correo no está registrado en nuestra base de datos" });
                }

                bool isValidUser = await _personnelService.ValidatePersonnelLogin(personnel.CorreoElectronico, personnel.PasswordEmpleado);

                if (!isValidUser)
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                var token = _authService.GenerateJwtToken(personnel.CorreoElectronico, employee);
                return Ok(new { token, role = employee.Rol });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error inesperado en el inicio de sesión: {ex.Message}" });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            try
            {
                await _personnelService.ForgotPassword(email);

                // Evitar que un atacante potencial descubra si el correo electrónico existe o no.
                return Accepted(new { message = "Si el correo está registrado, se enviará un enlace para restablecer la contraseña." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error inesperado en la solicitud de restablecimiento de contraseña: {ex.Message}" });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] string newPassword)
        {
            try
            {
                await _personnelService.ResetPassword(token, newPassword);
                return Ok(new { message = "Contraseña restablecida con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error inesperado al restablecer la contraseña: {ex.Message}" });
            }
        }
    }

}
