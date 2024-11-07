using JurassicPharm.DTO.Supplies;
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

        public async Task<List<GetSupplyDTO>> GetAllSupply()
        {
            return await _repository.GetAllSupply();
        }

        public async Task<bool> CreateSupply(CreateSupplyDTO supply)
        {
           return await _repository.CreateSupply(supply);
        }

        //public async Task<bool> DeleteSupply(int supplyId)
        //{
        //    return await _repository.DeleteSupply(supplyId);
        //}

        //public async Task<bool> UpdateSupply(CreateSupplyDTO supply, int codigo) 
        //{
        //    return await _repository.UpdateSupply(supply, codigo);
        //}

    }
}
