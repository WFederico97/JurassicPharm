using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.Services.HealthPlan.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.HealthPlan
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthPlanController : ControllerBase
    {

        private readonly IHealthPlanService _healthPlanService;
        public HealthPlanController(IHealthPlanService healthPlanService)
        {
            _healthPlanService = healthPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _healthPlanService.GetAll());
            }
            catch (Exception error)
            {
                return StatusCode(500, $"Error inesperado. Error: {error.Message}");
            }
        }
    }
}