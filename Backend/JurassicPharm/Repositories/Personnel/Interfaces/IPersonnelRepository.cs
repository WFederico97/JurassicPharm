using JurassicPharm.DTO.Personnel;
using JurassicPharm.Models;

namespace JurassicPharm.Repositories.Personnel.Interfaces
{
    public interface IPersonnelRepository
    {
        Task<bool> CreateEmployee(CreatePersonnelDTO employee);
        Task<List<Empleado>> GetAllPersonnel();
        Task<Empleado> GetPersonnel(int codigo);
        Task<bool> UpdatePersonnel(UpdatePersonnelDTO personnel, int legajo);
        Task<bool> DeletePersonnel(int legajo);
        Task<bool> ValidatePersonnelLogin(string email, string password);
        Task<Empleado> GetByEmail(string email);
    }
}
