namespace JurassicPharm.DTO.Supplies
{
    public class UpdateSupplyDTO
    {
        public string? Name { get; set; }

        public int? Price { get; set; }

        public int? IdSupplyType { get; set; }

        public int? IdDistribution { get; set; }

        public int Stock { get; set; }
        public int StockMinimo { get; set; }
    }
}
