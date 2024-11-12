using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Branch;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Branches.Implementations;
using JurassicPharm.Repositories.Branches.Interfaces;
using JurassicPharm.Services.Branches.Interfaces;

namespace JurassicPharm.Services.Branches.Implementations
{
    public class BranchesService : IBranchesService
    {
        private readonly IBranchesRepository _branchesRepository;

        public BranchesService(IBranchesRepository branchesRepository)
        {
            _branchesRepository = branchesRepository;
        }

        public async Task<List<BranchDTO>> GetAll()
        {
            return await _branchesRepository.GetAll();
        }
    }
}