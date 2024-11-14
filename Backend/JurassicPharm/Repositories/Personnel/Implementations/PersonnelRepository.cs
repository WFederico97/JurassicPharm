using JurassicPharm.DTO.Personnel;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Exceptions;
using JurassicPharm.Repositories.Personnel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;
using JurassicPharm.DTO.Cities;
using JurassicPharm.DTO.Stores;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;


namespace JurassicPharm.Repositories.Personnel.Implementations
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly JurassicPharmContext _context;

        public PersonnelRepository(JurassicPharmContext context)
        {
            _context = context;
        }

        public async Task<List<GetStoreDTO>> GetStores()
        {
            var sucursales = await _context.Sucursales
                .Include(s => s.Empleados)
                .Include(s => s.IdCiudadNavigation)
                    .ThenInclude(l => l.IdLocalidadNavigation)
                        .ThenInclude(p => p.IdProvinciaNavigation)
                .Select(s => new GetStoreDTO
                {
                    IdSucursal = s.IdSucursal,
                    Calle = s.Calle,
                    Altura = (int)s.Altura,
                    Ciudad = s.IdCiudadNavigation.Nombre,
                    Localidad = s.IdCiudadNavigation.IdLocalidadNavigation.Nombre,
                    Provincia = s.IdCiudadNavigation.IdLocalidadNavigation.IdProvinciaNavigation.Nombre,
                    Empleados = s.Empleados.Select(e => new GetPersonnelSummaryDTO
                    {
                        LegajoEmpleado = e.LegajoEmpleado,
                        Nombre = e.Nombre,
                        Apellido = e.Apellido,
                        Role = e.Rol,
                        Active = (bool)e.Active
                    }).ToList()
                }).ToListAsync();

            return sucursales;
        }

        public async Task<List<GetCitySummaryDTO>> GetCities()
        {
            var ciudades = await _context.Ciudades
                .Include(c => c.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .Include(c => c.Empleados)
                .Select(c => new GetCitySummaryDTO
                {
                    IdCiudad = c.IdCiudad,
                    Nombre = c.Nombre,
                    Localidad = c.IdLocalidadNavigation.Nombre,
                    Provincia = c.IdLocalidadNavigation.IdProvinciaNavigation.Nombre,
                    Sucursales = c.Sucursales.Select(s => new GetStoreDTO
                    {
                        IdSucursal = s.IdSucursal,
                        Calle = s.Calle,
                        Altura = (int)s.Altura,

                    }).ToList(),
                    Empleados = c.Empleados.Select(e => new GetPersonnelSummaryDTO
                    {
                        LegajoEmpleado = e.LegajoEmpleado,
                        Nombre = e.Nombre,
                        Apellido = e.Apellido,
                        Active = (bool)e.Active
                    }).ToList()
                }).ToListAsync();

            return ciudades;
        }

        public async Task<List<GetPersonnelDTO>> GetAllPersonnel()
        {
            var empleados = await _context.Empleados
                .Include(e => e.IdCiudadNavigation)
                    .ThenInclude(c => c.IdLocalidadNavigation)
                        .ThenInclude(l => l.IdProvinciaNavigation)
                .Include(e => e.IdSucursalNavigation)
                .Where(p => p.Active == true)
                .Select(e => new GetPersonnelDTO
                {
                    LegajoEmpleado = e.LegajoEmpleado,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    CorreoElectronico = e.CorreoElectronico,
                    Domicilio = $"{e.Calle} {e.Altura}",
                    Rol = e.Rol,
                    Sucursal = (int)e.IdSucursal,
                    Ciudad = e.IdCiudadNavigation.Nombre,
                    Localidad = e.IdCiudadNavigation.IdLocalidadNavigation.Nombre,
                    Provincia = e.IdCiudadNavigation.IdLocalidadNavigation.IdProvinciaNavigation.Nombre,
                    DireccionSucursal = $"{e.IdSucursalNavigation.Calle} {e.IdSucursalNavigation.Altura}"
                }).ToListAsync();

            return empleados;
        }

        public async Task<Empleado> GetPersonnel(int legajo)
        {
            Empleado employee = await _context.Empleados
                .Include(e => e.IdCiudadNavigation)
                .Include(e => e.IdSucursalNavigation)
                .Where(p => p.Active == true && p.LegajoEmpleado == legajo)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new NotFoundException($"El empleado con legajo {legajo} no existe en nuestros registros");
            }

            return employee;
        }

        public async Task<bool> UpdatePersonnel(UpdatePersonnelDTO personnel, int legajo)
        {
            Empleado personnelToUpdate = await _context.Empleados.Where(p => p.LegajoEmpleado == legajo & p.Active == true).FirstOrDefaultAsync();

            if (personnelToUpdate == null)
            {
                throw new NotFoundException($"No hay registros para el empleado con legajo: {legajo}");
            }

            if (!string.IsNullOrEmpty(personnel.Nombre) && personnel.Nombre != personnelToUpdate.Nombre)
            {
                personnelToUpdate.Nombre = personnel.Nombre;
            }

            if (!string.IsNullOrEmpty(personnel.Apellido) && personnel.Apellido != personnelToUpdate.Apellido)
            {
                personnelToUpdate.Apellido = personnel.Apellido;
            }

            if (!string.IsNullOrEmpty(personnel.Calle) && personnel.Calle != personnelToUpdate.Calle)
            {
                personnelToUpdate.Calle = personnel.Calle;
            }

            if (personnel.Altura.HasValue && personnel.Altura != personnelToUpdate.Altura)
            {
                personnelToUpdate.Altura = personnel.Altura.Value;
            }

            if (!string.IsNullOrEmpty(personnel.CorreoElectronico) && personnel.CorreoElectronico != personnelToUpdate.CorreoElectronico)
            {
                personnelToUpdate.CorreoElectronico = personnel.CorreoElectronico;
            }
            if (!string.IsNullOrEmpty(personnel.Rol) && personnel.Rol != personnelToUpdate.Rol)
            {
                personnelToUpdate.Rol = personnel.Rol;
            }


            _context.Empleados.Update(personnelToUpdate);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePersonnel(int legajo)
        {
            Empleado personnelToDelete = await _context.Empleados.Where(p => p.LegajoEmpleado == legajo).FirstOrDefaultAsync();

            if (personnelToDelete == null)
            {
                throw new NotFoundException($"No se encontró el empleado con legajo: {legajo}");
            }

            personnelToDelete.Active = false;

            _context.Empleados.Update(personnelToDelete);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateEmployee(CreatePersonnelDTO employee)
        {
            if (!IsValidEmail(employee.CorreoElectronico))
            {
                throw new InvalidEmailException($"El correo electrónico {employee.CorreoElectronico} no tiene un formato válido.");
            }

            Sucursal sucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.IdSucursal == employee.IdSucursal);
            if (sucursal == null)
            {
                throw new NotFoundException($"La sucursal {employee.IdSucursal} no existe en nuestros registros");
            }

            Ciudad ciudad = await _context.Ciudades.FirstOrDefaultAsync(c => c.IdCiudad == employee.IdCiudad);
            if (ciudad == null)
            {
                throw new NotFoundException($"La ciudad {employee.IdCiudad} no existe en nuestros registros");
            }
            if (String.IsNullOrEmpty(employee.PasswordEmpleado))
            {
                throw new InvalidPropertyException($"Debe ingresar una contraseña");
            }

            //Hasheo de password 
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(employee.PasswordEmpleado, 13);

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Empleado newEmployee = new Empleado()
                {
                    Apellido = employee.Apellido,
                    Nombre = employee.Nombre,
                    Calle = employee.Calle,
                    Altura = employee.Altura,
                    CorreoElectronico = employee.CorreoElectronico,
                    PasswordEmpleado = hashedPassword,
                    Rol = employee.Rol,
                    IdCiudad = employee.IdCiudad,
                    IdSucursal = employee.IdSucursal,
                    Active = true
                };

                await _context.Empleados.AddAsync(newEmployee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error al crear empleado: {ex.Message}");
            }
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var mailAddress = new MailAddress(email);
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public async Task<bool> ValidatePersonnelLogin(string email, string password)
        {
            Empleado employee = await _context.Empleados
                .FirstOrDefaultAsync(e => e.CorreoElectronico == email & e.Active == true);

            if (employee == null)
            {
                return false;
            }
            bool passwordReveal = BCrypt.Net.BCrypt.EnhancedVerify(password, employee.PasswordEmpleado);
            if (!passwordReveal)
            {
                throw new InvalidPropertyException("Credenciales inválidas");
            }
            return true;
        }

        public async Task<Empleado> GetByEmail(string email)
        {
            return await _context.Empleados
                .Include(e => e.IdCiudadNavigation)
                .Include(e => e.IdSucursalNavigation)
                .FirstOrDefaultAsync(e => e.CorreoElectronico == email & e.Active == true);
        }

        public async Task<string> CheckProlongedPrescriptionDate(int clientId)

        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SP_CONTROLAR_RECETA_PROLONGADA";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ID_CLIENTE", clientId));
                command.Parameters.Add(new SqlParameter("@FECHA_ACTUAL", DateTime.Now));

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                var result = new StringBuilder();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Append(reader.GetString(0));
                    }
                }

                return result.ToString();
            }
        }

    }

}
