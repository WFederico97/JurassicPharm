using JurassicPharm.DTO.Clients;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Services.Clients.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Clients
{
    [ApiController]
    [Route("api/")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet("GetClients")]
        public async Task<IActionResult> GetAllCLient()
        {
            try
            {
                return Ok(await _clientService.GetAllClients());

            }
            catch (Exception error)
            {
                return StatusCode(
                    500,
                    $"An unexpected error occurred while get all Clients. Error: {error.Message}"
                );
            }
        }

        [HttpPost("NewClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO client)
        {
            if (client.IdHealthPlan <= 0 || client.IdCity <= 0)
            {
                return BadRequest("Obra Social o Ciudad incorrecta");
            }
            try
            {
                bool isCreated = await _clientService.CreateClient(client);
                if (isCreated)
                {
                    return Ok("Client Creado con Exito");
                }
                return BadRequest("El cliente no pudo ser creado");
            }
            catch (Exception error)
            {
                return StatusCode(500, $"An unexpected error occurred while creating Client. Error: {error.Message}"
                );
            }
        }
        [HttpDelete("DeleteClient/{idClient}")]
        public async Task<IActionResult> Delete([FromRoute] int idClient)
        {
            try
            {
                bool isDeleted = await _clientService.DeleteClient(idClient);

                if (isDeleted)
                {
                    return Ok("Cliente Eliminado Correctamente");
                }

                return BadRequest("El Cliente no fue eliminado");
            }
            catch (Exception error)
            {
                return StatusCode(
                    500,
                    $"An unexpected error occurred while deleting invoice. Error: {error.Message}"
                );
            }
        }
        [HttpPut("Client/{idClient}")]
        public async Task<IActionResult> Update([FromBody] CreateClientDTO client, [FromRoute] int idClient)
        {
            try
            {
                bool isUpdated = await _clientService.UpdateClient(client, idClient);

                if (isUpdated)
                {
                    return Ok("Cliente actualizado correctamente");
                }

                return BadRequest("El cliente no pudo ser actualizado");
            }
            catch (Exception error)
            {
                return StatusCode(
                    500,
                    $"An unexpected error occurred while updating invoice. Error: {error.Message}"
                );
            }
        }


    }
}
