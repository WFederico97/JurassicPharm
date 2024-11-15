using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class Sucursale
{
    public int IdSucursal { get; set; }

    public int? IdCiudad { get; set; }

    public string? Calle { get; set; }

    public int? Altura { get; set; }
}
