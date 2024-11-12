using JurassicPharm.DTO.Supplies;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Supplies.Interfaces
{
    public interface ISuppliesService
    {
        Task<List<GetSupplyDTO>> GetAllSupply();

        Task<List<ViewFacturacionPorAnio>> GetSalesPerYear();
        Task<bool> CreateSupply(CreateSupplyDTO supply);
        //Task<bool> UpdateSupply(CreateSupplyDTO supply, int codigo);
        //Task<bool> DeleteSupply(int supplyId);


    }
}
