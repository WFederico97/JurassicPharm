using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class Ciudade
{
    public int IdCiudad { get; set; }

    public string? Nombre { get; set; }

    public int? IdLocalidad { get; set; }
}
