using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Models;
using JurassicPharm.Services.Invoices;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Invoices
{
    [ApiController]
    [Route("api/")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }


        [HttpPost("invoice")]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateDTO invoice)
        {
            if (invoice.ClientId <= 0 || invoice.BranchId <= 0)
            {
                return BadRequest("Invalid client or branch");
            }

            try
            {
                bool isCreated = await _invoiceService.Create(invoice);

                if (isCreated)
                {
                    return Ok("Invoice creade successfully");
                }

                return BadRequest("The invoice was not created");
            }
            catch (Exception error)
            {
                return StatusCode(
                    500,
                    $"An unexpected error occurred while creating invoice. Error: {error.Message}"
                );
            }

        }

        [HttpDelete("invoice/{invoiceId}")]
        public async Task<IActionResult> Delete([FromRoute] int invoiceId)
        {
            try
            {
                bool isDeleted = await _invoiceService.Delete(invoiceId);

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

        [HttpPut("invoice/{invoiceId}")]
        public async Task<IActionResult> Update([FromBody] InvoiceUpdateDTO invoice, [FromRoute] int invoiceId)
        {
            try
            {
                bool isUpdated = await _invoiceService.Update(invoice, invoiceId);

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
