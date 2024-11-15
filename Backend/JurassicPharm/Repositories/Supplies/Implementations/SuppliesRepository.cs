using JurassicPharm.DTO.Supplies;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Exceptions;
using JurassicPharm.Repositories.Supplies.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Repositories.Supplies.implementations
{
    public class SuppliesRepository : IsuppliesRepository
    {
        private readonly JurassicPharmContext _context;

        public SuppliesRepository(JurassicPharmContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, List<SelectOptionDTO>>> GetSelectOptionsDictionary()
        {
            var marcas = await _context.Marcas
                .Select(m => new SelectOptionDTO
                {
                    Id = m.IdMarca,
                    Nombre = m.Nombre
                })
                .ToListAsync();

            var tiposSuministro = await _context.TiposSuministro
                .Select(ts => new SelectOptionDTO
                {
                    Id = ts.IdTipoSuministro,
                    Nombre = ts.Nombre
                })
                .ToListAsync();

            var tiposDistribucion = await _context.TiposDistribucion
                .Select(td => new SelectOptionDTO
                {
                    Id = td.IdTipoDistribucion,
                    Nombre = td.Descripcion
                })
                .ToListAsync();

            var result = new Dictionary<string, List<SelectOptionDTO>>
                {
                    { "marcas", marcas },
                    { "tiposSuministro", tiposSuministro },
                    { "tiposDistribucion", tiposDistribucion }
                };

            return result;
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
            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.IdMarca == supply.IdBrand);
            if (marca == null)
            {
                throw new Exception("Marca Inexistente");
            }

            var distribucion = await _context.TiposDistribucion
                .FirstOrDefaultAsync(d => d.IdTipoDistribucion == supply.IdDistribution);
            if (distribucion == null)
            {
                throw new Exception("Distribucion Inexistente");
            }

            try
            {
                var newSupply = new Suministro
                {
                    Nombre = supply.Name,
                    PreUnitario = supply.Price,
                    Stock = supply.Stock,
                    StockMinimo = supply.MinimumStock,
                    IdTipoSuministro = supply.IdSupplyType,
                    IdTipoDistribucion = supply.IdDistribution,
                    IdMarca = supply.IdBrand
                };

                await _context.Suministros.AddAsync(newSupply);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear suministro: {ex.Message}");
            }
        }

        public async Task<List<ViewFacturacionPorAnio>> GetSalesPerYear()
        {
            return await _context.ViewFacturacionPorAnio.ToListAsync();
        }


        public async Task<bool> UpdateSupply(UpdateSupplyDTO supply, int codigo)
        {
            Suministro supplyToUpdate = await _context.Suministros.Where(s => s.IdSuministro == codigo).FirstOrDefaultAsync();
            if (supplyToUpdate == null)
            {
                throw new NotFoundException($"No hay registros para el suministro con id: {codigo}");
            }

            if (!string.IsNullOrEmpty(supply.Name) && supply.Name != supplyToUpdate.Nombre)
            {
                supplyToUpdate.Nombre = supply.Name;
            }

            if (supply.Price > 0 && supply.Price != supplyToUpdate.PreUnitario)
            {
                supplyToUpdate.PreUnitario = supply.Price;
            }
            if (supply.Stock > 0 && supply.Stock != supplyToUpdate.Stock)
            {
                supplyToUpdate.Stock = supply.Stock;
            }
            if (supply.StockMinimo > 0 && supply.StockMinimo != supplyToUpdate.StockMinimo)
            {
                supplyToUpdate.StockMinimo = supply.StockMinimo;
            }

            _context.Suministros.Update(supplyToUpdate);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}

