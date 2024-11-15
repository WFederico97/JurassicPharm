using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class DetallesRecetum
{
    public int IdDetalleReceta { get; set; }

    public int? IdReceta { get; set; }

    public int? IdSuministro { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }
}
