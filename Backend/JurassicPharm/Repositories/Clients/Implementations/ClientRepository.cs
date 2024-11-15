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
                var healthPlanExists = await _context.ObrasSociales
                                        .AnyAsync(healthPlan => healthPlan.IdObraSocial == client.IdHealthPlan);
                if (!healthPlanExists)
                {
                    throw new Exception("Obra social no existe");
                }
                var cityExists = await _context.Ciudades
                                    .AnyAsync(city => city.IdCiudad == client.IdCity);
                if (!cityExists)
                {
                    throw new Exception("Ciudad no existe");
                }
                Cliente cliente = new Cliente
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

        public async Task<bool> UpdateClient(UpdateClientDTO client, int idCliente)
        {
            Cliente clientToUpdate = await _context.Clientes.Where(client => client.IdCliente == idCliente).FirstOrDefaultAsync();
            if(clientToUpdate == null)
            {
                throw new Exception("Client Not Found");
            }
            if(!string.IsNullOrEmpty(client.Nombre) && client.Nombre != clientToUpdate.Nombre)
            {
                clientToUpdate.Nombre = client.Nombre;
            }
            if (!string.IsNullOrEmpty(client.Apellido) && client.Apellido!= clientToUpdate.Apellido)
            {
                clientToUpdate.Apellido = client.Apellido;
            }
            if (!string.IsNullOrEmpty(client.Calle) && client.Calle != clientToUpdate.Calle)
            {
                clientToUpdate.Calle = client.Calle;
            }
            if (!string.IsNullOrEmpty(client.CorreoElectronico) && client.CorreoElectronico != clientToUpdate.CorreoElectronico)
            {
                clientToUpdate.CorreoElectronico= client.CorreoElectronico;
            }
            if (client.Altura.HasValue && client.Altura != clientToUpdate.Altura)
            { 
            clientToUpdate.Altura = client.Altura;
            }
            if(client.IdCiudad.HasValue && client.IdCiudad != clientToUpdate.IdCiudad)
            {
                clientToUpdate.IdCiudad = client.IdCiudad;
            }
            if(client.IdObraSocial.HasValue && client.IdObraSocial != clientToUpdate.IdObraSocial)
            {
                clientToUpdate.IdObraSocial = client.IdObraSocial;
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
                    IdClient= c.IdCliente,
                    Name = c.Nombre,
                    Lastname = c.Apellido,
                    Email = c.CorreoElectronico,
                    Street = c.Calle,
                    Number= c.Altura,
                    City = c.IdCiudadNavigation.Nombre,
                    State = c.IdCiudadNavigation.IdLocalidadNavigation.IdProvinciaNavigation.Nombre,
                    HealthPlan = c.IdObraSocialNavigation.Nombre,
                }).ToListAsync();
            return clientes;
        }
    }
}
