namespace JurassicPharm.DTO.Personnel
{
    public class GetPersonnelDTO
    {
        public int LegajoEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Domicilio { get; set; }
        public string Rol { get; set; }
        public string Ciudad { get; set; }
        public string Localidad { get; set; } 
        public string Provincia { get; set; }
        public int Sucursal { get; set; } 
        public string DireccionSucursal { get; set; }
    }
}
