﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JurassicPharm.Models;

public partial class JurassicPharmContext : DbContext
{
    public JurassicPharmContext(DbContextOptions<JurassicPharmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudad> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleFactura> DetallesFactura { get; set; }

    public virtual DbSet<DetalleReceta> DetallesReceta { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Localidad> Localidades { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<ObraSocial> ObrasSociales { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Sucursal> Sucursales { get; set; }

    public virtual DbSet<Suministro> Suministros { get; set; }

    public virtual DbSet<TipoDistribucion> TiposDistribucion { get; set; }

    public virtual DbSet<TipoSuministro> TiposSuministro { get; set; }

    public virtual DbSet<ViewFacturacionPorAnio> ViewFacturacionPorAnio { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__CIUDADES__B7DC4CD5C608193F");

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Ciudades).HasConstraintName("FK_CIUDADES_LOCALIDADES");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTES__677F38F582F05F01");

            entity.Property(e => e.IdCliente).ValueGeneratedNever();

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Clientes).HasConstraintName("FK_CLIENTES_CIUDADES");

            entity.HasOne(d => d.IdObraSocialNavigation).WithMany(p => p.Clientes).HasConstraintName("FK_CLIENTES_OBRAS_SOCIALES");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("PK__DETALLES__F6BFE343A7703636");

            entity.ToTable("DETALLES_FACTURA", tb => tb.HasTrigger("TRG_REDUCIR_STOCK"));

            entity.HasOne(d => d.IdSuministroNavigation).WithMany(p => p.DetallesFactura).HasConstraintName("FK_DETALLES_FACTURA_SUMINISTROS");

            entity.HasOne(d => d.NroFacturaNavigation).WithMany(p => p.DetallesFactura).HasConstraintName("FK_DETALLES_FACTURA_FACTURAS");
        });

        modelBuilder.Entity<DetalleReceta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleReceta).HasName("PK__DETALLES__2C99ACD3530A9E7C");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.DetallesReceta).HasConstraintName("FK_DETALLES_RECETA_RECETAS");

            entity.HasOne(d => d.IdSuministroNavigation).WithMany(p => p.DetallesReceta).HasConstraintName("FK_DETALLES_RECETA_SUMINISTROS");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.LegajoEmpleado).HasName("PK__EMPLEADO__DF787D2A40F63E7C");

            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Empleados).HasConstraintName("FK_EMPLEADOS_CIUDADES");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Empleados).HasConstraintName("FK_EMPLEADOS_SUCURSALES");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.NroFactura).HasName("PK__FACTURAS__B31FA9AFDAA314A2");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas).HasConstraintName("FK_FACTURAS_CLIENTES");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Facturas).HasConstraintName("FK_FACTURAS_SUCURSALES");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad).HasName("PK__LOCALIDA__9A5E82AABE0D072D");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Localidades).HasConstraintName("FK_LOCALIDADES_PROVINCIAS");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__MARCAS__7E43E99E2916FEFF");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Matricula).HasName("PK__MEDICOS__30962D147DE3393F");

            entity.Property(e => e.Matricula).ValueGeneratedNever();
        });

        modelBuilder.Entity<ObraSocial>(entity =>
        {
            entity.HasKey(e => e.IdObraSocial).HasName("PK__OBRAS_SO__89039DF61B424F56");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.ObrasSociales).HasConstraintName("FK_OBRAS_SOCIALES_CIUDADES");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__PROVEEDO__8D3DFE282ACC098E");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Proveedores).HasConstraintName("FK_PROVEEDORES_CIUDADES");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__PROVINCI__66C18BFD1341DE3F");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PK__RECETAS__11DB53ABF6E6DF1E");

            entity.HasOne(d => d.IdObraSocialNavigation).WithMany(p => p.Recetas).HasConstraintName("FK_RECETAS_OBRAS_SOCIALES");

            entity.HasOne(d => d.MatriculaNavigation).WithMany(p => p.Recetas).HasConstraintName("FK_RECETAS_MEDICOS");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.IdStock).HasName("PK__STOCKS__3A39590A0C543D5F");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Stocks).HasConstraintName("FK_STOCKS_PROVEEDORES");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Stocks).HasConstraintName("FK_STOCKS_SUCURSALES");

            entity.HasOne(d => d.LegajoEmpleadoNavigation).WithMany(p => p.Stocks).HasConstraintName("FK_STOCKS_EMPLEADOS");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__SUCURSAL__4C7580136F7C1585");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Sucursales).HasConstraintName("FK_SUCURSALES_CIUDADES");
        });

        modelBuilder.Entity<Suministro>(entity =>
        {
            entity.HasKey(e => e.IdSuministro).HasName("PK__SUMINIST__ABFE9BD1FBD497AA");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Suministros).HasConstraintName("FK_SUMINISTROS_MARCAS");

            entity.HasOne(d => d.IdTipoDistribucionNavigation).WithMany(p => p.Suministros).HasConstraintName("FK_SUMINISTROS_TIPOS_DISTRIBUCION");

            entity.HasOne(d => d.IdTipoSuministroNavigation).WithMany(p => p.Suministros).HasConstraintName("FK_SUMINISTROS_TIPOS_SUMINISTROS");
        });

        modelBuilder.Entity<TipoDistribucion>(entity =>
        {
            entity.HasKey(e => e.IdTipoDistribucion).HasName("PK__TIPOS_DI__CA6BE0F2CE3824E5");
        });

        modelBuilder.Entity<TipoSuministro>(entity =>
        {
            entity.HasKey(e => e.IdTipoSuministro).HasName("PK__TIPOS_SU__22748C15A8DFDC5D");
        });

        modelBuilder.Entity<ViewFacturacionPorAnio>(entity =>
        {
            entity.ToView("VIEW_FACTURACION_POR_ANIO");
        });


        OnModelCreatingGeneratedFunctions(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}