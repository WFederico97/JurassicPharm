using JurassicPharm.DTO.Supplies;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Supplies.Interfaces
{
    public interface ISuppliesService
    {
        Task<List<ViewFacturacionPorSuministroAnual>> GetCurrentYearSalesBySupply();
        Task<Dictionary<string, List<SelectOptionDTO>>> GetSelectOptions();
        Task<List<GetSupplyDTO>> GetAllSupply();
        Task<List<ViewFacturacionPorAnio>> GetSalesPerYear();
        Task<bool> CreateSupply(CreateSupplyDTO supply);
        Task<bool> UpdateSupply(UpdateSupplyDTO supply, int codigo);


    }
}
