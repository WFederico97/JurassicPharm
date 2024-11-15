using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class Localidade
{
    public int IdLocalidad { get; set; }

    public string? Nombre { get; set; }

    public int? IdProvincia { get; set; }
}
