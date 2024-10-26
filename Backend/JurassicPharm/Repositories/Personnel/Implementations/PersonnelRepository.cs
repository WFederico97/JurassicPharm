﻿using JurassicPharm.DTOs.Personnel;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Exceptions;
using JurassicPharm.Repositories.Personnel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace JurassicPharm.Repositories.Personnel.Implementations
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly JurassicPharmContext _context;

        public PersonnelRepository(JurassicPharmContext context)
        {
            _context = context;
        }

        public async Task<List<Empleado>> GetAllPersonnel()
        {
            return await _context.Empleados.Where(p => p.Active == true).ToListAsync();
        }

        public async Task<Empleado> GetPersonnel(int legajo)
        {
            Empleado employee = await _context.Empleados
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
            Empleado personnelToUpdate = await _context.Empleados.Where(p => p.LegajoEmpleado == legajo).FirstOrDefaultAsync();

            if (personnelToUpdate == null)
            {
                throw new NotFoundException($"No hay registros para el empleado con legajo: {legajo}");
            }

            // Actualizar solo los campos que han sido modificados
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

            _context.Empleados.Update(personnelToUpdate);

            return await _context.SaveChangesAsync() > 0; // Verificar si algo fue modificado
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

            return await _context.SaveChangesAsync() > 0; // Verificar si algo fue modificado
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
    }

}