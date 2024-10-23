using JurassicPharm.Models;

namespace JurassicPharm.Repositories.Personnel.Interfaces
{
    public interface IPersonnelRepository
    {
        List<Empleado> GetAllPersonnel();
        Empleado GetPersonnel(int codigo);
        void UpdatePersonnel(Empleado empleado);
        void DeletePersonnel(Empleado empleado);
    }
}
