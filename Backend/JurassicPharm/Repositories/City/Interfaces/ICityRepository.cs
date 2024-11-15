using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.Cities;

namespace JurassicPharm.Repositories.City.Interfaces
{
    public interface ICityRepository
    {
        public Task<List<GetCityDTO>> GetAll();
    }
}