using JurassicPharm.DTO.Clients;
using JurassicPharm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JurassicPharm.Repositories.Clients.Interfaces
{
    public interface IClientRepository
    {
        Task<bool> CreateClient(CreateClientDTO client);
        Task<bool> DeleteClient(int idCliente);
        Task<Cliente> GetClienteById(int idCliente);
        Task<bool> UpdateClient(UpdateClientDTO client, int idCliente);
        Task<List<ClientResponseDTO>> GetAllClients();
    }
}