using JurassicPharm.Models;
using JurassicPharm.Models.DTOs.Personnel;
using JurassicPharm.Repositories.Personnel.Implementations;
using JurassicPharm.Repositories.Personnel.Interfaces;
using JurassicPharm.Services.Personnel.Interfaces;

namespace JurassicPharm.Services.Personnel.Implementations
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _repository;

        public PersonnelService(IPersonnelRepository repository)
        {
            _repository = repository;
        }

        public List<PersonnelDTO> GetAllPersonnel()
        {
            List<Empleado> personnel = _repository.GetAllPersonnel();
            return personnel.Select(p => new PersonnelDTO
            {
                Apellido = p.Apellido,
                Nombre = p.Nombre,
                LegajoEmpleado = p.LegajoEmpleado,
                CorreoElectronico = p.CorreoElectronico,
                IdSucursal = (Int32)p.IdSucursal

            }).ToList();
            
        }

        public Empleado GetPersonnel(int codigo)
        {
            return _repository.GetPersonnel(codigo);
        }

        public void UpdatePersonnel(Empleado empleado)
        {
            _repository.UpdatePersonnel(empleado);
        }

        public void DeletePersonnel(Empleado empleado)
        {
            _repository.DeletePersonnel(empleado);
        }
    }
}
