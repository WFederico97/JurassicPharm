using JurassicPharm.DTO.Clients;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Clients.Implementations;
using JurassicPharm.Repositories.Clients.Interfaces;
using JurassicPharm.Services.Clients.Interfaces;

namespace JurassicPharm.Services.Clients.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public Task<bool> CreateClient(CreateClientDTO client)
        {
            return _clientRepository.CreateClient(client);
        }

        public Task<bool> DeleteClient(int idCliente)
        {
            return _clientRepository.DeleteClient(idCliente);
        }

        public Task<List<ClientResponseDTO>> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public Task<Cliente> GetClientById(int idCliente)
        {
            return _clientRepository.GetClienteById(idCliente);
        }

        public Task<bool> UpdateClient(CreateClientDTO client, int idCliente)
        {
            return _clientRepository.UpdateClient(client, idCliente);
        }
    }
}
