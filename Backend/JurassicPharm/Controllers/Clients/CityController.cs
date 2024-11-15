using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.Services.City.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Clients
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _cityService.GetAll());
            }
            catch (Exception error)
            {
                return StatusCode(500, $"Error inesperado. Error: {error.Message}");
            }
        }
    }
}