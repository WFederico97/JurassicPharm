using JurassicPharm.DTO.Cities;
using JurassicPharm.DTO.Personnel;
using JurassicPharm.DTO.Stores;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Personnel.Interfaces;
using JurassicPharm.Services.Personnel.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace JurassicPharm.Services.Personnel.Implementations
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _repository;

        public PersonnelService(IPersonnelRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetCitySummaryDTO>> GetCities()
        {
            return await _repository.GetCities();
        }

        public async Task<List<GetStoreDTO>> GetStores()
        {
            return await _repository.GetStores();
        }
        public async Task<List<GetPersonnelDTO>> GetAllPersonnel()
        {
            return await _repository.GetAllPersonnel();
        }

        public async Task<Empleado> GetPersonnel(int codigo)
        {
            return await _repository.GetPersonnel(codigo);
        }
        public async Task<bool> CreateEmployee(CreatePersonnelDTO employee)
        {
            return await _repository.CreateEmployee(employee);
        }
        public async Task<bool> UpdatePersonnel(UpdatePersonnelDTO personnel, int legajo)
        {
            return await _repository.UpdatePersonnel(personnel, legajo);
        }

        public async Task<bool> DeletePersonnel(int legajo)
        {
            return await _repository.DeletePersonnel(legajo);
        }
        public async Task<bool> ValidatePersonnelLogin(string email, string password)
        {
            return await _repository.ValidatePersonnelLogin(email, password);
        }

        public async Task<Empleado> GetByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }
        public async Task<string> CheckProlongedPrescriptionDate(int clientId)
        {
            return await _repository.CheckProlongedPrescriptionDate(clientId);
        }
    }
}
