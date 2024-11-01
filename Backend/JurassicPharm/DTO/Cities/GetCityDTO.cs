using JurassicPharm.DTO.Personnel;

namespace JurassicPharm.DTO.Cities
{
    public class GetCityDTO
    {
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public List<GetPersonnelSummaryDTO> Empleados { get; set; }
    }
}
