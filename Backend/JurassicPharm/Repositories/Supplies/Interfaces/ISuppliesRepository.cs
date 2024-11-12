using JurassicPharm.Models;
using JurassicPharm.DTO.Supplies;

namespace JurassicPharm.Repositories.Supplies.Interfaces
{
    public interface IsuppliesRepository
    {
        Task<List<GetSupplyDTO>> GetAllSupply();
        Task<List<ViewFacturacionPorAnio>> GetSalesPerYear();
        Task<bool> CreateSupply(CreateSupplyDTO supply);
        //Task<bool> DeleteSupply(int supplyId);
        //Task<bool> UpdateSupply(CreateSupplyDTO supply, int codigo);
    }

}