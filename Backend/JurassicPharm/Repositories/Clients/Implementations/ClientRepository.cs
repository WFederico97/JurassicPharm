using JurassicPharm.DTO.Clients;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Clients.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Repositories.Clients.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly JurassicPharmContext _context;
        public ClientRepository(JurassicPharmContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateClient(CreateClientDTO client)
        {
            try
            {
                var HealthPlan = await _context.ObrasSociales.Where(healthPlan => healthPlan.IdObraSocial == client.IdHealthPlan).ToListAsync();
                if (HealthPlan == null)
                {
                    throw new Exception("Obra social no existe");
                }
                var City = await _context.Ciudades.Where(city => city.IdCiudad == client.IdCity).ToListAsync();
                if (City == null)
                {
                    throw new Exception("Ciudad no existe");
                }
                Cliente cliente = new Cliente()
                {
                    IdObraSocial = client.IdHealthPlan,
                    IdCiudad = client.IdCity,
                    Nombre = client.Name,
                    Apellido = client.Lastname,
                    CorreoElectronico = client.Email,
                    Calle = client.Street,
                    Altura = client.Number
                };

                await _context.Clientes.AddAsync(cliente);

                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task<Cliente?> GetClienteById(int idCliente)
        {
            return await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.IdCliente == idCliente);
        }

        public async Task<bool> DeleteClient(int idCliente)
        {
            Cliente clientToDelete = await _context.Clientes.Where(cliente => cliente.IdCliente == idCliente).FirstOrDefaultAsync();
            if (clientToDelete == null)
            {
                throw new Exception("Client Not Found");
            }
            _context.Clientes.Remove(clientToDelete);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateClient(CreateClientDTO client, int idCliente)
        {
            Cliente clientToUpdate = await _context.Clientes.Where(client => client.IdCliente == idCliente).FirstOrDefaultAsync();
            if (clientToUpdate == null)
            {
                throw new Exception("Client Not Found");
            }
            if (!string.IsNullOrEmpty(client.Name) && client.Name != clientToUpdate.Nombre)
            {
                clientToUpdate.Nombre = client.Name;
            }
            if (!string.IsNullOrEmpty(client.Lastname) && client.Lastname != clientToUpdate.Apellido)
            {
                clientToUpdate.Apellido = client.Lastname;
            }
            if (!string.IsNullOrEmpty(client.Street) && client.Street != clientToUpdate.Calle)
            {
                clientToUpdate.Calle = client.Street;
            }
            if (!string.IsNullOrEmpty(client.Email) && client.Email != clientToUpdate.CorreoElectronico)
            {
                clientToUpdate.CorreoElectronico = client.Email;
            }
            if (client.Number.HasValue && client.Number != clientToUpdate.Altura)
            {
                clientToUpdate.Altura = client.Number;
            }
            if (client.IdCity.HasValue && client.IdCity != clientToUpdate.IdCiudad)
            {
                clientToUpdate.IdCiudad = client.IdCity;
            }
            if (client.IdHealthPlan.HasValue && client.IdHealthPlan != clientToUpdate.IdObraSocial)
            {
                clientToUpdate.IdObraSocial = client.IdHealthPlan;
            }
            _context.Clientes.Update(clientToUpdate);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<ClientResponseDTO>> GetAllClients()
        {
            var clientes = await _context.Clientes
                .Include(c => c.IdCiudadNavigation)
                    .ThenInclude(l => l.IdLocalidadNavigation)
                        .ThenInclude(p => p.IdProvinciaNavigation)
                .Include(c => c.IdObraSocialNavigation)
                .Select(c => new ClientResponseDTO
                {
                    IdClient = c.IdCliente,
                    Name = c.Nombre,
                    Lastname = c.Apellido,
                    Email = c.CorreoElectronico,
                    Street = c.Calle,
                    Number = c.Altura,
                    City = c.IdCiudadNavigation.Nombre,
                    State = c.IdCiudadNavigation.IdLocalidadNavigation.IdProvinciaNavigation.Nombre,
                    HealthPlan = c.IdObraSocialNavigation.Nombre,
                }).ToListAsync();
            return clientes;
        }
    }
}
