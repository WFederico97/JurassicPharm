using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.HealthPlan;
using JurassicPharm.Models;
using JurassicPharm.Repositories.HealthPlan.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Repositories.HealthPlan.Implementations
{
    public class HealthPlanRepository : IHealthPlanRepository
    {
        private readonly JurassicPharmContext _context;
        public HealthPlanRepository(JurassicPharmContext context)
        {
            _context = context;
        }
        public async Task<List<HealthPlanDTO>> GetAll()
        {
            List<HealthPlanDTO> response = new List<HealthPlanDTO>();

            var healthPlanList = await _context.ObrasSociales.ToListAsync();

            healthPlanList.ForEach(plan => response.Add(new HealthPlanDTO() { Id = plan.IdObraSocial, Name = plan.Nombre }));

            return response;
        }
    }
}