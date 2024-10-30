using JurassicPharm.DTO.Personnel;
using JurassicPharm.Models;
using JurassicPharm.Services.Personnel.Implementations;
using JurassicPharm.Services.Personnel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Personnel
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {

        private readonly IPersonnelService _service;

        public PersonnelController (IPersonnelService service)
        {
            _service = service;
        }

        [HttpGet("/GetCities")]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                return Ok(await _service.GetCities());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cometer la operacion: {ex.Message}");
            }
        }

        [HttpGet("/GetStores")]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                return Ok(await _service.GetStores());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cometer la operacion: {ex.Message}");
            }
        }

        [HttpGet("/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAllPersonnel());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cometer la operacion: {ex.Message}");
            }

        }

        [HttpGet("/GetEmployeeById/{legajo}")]
        public async Task<IActionResult> GetById([FromRouteAttribute]  int legajo)
        {
            try
            {
                return Ok(await _service.GetPersonnel(legajo));
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al cometer la operacion: {ex.Message}");
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("/NewEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreatePersonnelDTO employee)
        {
            if(employee.IdSucursal <= 0 || employee.IdCiudad <= 0)
            {
                return BadRequest("Sucursal o Ciudad incorrecta");
            }
            try
            {
                bool isCreated = await _service.CreateEmployee(employee);
                if (!isCreated)
                {
                    return BadRequest($"El empleado {employee.Nombre},{employee.Apellido} no pudo ser dado de alta");
                }
                    return Ok("Empleado dado de alta correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cometer la operacion: {ex.Message}");
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut("/UpdateEmployee/{legajo}")]
        public async Task<IActionResult> Update([FromRoute] int legajo, [FromBody] UpdatePersonnelDTO updatedPersonnel)
        {
            try
            {
                bool isUpdated = await _service.UpdatePersonnel(updatedPersonnel, legajo);

                if (!isUpdated)
                {
                    return BadRequest($"El empleado de legajo: {legajo} no pudo ser actualizado");
                }
                return Ok("Empleado actualizado");
            }
            catch (Exception ex)
            {
                return StatusCode(501, $"Internal Server Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut("/DeleteEmployee/{legajo}")]
        public async Task<IActionResult> Delete([FromRoute] int legajo)
        {
            try
            {
                bool isUpdated = await _service.DeletePersonnel(legajo);

                if (!isUpdated)
                {
                    return BadRequest($"El empleado de legajo: {legajo} no pudo ser dado de baja");
                }
                return Ok("Empleado dado de baja");
            }
            catch (Exception ex)
            {
                return StatusCode(501, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
