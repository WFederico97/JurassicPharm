using JurassicPharm.DTO.Personnel;
using JurassicPharm.DTO.Stores;
using JurassicPharm.DTO.Supplies;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Exceptions;
using JurassicPharm.Repositories.Supplies.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace JurassicPharm.Repositories.Supplies.implementations
{
    public class SuppliesRepository : IsuppliesRepository
    {
        private readonly JurassicPharmContext _context;

        public SuppliesRepository(JurassicPharmContext context)
        {
            _context = context;
        }

        public async Task<List<GetSupplyDTO>> GetAllSupply()
        {
            var suministros = await _context.Suministros
                .Include(s => s.IdMarcaNavigation)
                .Include(s => s.IdTipoDistribucionNavigation)
                .Include(s => s.IdTipoSuministroNavigation)
                .Select(s => new GetSupplyDTO
                {
                    IdSupply = s.IdSuministro,
                    Name = s.Nombre,
                    Brand = s.IdMarcaNavigation.Nombre,
                    Distribution = s.IdTipoDistribucionNavigation.Descripcion,
                    SupplyType = s.IdTipoSuministroNavigation.Nombre,
                    Price = s.PreUnitario,
                    Stock = s.Stock,
                    MinimumStock = s.StockMinimo
                }).ToListAsync();

            return suministros;
        }

        public async Task<bool> CreateSupply(CreateSupplyDTO supply)
        {
            bool flag = false;

            var marca = await _context.Marcas
                .Where(m => m.IdMarca == supply.IdBrand).ToListAsync();
            if (marca == null)
            {
                throw new Exception("Marca Inexistente");
            }
            var distribucion = await _context.TiposDistribucion
                .Where(d => d.IdTipoDistribucion == supply.IdDistribution).ToListAsync();
            if (distribucion == null)
            {
                throw new Exception("Distribucion Inexistente");
            }

            try
            {
                Suministro NewSupply = new Suministro()
                {
                    Nombre = supply.Name,
                    PreUnitario = supply.Price,
                    IdTipoSuministro = supply.IdSupplyType,
                    IdTipoDistribucion = supply.IdDistribution,
                    IdMarca = supply.IdBrand
                };

                await _context.Suministros.AddAsync(NewSupply);
                await _context.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear suministro: {ex.Message}");
            }

            return flag;

        }

        public async Task<List<ViewFacturacionPorAnio>> GetSalesPerYear()
        {
            return await _context.ViewFacturacionPorAnio.ToListAsync();
        }

        //public async Task<bool> DeleteSupply(int supplyId)
        //{
        //    Suministro SupplyToDelete = await _context.Suministros.Where (s => s.IdSupply == supplyId).FirstOrDefaultAsync(); 
        //    if (SupplyToDelete == null)
        //    {
        //        throw new NotFoundException($"no se encontro el suministro {supplyId}");
        //    }

        //    SupplyToDelete.Active = false;

        //    _context.Suministros.Update(SupplyToDelete);

        //    return await _context.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> UpdateSupply(CreateSupplyDTO supply, int codigo)
        //{
        //    Suministro supplyToUpdate = await _context.Suministros.Where(s => s.IdSupply == codigo & s.Active == true).FirstOrDefaultAsync();
        //    if (supplyToUpdate == null)
        //    {
        //        throw new NotFoundException($"No hay registros para el suministro con id: {codigo}");
        //    }

        //    if (!string.IsNullOrEmpty(supply.Name) && supply.Name != supplyToUpdate.Nombre)
        //    {
        //        supplyToUpdate.Nombre = supply.Name;
        //    }

        //    if (!string.IsNullOrEmpty(supply.Price) && supply.Price != supplyToUpdate.PreUnitario)
        //    {
        //        supplyToUpdate.PreUnitario = supply.Price;
        //    }

        //    _context.Suministros.Update(supplyToUpdate);

        //    return await _context.SaveChangesAsync() > 0;
        //}
    }
}

