using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.HealthPlan;
using JurassicPharm.Models;

namespace JurassicPharm.Repositories.HealthPlan.Interfaces
{
    public interface IHealthPlanRepository
    {
        public Task<List<HealthPlanDTO>> GetAll();
    }
}