using JurassicPharm.DTO.Supplies;
using JurassicPharm.Services.Supplies.Interfaces;
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

        [HttpPost("NewSupply")]

        public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyDTO supply)
        {
            try
            {
                bool isValid = await _service.CreateSupply(supply);
                if (isValid)
                {
                    Ok("Suministro creado con exito");
                }
                return StatusCode(500, $"Error al realizar la operacion");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar la operacion: {ex.Message}");
            }
        }
        //public async Task<IActionResult> DeleteSupply()
        //{
            
        //}


        
    }
}
