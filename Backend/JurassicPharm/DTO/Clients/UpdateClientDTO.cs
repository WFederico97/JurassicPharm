using JurassicPharm.Models;

namespace JurassicPharm.DTO.Clients
{
    public class UpdateClientDTO
    {
        public int? IdObraSocial { get; set; }

        public int? IdCiudad { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string CorreoElectronico { get; set; }

        public string Calle { get; set; }

        public int? Altura { get; set; }
    }
}
