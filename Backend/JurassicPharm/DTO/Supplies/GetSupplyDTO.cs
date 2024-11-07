using JurassicPharm.Models;

namespace JurassicPharm.DTO.Supplies
{
    public class GetSupplyDTO
    {
        public int IdSupply { get; set; }
        public string Name { get; set; }

        public int? Price { get; set; }

        public string? SupplyType { get; set; }

        public string? Distribution { get; set; }

        public string? Brand { get; set; }
        public int? Stock { get; set; }
        public int? MinimumStock { get; set; }
    }
}