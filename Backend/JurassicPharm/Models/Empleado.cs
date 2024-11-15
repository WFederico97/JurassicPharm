﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Models;

[Table("EMPLEADOS")]
public partial class Empleado
{
    [Key]
    [Column("legajo_empleado")]
    public int LegajoEmpleado { get; set; }

    [Column("id_sucursal")]
    public int? IdSucursal { get; set; }

    [Column("id_ciudad")]
    public int? IdCiudad { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("apellido")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Apellido { get; set; }

    [Column("calle")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Calle { get; set; }

    [Column("altura")]
    public int? Altura { get; set; }

    [Column("correo_electronico")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CorreoElectronico { get; set; }

    public bool? Active { get; set; }

    [Column("rol")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Rol { get; set; }

    [Column("password_empleado")]
    [StringLength(64)]
    [Unicode(false)]
    public string? PasswordEmpleado { get; set; }

    [ForeignKey("IdCiudad")]
    [InverseProperty("Empleados")]
    public virtual Ciudad? IdCiudadNavigation { get; set; }

    [ForeignKey("IdSucursal")]
    [InverseProperty("Empleados")]
    public virtual Sucursal? IdSucursalNavigation { get; set; }

    [InverseProperty("LegajoEmpleadoNavigation")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}




