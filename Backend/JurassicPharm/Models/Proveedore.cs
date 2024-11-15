using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public int? IdCiudad { get; set; }

    public string? RazonSocial { get; set; }
}
