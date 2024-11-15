using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Cities;
using JurassicPharm.Models;
using JurassicPharm.Repositories.City.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Repositories.City.Implementations
{
    public class CityRepository : ICityRepository
    {
        private readonly JurassicPharmContext _context;
        public CityRepository(JurassicPharmContext context)
        {
            _context = context;
        }
        public async Task<List<GetCityDTO>> GetAll()
        {
            List<GetCityDTO> response = new List<GetCityDTO>();

            var cities = await _context.Ciudades
            .Include(l => l.IdLocalidadNavigation)
            .Include(l => l.IdLocalidadNavigation.IdProvinciaNavigation)
            .ToListAsync();

            cities.ForEach(city => response.Add(new GetCityDTO()
            {
                IdCiudad = city.IdCiudad,
                Nombre = city.Nombre,
                Localidad = city.IdLocalidadNavigation.Nombre,
                Provincia = city.IdLocalidadNavigation.IdProvinciaNavigation.Nombre
            })
            );

            return response;
        }
    }
}