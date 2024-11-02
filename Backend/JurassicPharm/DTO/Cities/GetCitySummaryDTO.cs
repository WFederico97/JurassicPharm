using JurassicPharm.DTO.Personnel;
using JurassicPharm.DTO.Stores;

namespace JurassicPharm.DTO.Cities
{
    public class GetCitySummaryDTO : GetCityDTO
    {
        public List<GetPersonnelSummaryDTO> Empleados { get; set; }
        public List<GetStoreDTO> Sucursales { get; set; }
    }
}
