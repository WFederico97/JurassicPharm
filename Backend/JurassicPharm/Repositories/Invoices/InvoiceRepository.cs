using JurassicPharm.DTO.Branch;
using JurassicPharm.DTO.Invoice;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.DTO.InvoiceDetail;
using JurassicPharm.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

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
            Cliente? client = await _context.Clientes.FirstOrDefaultAsync(client => client.IdCliente == invoice.ClientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            Suministro? branch = await _context.Suministros.FirstOrDefaultAsync(branch => branch.IdSuministro == invoice.BranchId);

            if (branch == null)
            {
                throw new Exception("Branch not found");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            Factura invoiceToCreate = new Factura()
            {
                IdCliente = invoice.ClientId,
                IdSucursal = invoice.BranchId,
                Fecha = invoice.Date
            };

            try
            {
                _context.Facturas.Add(invoiceToCreate);
                await _context.SaveChangesAsync();

                int invoiceId = invoiceToCreate.NroFactura;

                foreach (var detail in invoice.Details)
                {
                    Suministro? supply = await _context.Suministros.FirstOrDefaultAsync(s => s.IdSuministro == detail.SupplyId);

                    if (supply == null)
                    {
                        throw new Exception("Supply not found");
                    }

                    DetalleFactura detailToCreate = new DetalleFactura()
                    {
                        NroFactura = invoiceId,
                        IdSuministro = detail.SupplyId,
                        PreVenta = detail.SalePrice,
                        Cantidad = detail.Amount
                    };

                    _context.DetallesFactura.Add(detailToCreate);
                }

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

        public async Task<Factura?> GetInvoceById(int invoiceId)
        {
            return await _context.Facturas.FirstOrDefaultAsync(invoice => invoice.NroFactura == invoiceId);
        }

        public async Task<List<InvoiceResponseDTO>> GetAll()

        {
            return await _context.Facturas
                .Include(invoice => invoice.DetallesFactura)
                .Include(invoice => invoice.IdClienteNavigation)
                .Include(invoice => invoice.IdSucursalNavigation)
                .Select(invoice => new InvoiceResponseDTO
                {
                    InvoiceNumber = (int)invoice.NroFactura,
                    ClientId = (int)invoice.IdCliente,
                    ClientName = invoice.IdClienteNavigation.Nombre,
                    ClienLastName = invoice.IdClienteNavigation.Apellido,
                    Branch = new BranchDTO() { Id = invoice.IdSucursalNavigation.IdSucursal, Address = $"{invoice.IdSucursalNavigation.Calle}, {invoice.IdSucursalNavigation.Altura}" },
                    Date = (DateOnly)invoice.Fecha,
                    Details = invoice.DetallesFactura.Select(detail => new InvoiceDetailResponseDTO
                    {
                        SupplyName = detail.IdSuministroNavigation.Nombre,
                        SalePrice = (int)detail.PreVenta,
                        Amount = (int)detail.Cantidad
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<string> CheckProlongedPrescriptionDate(int clientId)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SP_CONTROLAR_RECETA_PROLONGADA";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ID_CLIENTE", clientId));
                command.Parameters.Add(new SqlParameter("@FECHA_ACTUAL", DateTime.Now));

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                var result = new StringBuilder();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Append(reader.GetString(0));
                    }
                }

                return result.ToString();
            }
        }

        public async Task<List<ViewFacturacionPorAnio>> GetBillingReportBySupplyType()
        {
            List<ViewFacturacionPorAnio> list = new List<ViewFacturacionPorAnio>();

            var bilingReports = await _context.ViewFacturacionPorAnio.ToListAsync();

            bilingReports.ForEach(report =>
            {
                list.Add(new ViewFacturacionPorAnio()
                {
                    Supply = report.Supply,
                    Year = report.Year,
                    Total = report.Total
                });
            });
            return bilingReports;
        }

        public async Task<decimal> GetDiscountByInsurance(int obraSocialId, int invoiceNumber)
        {
            decimal discountTotal = 0;

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT dbo.FX_CALCULAR_DESCUENTO(@obraSocialId, @invoiceNumber)";
                    command.Parameters.Add(new SqlParameter("@obraSocialId", obraSocialId));
                    command.Parameters.Add(new SqlParameter("@invoiceNumber", invoiceNumber));

                    var result = await command.ExecuteScalarAsync();
                    if (result != DBNull.Value && result != null)
                    {
                        discountTotal = Convert.ToDecimal(result);
                    }
                }
            }

            return discountTotal;
        }

        public async Task<List<TopSuppliersDTO>> GetTopSuppliersByDeliveries()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SP_TOP_PROVEEDORES_ENTREGAS";
                command.CommandType = CommandType.StoredProcedure;

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                var suppliers = new List<TopSuppliersDTO>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        suppliers.Add(new TopSuppliersDTO
                        {
                            CompanyName = reader.GetString(0),
                            TotalDelivered = reader.GetInt32(1)
                        });
                    }
                }

                return suppliers;
            }
        }
    }
}


