using JurassicPharm.DTO.Supplies;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Supplies.Interfaces;
using JurassicPharm.Services.Supplies.Interfaces;


namespace JurassicPharm.Services.Supplies.Implementations
{
    public class SuppliesService : ISuppliesService
    {
        private readonly IsuppliesRepository _repository;

        public SuppliesService(IsuppliesRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ViewFacturacionPorSuministroAnual>> GetCurrentYearSalesBySupply()
        {
            return await _repository.GetCurrentYearSalesBySupply();
        }
        public async Task<Dictionary<string, List<SelectOptionDTO>>> GetSelectOptions()
        {
            return await _repository.GetSelectOptionsDictionary();
        }

        public async Task<List<GetSupplyDTO>> GetAllSupply()
        {
            return await _repository.GetAllSupply();
        }

        public async Task<bool> CreateSupply(CreateSupplyDTO supply)
        {
            return await _repository.CreateSupply(supply);
        }

        public async Task<List<ViewFacturacionPorAnio>> GetSalesPerYear()
        {
            return await _repository.GetSalesPerYear();
        }

        public async Task<bool> UpdateSupply(UpdateSupplyDTO supply, int codigo)
        {
            return await _repository.UpdateSupply(supply, codigo);
        }

    }
}
