using JurassicPharm.Models;
using JurassicPharm.DTO.Supplies;

namespace JurassicPharm.Repositories.Supplies.Interfaces
{
    public interface IsuppliesRepository
    {
        Task<List<ViewFacturacionPorSuministroAnual>> GetCurrentYearSalesBySupply();
        Task<Dictionary<string, List<SelectOptionDTO>>> GetSelectOptionsDictionary();
        Task<List<GetSupplyDTO>> GetAllSupply();
        Task<List<ViewFacturacionPorAnio>> GetSalesPerYear();
        Task<bool> CreateSupply(CreateSupplyDTO supply);
        Task<bool> UpdateSupply(UpdateSupplyDTO supply, int codigo);
    }

}