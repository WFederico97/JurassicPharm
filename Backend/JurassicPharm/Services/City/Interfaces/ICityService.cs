using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Cities;

namespace JurassicPharm.Services.City.Interfaces
{
    public interface ICityService
    {
        public Task<List<GetCityDTO>> GetAll();
    }
}