﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using JurassicPharm.Models;

namespace JurassicPharm.Models
{
    public partial class JurassicPharmContext
    {

        [DbFunction("FX_CALCULAR_DESCUENTO", "dbo")]
        public static decimal? FX_CALCULAR_DESCUENTO(int? ID_OBRA_SOCIAL, int? NRO_FACTURA)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        protected void OnModelCreatingGeneratedFunctions(ModelBuilder modelBuilder)
        {
        }
    }
}
