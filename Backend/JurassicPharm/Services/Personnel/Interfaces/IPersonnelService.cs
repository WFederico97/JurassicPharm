using JurassicPharm.Models;
using JurassicPharm.Models.DTOs.Personnel;

namespace JurassicPharm.Services.Personnel.Interfaces
{
    public interface IPersonnelService
    {
        List<PersonnelDTO> GetAllPersonnel();
        Empleado GetPersonnel(int codigo);
        void UpdatePersonnel(Empleado empleado);
        void DeletePersonnel(Empleado empleado);
    }
}
