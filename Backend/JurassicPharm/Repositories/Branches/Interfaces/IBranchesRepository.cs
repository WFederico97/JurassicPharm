using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Branch;
using JurassicPharm.Models;

namespace JurassicPharm.Repositories.Branches.Interfaces
{
    public interface IBranchesRepository
    {
        public Task<List<BranchDTO>> GetAll();
    }
}