﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Models;

[Keyless]
public partial class ViewFacturacionPorAnio
{
    [Column("Suministro")]
    public string? Supply { get; set; }
    [Column("Año")]
    public int Year { get; set; }
    [Column("Total Facturado")]
    public int Total { get; set; }
}