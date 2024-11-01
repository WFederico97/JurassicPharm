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
            Empleado employee = await _personnelService.GetByEmail(personnel.CorreoElectronico);
            if (employee == null)
            {
                return BadRequest("Ese correo no está registrado en nuestra base de datos");
            }

            bool isValidUser = await _personnelService.ValidatePersonnelLogin(personnel.CorreoElectronico, personnel.PasswordEmpleado);

            if (!isValidUser)
            {
                return Unauthorized("Credenciales inválidas");
            }

            var token = _authService.GenerateJwtToken(personnel.CorreoElectronico, employee.Rol, employee);
            return Ok(new { token, role = employee.Rol });
        }

    }
}
