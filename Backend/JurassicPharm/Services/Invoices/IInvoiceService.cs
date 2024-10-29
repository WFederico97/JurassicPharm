using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Invoices
{
    public interface IInvoiceService
    {
        public Task<List<InvoiceResponseDTO>> GetAll();
        public Task<bool> Create(InvoiceCreateDTO invoice);

        public Task<bool> Update(InvoiceUpdateDTO invoice, int invoiceId);

        public Task<bool> Delete(int invoiceId);
    }
}