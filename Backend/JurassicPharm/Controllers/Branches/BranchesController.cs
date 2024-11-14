using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.Services.Branches.Implementations;
using JurassicPharm.Services.Branches.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Branches
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchesService _branchesService;

        public BranchesController(IBranchesService branchesService)
        {
            _branchesService = branchesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _branchesService.GetAll());
            }
            catch (Exception error)
            {
                return StatusCode(
                    500,
                    $"An unexpected error occurred while get all branches. Error: {error.Message}"
                );
            }
        }
    }
}