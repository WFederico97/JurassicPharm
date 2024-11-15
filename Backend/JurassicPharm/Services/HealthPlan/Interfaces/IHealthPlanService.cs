using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.Models;
using JurassicPharm.DTO.HealthPlan;

namespace JurassicPharm.Services.HealthPlan.Interfaces
{
    public interface IHealthPlanService
    {
        public Task<List<HealthPlanDTO>> GetAll();
    }
}