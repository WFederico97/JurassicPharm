using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class DetallesFactura
{
    public int IdDetalleFactura { get; set; }

    public int? NroFactura { get; set; }

    public int? IdSuministro { get; set; }

    public int? PreVenta { get; set; }

    public int? Cantidad { get; set; }
}
