﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Models;

[Table("DETALLES_FACTURA")]
public partial class DetalleFactura
{
    [Key]
    [Column("id_detalle_factura")]
    public int IdDetalleFactura { get; set; }

    [Column("nro_factura")]
    public int? NroFactura { get; set; }

    [Column("id_suministro")]
    public int? IdSuministro { get; set; }

    [Column("pre_venta")]
    public int? PreVenta { get; set; }

    [Column("cantidad")]
    public int? Cantidad { get; set; }

    [ForeignKey("IdSuministro")]
    [InverseProperty("DetallesFactura")]
    public virtual Suministro? IdSuministroNavigation { get; set; }

    [ForeignKey("NroFactura")]
    [InverseProperty("DetallesFactura")]
    public virtual Factura? NroFacturaNavigation { get; set; }
}