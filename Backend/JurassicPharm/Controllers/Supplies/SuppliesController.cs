using JurassicPharm.DTO.Supplies;
using JurassicPharm.Services.Supplies.Implementations;
using JurassicPharm.Services.Supplies.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Supplies
{
    [ApiController]
    [Route("api/")]
    public class SuppliesController : ControllerBase
    {
        private readonly ISuppliesService _service;

        public SuppliesController(ISuppliesService service)
        {
            _service = service;
        }

        [HttpGet("salesBySupply")]
        public async Task<IActionResult> GetSalesBySupply()
        {
            try
            {
                var result = await _service.GetCurrentYearSalesBySupply();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener la facturación por suministro: {ex.Message}" });
            }
        }

        [HttpGet("selectOptions")]
        public async Task<IActionResult> GetSelectOptions()
        {
            try
            {
                var result = await _service.GetSelectOptions();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener las opciones de selección: {ex.Message}" });
            }
        }

        [HttpGet("Supplies")]
        public async Task<IActionResult> GetAllSupply()
        {
            try
            {
                var supplies = await _service.GetAllSupply();
                return Ok(supplies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener los suministros: {ex.Message}" });
            }
        }

        [Authorize("RequireStockManagerRole")]
        [HttpPost("NewSupply")]
        public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyDTO supply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos de suministro inválidos." });
            }

            try
            {
                bool isCreated = await _service.CreateSupply(supply);
                if (isCreated)
                {
                    return Ok(new { message = "Suministro creado con éxito" });
                }
                return StatusCode(500, new { message = "No se pudo crear el suministro." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al crear el suministro: {ex.Message}" });
            }
        }

        [Authorize("RequireStockManagerRole")]
        [HttpPut("UpdateSupply/{codigo}")]
        public async Task<IActionResult> UpdateSupply([FromBody] UpdateSupplyDTO supply, [FromRoute] int codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos de suministro inválidos." });
            }

            try
            {
                bool isUpdated = await _service.UpdateSupply(supply, codigo);
                if (isUpdated)
                {
                    return Ok(new { message = "Suministro actualizado con éxito" });
                }
                return NotFound(new { message = $"No se encontró un suministro con el código {codigo}." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al actualizar el suministro: {ex.Message}" });
            }
        }




    }
}
