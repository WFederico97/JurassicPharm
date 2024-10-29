using JurassicPharm.DTO.Personnel;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Personnel.Implementations;
using JurassicPharm.Repositories.Personnel.Interfaces;
using JurassicPharm.Services.Personnel.Interfaces;
using System.Net.Mail;

namespace JurassicPharm.Services.Personnel.Implementations
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _repository;

        public PersonnelService(IPersonnelRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Empleado>> GetAllPersonnel()
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


    }
}
