namespace JurassicPharm.DTOs.Personnel
{
    public class CreatePersonnelDTO : UpdatePersonnelDTO
    {
        public int? IdSucursal { get; set; }
        public int? IdCiudad { get; set; }

    }
}
