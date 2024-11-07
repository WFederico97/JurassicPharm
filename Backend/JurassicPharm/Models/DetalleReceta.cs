﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Models;

[Table("DETALLES_RECETA")]
public partial class DetalleReceta
{
    [Key]
    [Column("id_detalle_receta")]
    public int IdDetalleReceta { get; set; }

    [Column("id_receta")]
    public int? IdReceta { get; set; }

    [Column("id_suministro")]
    public int? IdSuministro { get; set; }

    [Column("descripcion")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("cantidad")]
    public int? Cantidad { get; set; }

    [ForeignKey("IdReceta")]
    [InverseProperty("DetallesReceta")]
    public virtual Receta? IdRecetaNavigation { get; set; }

    [ForeignKey("IdSuministro")]
    [InverseProperty("DetallesReceta")]
    public virtual Suministro? IdSuministroNavigation { get; set; }
}