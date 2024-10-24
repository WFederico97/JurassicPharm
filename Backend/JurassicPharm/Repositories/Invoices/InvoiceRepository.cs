using System.Linq.Expressions;
using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.Models;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Repositories.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly JurassicPharmContext _context;

        public InvoiceRepository(JurassicPharmContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(InvoiceCreateDTO invoice)
        {
            Clientes? client = await _context.Clientes.FirstOrDefaultAsync(client => client.IdCliente == invoice.ClientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            Suministros? branch = await _context.Suministros.FirstOrDefaultAsync(branch => branch.IdSuministro == invoice.BranchId);

            if (branch == null)
            {
                throw new Exception("Branch not found");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            Facturas invoiceToCreate = new Facturas()
            {
                IdCliente = invoice.ClientId,
                IdSucursal = invoice.BranchId,
                Fecha = invoice.Date
            };

            try
            {
                await _context.Facturas.AddAsync(invoiceToCreate);

                //Auto-generated id
                int invoiceId = invoiceToCreate.NroFactura;

                invoice.Details.ForEach(async detail =>
                {
                    Suministros? supply = await _context.Suministros.FirstOrDefaultAsync(s => s.IdSuministro == detail.SupplyId);

                    if (supply == null)
                    {
                        throw new Exception("Supply not found");
                    }

                    DetallesFactura detailToCreate = new DetallesFactura()
                    {
                        NroFactura = invoiceId,
                        IdSuministro = detail.SupplyId,
                        PreVenta = detail.SalePrice,
                        Cantidad = detail.Amount
                    };

                    await _context.DetallesFactura.AddAsync(detailToCreate);
                });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;

            }
            catch (Exception error)
            {
                await transaction.RollbackAsync();
                throw new Exception(error.Message);

            }
        }

        public async Task<bool> Delete(int invoiceId)
        {
            var invoicesToDelete = await GetInvoceById(invoiceId);

            if (invoicesToDelete == null)
            {
                throw new Exception("Invoce not found");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {   //First, bulk delete all details from invoice
                _context.DetallesFactura.Where(detail => detail.NroFactura == invoiceId).ExecuteDelete();

                //Then remove invoice
                _context.Facturas.Remove(invoicesToDelete);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<Facturas?> GetInvoceById(int invoiceId)
        {
            return await _context.Facturas.FirstOrDefaultAsync(invoice => invoice.NroFactura == invoiceId);
        }

        public async Task<bool> Update(InvoiceUpdateDTO invoice, int invoiceId)
        {
            bool result = false;

            var invoiceToUpdate = await _context.Facturas.FindAsync(invoiceId);

            if (invoiceToUpdate == null)
            {
                throw new Exception("Invoice not found");
            }

            invoiceToUpdate.IdCliente = invoice.ClientId;
            invoiceToUpdate.IdSucursal = invoice.BranchId;
            invoiceToUpdate.Fecha = invoice.Date;

            _context.Facturas.Update(invoiceToUpdate);

            result = await _context.SaveChangesAsync() == 1;

            return result;
        }
    }
}