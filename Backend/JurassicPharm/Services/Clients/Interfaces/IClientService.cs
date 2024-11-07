using JurassicPharm.DTO.Clients;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Clients.Interfaces
{
    public interface IClientService
    {
        Task<bool> CreateClient(CreateClientDTO client);
        Task<bool> DeleteClient(int idCliente);
        Task<Cliente> GetClientById(int idCliente);
        Task<bool> UpdateClient(UpdateClientDTO client, int idCliente);
        Task<List<ClientResponseDTO>> GetAllClients();
    }
}
