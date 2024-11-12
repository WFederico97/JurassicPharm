using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Branch;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Branches.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Repositories.Branches.Implementations
{
    public class BranchesRepository : IBranchesRepository
    {
        private readonly JurassicPharmContext _context;

        public BranchesRepository(JurassicPharmContext context)
        {
            _context = context;
        }
        public async Task<List<BranchDTO>> GetAll()
        {
            return await _context.Sucursales.Include(sucursal => sucursal.IdCiudadNavigation)
                                            .Select(sucursal => new BranchDTO()
                                            {
                                                Id = sucursal.IdSucursal,
                                                Address = sucursal.Calle,
                                                StreetLevel = sucursal.Altura,
                                                City = sucursal.IdCiudadNavigation.Nombre
                                            }
                                           ).ToListAsync();
        }
    }
}