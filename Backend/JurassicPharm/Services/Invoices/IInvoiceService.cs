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
        public Task<string> CheckProlongedPrescriptionDate(int clientId);
        public Task<List<BillingReportDTO>> GetBillingReportBySupplyType();
        public Task<decimal> GetDiscountByInsurance(int obraSocialId, int invoiceNumber);
        public Task<List<TopSuppliersDTO>> GetTopSuppliersByDeliveries();


    }
}