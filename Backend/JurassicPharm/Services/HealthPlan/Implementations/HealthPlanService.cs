using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.HealthPlan;
using JurassicPharm.Models;
using JurassicPharm.Repositories.HealthPlan.Interfaces;
using JurassicPharm.Services.HealthPlan.Interfaces;

namespace JurassicPharm.Services.HealthPlan.Implementations
{
    public class HealthPlanService : IHealthPlanService
    {
        private readonly IHealthPlanRepository _healthPlanRepository;
        public HealthPlanService(IHealthPlanRepository healthPlanRepository)
        {
            _healthPlanRepository = healthPlanRepository;
        }
        public async Task<List<HealthPlanDTO>> GetAll()
        {
            return await _healthPlanRepository.GetAll();
        }
    }
}