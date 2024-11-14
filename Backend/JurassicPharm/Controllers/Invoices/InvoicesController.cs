using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Models;
using JurassicPharm.Services.Invoices;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("checkPrescriptionDate/{clientId}")]
        public async Task<IActionResult> CheckPrescriptionDate(int clientId)
        {
            var result = await _invoiceService.CheckProlongedPrescriptionDate(clientId);
            return Ok(result);
        }

        [HttpGet("billingReport")]
        public async Task<IActionResult> GetBillingReport()
        {
            var report = await _invoiceService.GetBillingReportBySupplyType();
            return Ok(report);
        }

        [HttpGet("discount/{obraSocialId}/{invoiceNumber}")]
        public async Task<IActionResult> GetDiscount(int obraSocialId, int invoiceNumber)
        {
            var discount = await _invoiceService.GetDiscountByInsurance(obraSocialId, invoiceNumber);
            return Ok(discount);
        }

        [HttpGet("topSuppliers")]
        public async Task<IActionResult> GetTopSuppliers()
        {
            var suppliers = await _invoiceService.GetTopSuppliersByDeliveries();
            return Ok(suppliers);
        }

        [HttpGet("GetInvoices")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _invoiceService.GetAll());

            }
            catch (Exception error)
            {
                return StatusCode(
                    500,
                    $"An unexpected error occurred while get all invoices. Error: {error.Message}"
                );
            }
        }

        [Authorize("AdminOrCashier")]
        [HttpPost("NewInvoice")]
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

        [Authorize("AdminOrCashier")]
        [HttpDelete("DeleteInvoice/{invoiceId}")]
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
        [Authorize("AdminOrCashier")]
        [HttpPut("UpdateInvoice/{invoiceId}")]
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
