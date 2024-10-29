namespace JurassicPharm.DTO.Personnel
{
    public class CreatePersonnelDTO : UpdatePersonnelDTO
    {
        public string PasswordEmpleado { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdCiudad { get; set; }

    }
}
