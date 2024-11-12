using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Branch;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Branches.Interfaces
{
    public interface IBranchesService
    {
        public Task<List<BranchDTO>> GetAll();
    }
}