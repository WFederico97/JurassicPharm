﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JurassicPharm.Models;

public partial class Provincias
{
    public int IdProvincia { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Localidades> Localidades { get; set; } = new List<Localidades>();
}