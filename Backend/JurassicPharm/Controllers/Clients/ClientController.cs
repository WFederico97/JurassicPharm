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
        [HttpGet("Client")]
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

        [HttpPost("Client")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO client)
        {
            if (client.IdHealthPlan <= 0 || client.IdCity <= 0)
            {
                return BadRequest("Invalid Health Plan or City");
            }
            try
            {
                bool isCreated = await _clientService.CreateClient(client);
                if (isCreated)
                {
                    return Ok("Client created successfully");
                }
                return BadRequest("The client was not created");
            }
            catch (Exception error)
            {
                return StatusCode(500, $"An unexpected error occurred while creating Client. Error: {error.Message}"
                );
            }
        }
        [HttpDelete("Client/{idClient}")]
        public async Task<IActionResult> Delete([FromRoute] int idClient)
        {
            try
            {
                bool isDeleted = await _clientService.DeleteClient(idClient);

                if (isDeleted)
                {
                    return Ok("Invoice deleted successfully");
                }

                return BadRequest("The invoice was not deleted");
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
        public async Task<IActionResult> Update([FromBody] UpdateClientDTO client, [FromRoute] int idClient)
        {
            try
            {
                bool isUpdated = await _clientService.UpdateClient(client, idClient);

                if (isUpdated)
                {
                    return Ok("Invoice updated successfully");
                }

                return BadRequest("The invoice was not updated");
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
