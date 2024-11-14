using JurassicPharm.Models;

namespace JurassicPharm.DTO.Supplies
{
    public class GetMedBrandDTO
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public List<GetSupplyDTO> Suministros { get; set; }
    }
}
