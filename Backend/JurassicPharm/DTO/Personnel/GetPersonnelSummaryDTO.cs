namespace JurassicPharm.DTO.Personnel
{
    public class GetPersonnelSummaryDTO
    {
        public int LegajoEmpleado { get; set; }
        public string Role { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public bool Active { get; set; }
    }
}
