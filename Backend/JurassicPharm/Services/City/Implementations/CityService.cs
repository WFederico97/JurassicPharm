using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Cities;
using JurassicPharm.Repositories.City.Interfaces;
using JurassicPharm.Services.City.Interfaces;

namespace JurassicPharm.Services.City.Implementations
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<List<GetCityDTO>> GetAll()
        {
            return await _cityRepository.GetAll();
        }
    }
}