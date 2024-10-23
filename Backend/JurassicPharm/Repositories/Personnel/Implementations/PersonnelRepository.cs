using JurassicPharm.Models;
using JurassicPharm.Repositories.Personnel.Interfaces;

namespace JurassicPharm.Repositories.Personnel.Implementations
{
    public class PersonnelRepository: IPersonnelRepository
    {
        private readonly jurassic_pharmContext _context;

        public PersonnelRepository(jurassic_pharmContext context)
        {
            _context = context;
        }

        public List<Empleado> GetAllPersonnel()
        {
            return _context.Empleados.ToList();
        }
        public Empleado GetPersonnel(int codigo)
        {
            return _context.Empleados.Where(p => p.LegajoEmpleado == codigo).FirstOrDefault();
        }
        public void UpdatePersonnel(Empleado empleado)
        {
            _context.Update(empleado);
        }
        public void DeletePersonnel(Empleado empleado)
        {
            _context.Update(empleado);
        }
    }
}
