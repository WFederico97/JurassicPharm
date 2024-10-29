using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Models;

namespace JurassicPharm.Repositories.Invoices
{
    public interface IInvoiceRepository
    {
        public Task<List<InvoiceResponseDTO>> GetAll();
        public Task<Factura?> GetInvoceById(int invoiceId);

        public Task<bool> Create(InvoiceCreateDTO invoice);

        public Task<bool> Update(InvoiceUpdateDTO invoice, int invoiceId);

        public Task<bool> Delete(int invoiceId);

    }
}