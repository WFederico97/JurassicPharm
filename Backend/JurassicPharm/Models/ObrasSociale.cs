using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class ObrasSociale
{
    public int IdObraSocial { get; set; }

    public string? Nombre { get; set; }

    public int? IdCiudad { get; set; }
}
