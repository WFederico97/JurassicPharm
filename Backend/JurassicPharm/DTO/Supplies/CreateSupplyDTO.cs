using JurassicPharm.Models;

namespace JurassicPharm.DTO.Supplies

{
    public class CreateSupplyDTO
    {

        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
        public int IdBrand { get; set; }
        public int IdDistribution { get; set; }
        public int IdSupplyType { get; set; }


    }
}
