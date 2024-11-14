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


        [HttpGet("selectOptions")]
        public async Task<IActionResult> GetSelectOptions()
        {
            var result = await _service.GetSelectOptions();
            return Ok(result);
        }

        [HttpGet("Supplies")]
        public async Task<IActionResult> GetAllSupply()
        {
            try
            {
                return Ok(await _service.GetAllSupply());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar la operacion: {ex.Message}");
            }
        }

        [Authorize("RequireStockManagerRole")]
        [HttpPost("NewSupply")]

        public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyDTO supply)
        {
            try
            {
                bool isValid = await _service.CreateSupply(supply);
                if (isValid)
                {
                    return Ok("Suministro creado con exito");
                }
                return StatusCode(500, $"Error al realizar la operacion");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar la operacion: {ex.Message}");
            }
        }

        [Authorize("RequireStockManagerRole")]
        [HttpPut("UpdateSupply/{codigo}")]
        public async Task<IActionResult> UpdateSupply([FromBody] UpdateSupplyDTO supply, [FromRoute] int codigo)
        {
            try
            {
                bool isValid = await _service.UpdateSupply(supply, codigo);
                if (isValid)
                {
                    return Ok("Suministro actualizado con exito");
                }
                return StatusCode(500, $"Error al realizar la operacion");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar la operacion: {ex.Message}");
            }
        }



    }
}
