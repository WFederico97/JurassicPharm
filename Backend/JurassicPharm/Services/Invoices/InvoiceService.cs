using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Invoices;

namespace JurassicPharm.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public Task<bool> Create(InvoiceCreateDTO invoice)
        {
            if (!ValidateDate(invoice))
            {
                throw new Exception("The invoice cannot be more than 2 days later");
            }

            return _invoiceRepository.Create(invoice);
        }

        public Task<bool> Delete(int invoiceId)
        {
            return _invoiceRepository.Delete(invoiceId);
        }

        public async Task<List<InvoiceResponseDTO>> GetAll()
        {
            return await _invoiceRepository.GetAll();
        }

        public Task<bool> Update(InvoiceUpdateDTO invoice, int invoiceId)
        {
            if (!ValidateDate(invoice))
            {
                throw new Exception("The invoice cannot be more than 2 days later");
            }

            return _invoiceRepository.Update(invoice, invoiceId);
        }

        private bool ValidateDate(InvoiceUpdateDTO invoice)
        {
            int daysDifference = DateTime.Now.Day - invoice.Date.Day;

            return daysDifference <= 2;
        }
    }
}