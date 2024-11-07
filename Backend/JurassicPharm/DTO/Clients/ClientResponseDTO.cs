namespace JurassicPharm.DTO.Clients
{
    public class ClientResponseDTO
    {
        public int IdClient { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public int? Number { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? HealthPlan { get; set; }
    }
}
