using JurassicPharm.DTO.Personnel;

namespace JurassicPharm.DTO.Stores
{
    public class GetStoreDTO
    {
        public int IdSucursal { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public string Ciudad { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public List<GetPersonnelSummaryDTO> Empleados { get; set; }
    }
}
