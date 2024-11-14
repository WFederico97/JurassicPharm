using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JurassicPharm.Models
{
    [Keyless]
    public partial class ViewFacturacionPorSuministroAnual
    {
        [Column("Suministro")]
        public string Suministro { get; set; }

        [Column("Año")]
        public int Anio { get; set; }

        [Column("TotalFacturado")]
        public float TotalFacturado { get; set; }
    }
}
