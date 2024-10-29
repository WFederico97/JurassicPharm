using JurassicPharm.DTO.Personnel;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Personnel.Interfaces
{
    public interface IPersonnelService
    {
        Task<bool> CreateEmployee(CreatePersonnelDTO employee);
        Task<List<Empleado>> GetAllPersonnel();
        Task<Empleado> GetPersonnel(int codigo);
        Task<bool> UpdatePersonnel(UpdatePersonnelDTO personnel, int legajo);
        Task<bool> DeletePersonnel(int legajo);
    }
}
