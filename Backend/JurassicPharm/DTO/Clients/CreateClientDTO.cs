namespace JurassicPharm.DTO.Clients
{
    public class CreateClientDTO
    {
        public int? IdHealthPlan { get; set; }

        public int? IdCity { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public int? Number { get; set; }
    }
}
