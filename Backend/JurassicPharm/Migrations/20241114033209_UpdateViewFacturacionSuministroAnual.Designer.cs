﻿// <auto-generated />
using System;
using JurassicPharm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JurassicPharm.Migrations
{
    [DbContext(typeof(JurassicPharmContext))]
    [Migration("20241114033209_UpdateViewFacturacionSuministroAnual")]
    partial class UpdateViewFacturacionSuministroAnual
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JurassicPharm.Models.Ciudad", b =>
                {
                    b.Property<int>("IdCiudad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_ciudad");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCiudad"));

                    b.Property<int?>("IdLocalidad")
                        .HasColumnType("int")
                        .HasColumnName("id_localidad");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdCiudad")
                        .HasName("PK__CIUDADES__B7DC4CD5C608193F");

                    b.HasIndex("IdLocalidad");

                    b.ToTable("CIUDADES");
                });

            modelBuilder.Entity("JurassicPharm.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_cliente");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<int?>("Altura")
                        .HasColumnType("int")
                        .HasColumnName("altura");

                    b.Property<string>("Apellido")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellido");

                    b.Property<string>("Calle")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("calle");

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("correo_electronico");

                    b.Property<int?>("IdCiudad")
                        .HasColumnType("int")
                        .HasColumnName("id_ciudad");

                    b.Property<int?>("IdObraSocial")
                        .HasColumnType("int")
                        .HasColumnName("id_obra_social");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdCliente")
                        .HasName("PK__CLIENTES__677F38F582F05F01");

                    b.HasIndex("IdCiudad");

                    b.HasIndex("IdObraSocial");

                    b.ToTable("CLIENTES");
                });

            modelBuilder.Entity("JurassicPharm.Models.DetalleFactura", b =>
                {
                    b.Property<int>("IdDetalleFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_detalle_factura");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleFactura"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int?>("IdSuministro")
                        .HasColumnType("int")
                        .HasColumnName("id_suministro");

                    b.Property<int?>("NroFactura")
                        .HasColumnType("int")
                        .HasColumnName("nro_factura");

                    b.Property<int?>("PreVenta")
                        .HasColumnType("int")
                        .HasColumnName("pre_venta");

                    b.HasKey("IdDetalleFactura")
                        .HasName("PK__DETALLES__F6BFE343A7703636");

                    b.HasIndex("IdSuministro");

                    b.HasIndex("NroFactura");

                    b.ToTable("DETALLES_FACTURA", null, t =>
                        {
                            t.HasTrigger("TRG_REDUCIR_STOCK");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("JurassicPharm.Models.DetalleReceta", b =>
                {
                    b.Property<int>("IdDetalleReceta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_detalle_receta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleReceta"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("descripcion");

                    b.Property<int?>("IdReceta")
                        .HasColumnType("int")
                        .HasColumnName("id_receta");

                    b.Property<int?>("IdSuministro")
                        .HasColumnType("int")
                        .HasColumnName("id_suministro");

                    b.HasKey("IdDetalleReceta")
                        .HasName("PK__DETALLES__2C99ACD3530A9E7C");

                    b.HasIndex("IdReceta");

                    b.HasIndex("IdSuministro");

                    b.ToTable("DETALLES_RECETA");
                });

            modelBuilder.Entity("JurassicPharm.Models.Empleado", b =>
                {
                    b.Property<int>("LegajoEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("legajo_empleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LegajoEmpleado"));

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int?>("Altura")
                        .HasColumnType("int")
                        .HasColumnName("altura");

                    b.Property<string>("Apellido")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellido");

                    b.Property<string>("Calle")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("calle");

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("correo_electronico");

                    b.Property<int?>("IdCiudad")
                        .HasColumnType("int")
                        .HasColumnName("id_ciudad");

                    b.Property<int?>("IdSucursal")
                        .HasColumnType("int")
                        .HasColumnName("id_sucursal");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<string>("PasswordEmpleado")
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("password_empleado");

                    b.Property<string>("Rol")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("rol");

                    b.HasKey("LegajoEmpleado")
                        .HasName("PK__EMPLEADO__DF787D2A40F63E7C");

                    b.HasIndex("IdCiudad");

                    b.HasIndex("IdSucursal");

                    b.ToTable("EMPLEADOS");
                });

            modelBuilder.Entity("JurassicPharm.Models.Factura", b =>
                {
                    b.Property<int>("NroFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("nro_factura");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NroFactura"));

                    b.Property<DateOnly?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<int?>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("id_cliente");

                    b.Property<int?>("IdSucursal")
                        .HasColumnType("int")
                        .HasColumnName("id_sucursal");

                    b.HasKey("NroFactura")
                        .HasName("PK__FACTURAS__B31FA9AFDAA314A2");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdSucursal");

                    b.ToTable("FACTURAS");
                });

            modelBuilder.Entity("JurassicPharm.Models.Localidad", b =>
                {
                    b.Property<int>("IdLocalidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_localidad");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLocalidad"));

                    b.Property<int?>("IdProvincia")
                        .HasColumnType("int")
                        .HasColumnName("id_provincia");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdLocalidad")
                        .HasName("PK__LOCALIDA__9A5E82AABE0D072D");

                    b.HasIndex("IdProvincia");

                    b.ToTable("LOCALIDADES");
                });

            modelBuilder.Entity("JurassicPharm.Models.Marca", b =>
                {
                    b.Property<int>("IdMarca")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_marca");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMarca"));

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdMarca")
                        .HasName("PK__MARCAS__7E43E99E2916FEFF");

                    b.ToTable("MARCAS");
                });

            modelBuilder.Entity("JurassicPharm.Models.Medico", b =>
                {
                    b.Property<int>("Matricula")
                        .HasColumnType("int")
                        .HasColumnName("matricula");

                    b.Property<string>("Apellido")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellido");

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("correo_electronico");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("Matricula")
                        .HasName("PK__MEDICOS__30962D147DE3393F");

                    b.ToTable("MEDICOS");
                });

            modelBuilder.Entity("JurassicPharm.Models.ObraSocial", b =>
                {
                    b.Property<int>("IdObraSocial")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_obra_social");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdObraSocial"));

                    b.Property<int?>("IdCiudad")
                        .HasColumnType("int")
                        .HasColumnName("id_ciudad");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdObraSocial")
                        .HasName("PK__OBRAS_SO__89039DF61B424F56");

                    b.HasIndex("IdCiudad");

                    b.ToTable("OBRAS_SOCIALES");
                });

            modelBuilder.Entity("JurassicPharm.Models.Proveedor", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_proveedor");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedor"));

                    b.Property<int?>("IdCiudad")
                        .HasColumnType("int")
                        .HasColumnName("id_ciudad");

                    b.Property<string>("RazonSocial")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("razon_social");

                    b.HasKey("IdProveedor")
                        .HasName("PK__PROVEEDO__8D3DFE282ACC098E");

                    b.HasIndex("IdCiudad");

                    b.ToTable("PROVEEDORES");
                });

            modelBuilder.Entity("JurassicPharm.Models.Provincia", b =>
                {
                    b.Property<int>("IdProvincia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_provincia");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProvincia"));

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdProvincia")
                        .HasName("PK__PROVINCI__66C18BFD1341DE3F");

                    b.ToTable("PROVINCIAS");
                });

            modelBuilder.Entity("JurassicPharm.Models.Receta", b =>
                {
                    b.Property<int>("IdReceta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_receta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReceta"));

                    b.Property<DateOnly?>("FechaVencimiento")
                        .HasColumnType("date")
                        .HasColumnName("fecha_vencimiento");

                    b.Property<int?>("IdObraSocial")
                        .HasColumnType("int")
                        .HasColumnName("id_obra_social");

                    b.Property<int?>("Matricula")
                        .HasColumnType("int")
                        .HasColumnName("matricula");

                    b.Property<string>("TipoReceta")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tipo_receta");

                    b.HasKey("IdReceta")
                        .HasName("PK__RECETAS__11DB53ABF6E6DF1E");

                    b.HasIndex("IdObraSocial");

                    b.HasIndex("Matricula");

                    b.ToTable("RECETAS");
                });

            modelBuilder.Entity("JurassicPharm.Models.Stock", b =>
                {
                    b.Property<int>("IdStock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_stock");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStock"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<DateOnly?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<int?>("IdProveedor")
                        .HasColumnType("int")
                        .HasColumnName("id_proveedor");

                    b.Property<int?>("IdSucursal")
                        .HasColumnType("int")
                        .HasColumnName("id_sucursal");

                    b.Property<int?>("LegajoEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("legajo_empleado");

                    b.HasKey("IdStock")
                        .HasName("PK__STOCKS__3A39590A0C543D5F");

                    b.HasIndex("IdProveedor");

                    b.HasIndex("IdSucursal");

                    b.HasIndex("LegajoEmpleado");

                    b.ToTable("STOCKS");
                });

            modelBuilder.Entity("JurassicPharm.Models.Sucursal", b =>
                {
                    b.Property<int>("IdSucursal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_sucursal");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSucursal"));

                    b.Property<int?>("Altura")
                        .HasColumnType("int")
                        .HasColumnName("altura");

                    b.Property<string>("Calle")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("calle");

                    b.Property<int?>("IdCiudad")
                        .HasColumnType("int")
                        .HasColumnName("id_ciudad");

                    b.HasKey("IdSucursal")
                        .HasName("PK__SUCURSAL__4C7580136F7C1585");

                    b.HasIndex("IdCiudad");

                    b.ToTable("SUCURSALES");
                });

            modelBuilder.Entity("JurassicPharm.Models.Suministro", b =>
                {
                    b.Property<int>("IdSuministro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_suministro");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSuministro"));

                    b.Property<int?>("IdMarca")
                        .HasColumnType("int")
                        .HasColumnName("id_marca");

                    b.Property<int?>("IdTipoDistribucion")
                        .HasColumnType("int")
                        .HasColumnName("id_tipo_distribucion");

                    b.Property<int?>("IdTipoSuministro")
                        .HasColumnType("int")
                        .HasColumnName("id_tipo_suministro");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<int?>("PreUnitario")
                        .HasColumnType("int")
                        .HasColumnName("pre_unitario");

                    b.Property<int?>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<int?>("StockMinimo")
                        .HasColumnType("int")
                        .HasColumnName("stock_minimo");

                    b.HasKey("IdSuministro")
                        .HasName("PK__SUMINIST__ABFE9BD1FBD497AA");

                    b.HasIndex("IdMarca");

                    b.HasIndex("IdTipoDistribucion");

                    b.HasIndex("IdTipoSuministro");

                    b.ToTable("SUMINISTROS");
                });

            modelBuilder.Entity("JurassicPharm.Models.TipoDistribucion", b =>
                {
                    b.Property<int>("IdTipoDistribucion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_tipo_distribucion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoDistribucion"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("descripcion");

                    b.HasKey("IdTipoDistribucion")
                        .HasName("PK__TIPOS_DI__CA6BE0F2CE3824E5");

                    b.ToTable("TIPOS_DISTRIBUCION");
                });

            modelBuilder.Entity("JurassicPharm.Models.TipoSuministro", b =>
                {
                    b.Property<int>("IdTipoSuministro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_tipo_suministro");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoSuministro"));

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.HasKey("IdTipoSuministro")
                        .HasName("PK__TIPOS_SU__22748C15A8DFDC5D");

                    b.ToTable("TIPOS_SUMINISTRO");
                });

            modelBuilder.Entity("JurassicPharm.Models.ViewFacturacionPorAnio", b =>
                {
                    b.Property<string>("Supply")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Suministro");

                    b.Property<int>("Total")
                        .HasColumnType("int")
                        .HasColumnName("Total Facturado");

                    b.Property<int>("Year")
                        .HasColumnType("int")
                        .HasColumnName("Año");

                    b.ToTable((string)null);

                    b.ToView("VIEW_FACTURACION_POR_ANIO", (string)null);
                });

            modelBuilder.Entity("JurassicPharm.Models.ViewFacturacionPorSuministroAnual", b =>
                {
                    b.Property<int>("Ano")
                        .HasColumnType("int")
                        .HasColumnName("Año");

                    b.Property<string>("Suministro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Suministro");

                    b.Property<decimal>("TotalFacturado")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("TotalFacturado");

                    b.ToTable((string)null);

                    b.ToView("VIEW_FACTURACION_POR_SUMINISTRO_ANUAL", (string)null);
                });

            modelBuilder.Entity("JurassicPharm.Models.Ciudad", b =>
                {
                    b.HasOne("JurassicPharm.Models.Localidad", "IdLocalidadNavigation")
                        .WithMany("Ciudades")
                        .HasForeignKey("IdLocalidad")
                        .HasConstraintName("FK_CIUDADES_LOCALIDADES");

                    b.Navigation("IdLocalidadNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Cliente", b =>
                {
                    b.HasOne("JurassicPharm.Models.Ciudad", "IdCiudadNavigation")
                        .WithMany("Clientes")
                        .HasForeignKey("IdCiudad")
                        .HasConstraintName("FK_CLIENTES_CIUDADES");

                    b.HasOne("JurassicPharm.Models.ObraSocial", "IdObraSocialNavigation")
                        .WithMany("Clientes")
                        .HasForeignKey("IdObraSocial")
                        .HasConstraintName("FK_CLIENTES_OBRAS_SOCIALES");

                    b.Navigation("IdCiudadNavigation");

                    b.Navigation("IdObraSocialNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.DetalleFactura", b =>
                {
                    b.HasOne("JurassicPharm.Models.Suministro", "IdSuministroNavigation")
                        .WithMany("DetallesFactura")
                        .HasForeignKey("IdSuministro")
                        .HasConstraintName("FK_DETALLES_FACTURA_SUMINISTROS");

                    b.HasOne("JurassicPharm.Models.Factura", "NroFacturaNavigation")
                        .WithMany("DetallesFactura")
                        .HasForeignKey("NroFactura")
                        .HasConstraintName("FK_DETALLES_FACTURA_FACTURAS");

                    b.Navigation("IdSuministroNavigation");

                    b.Navigation("NroFacturaNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.DetalleReceta", b =>
                {
                    b.HasOne("JurassicPharm.Models.Receta", "IdRecetaNavigation")
                        .WithMany("DetallesReceta")
                        .HasForeignKey("IdReceta")
                        .HasConstraintName("FK_DETALLES_RECETA_RECETAS");

                    b.HasOne("JurassicPharm.Models.Suministro", "IdSuministroNavigation")
                        .WithMany("DetallesReceta")
                        .HasForeignKey("IdSuministro")
                        .HasConstraintName("FK_DETALLES_RECETA_SUMINISTROS");

                    b.Navigation("IdRecetaNavigation");

                    b.Navigation("IdSuministroNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Empleado", b =>
                {
                    b.HasOne("JurassicPharm.Models.Ciudad", "IdCiudadNavigation")
                        .WithMany("Empleados")
                        .HasForeignKey("IdCiudad")
                        .HasConstraintName("FK_EMPLEADOS_CIUDADES");

                    b.HasOne("JurassicPharm.Models.Sucursal", "IdSucursalNavigation")
                        .WithMany("Empleados")
                        .HasForeignKey("IdSucursal")
                        .HasConstraintName("FK_EMPLEADOS_SUCURSALES");

                    b.Navigation("IdCiudadNavigation");

                    b.Navigation("IdSucursalNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Factura", b =>
                {
                    b.HasOne("JurassicPharm.Models.Cliente", "IdClienteNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK_FACTURAS_CLIENTES");

                    b.HasOne("JurassicPharm.Models.Sucursal", "IdSucursalNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdSucursal")
                        .HasConstraintName("FK_FACTURAS_SUCURSALES");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdSucursalNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Localidad", b =>
                {
                    b.HasOne("JurassicPharm.Models.Provincia", "IdProvinciaNavigation")
                        .WithMany("Localidades")
                        .HasForeignKey("IdProvincia")
                        .HasConstraintName("FK_LOCALIDADES_PROVINCIAS");

                    b.Navigation("IdProvinciaNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.ObraSocial", b =>
                {
                    b.HasOne("JurassicPharm.Models.Ciudad", "IdCiudadNavigation")
                        .WithMany("ObrasSociales")
                        .HasForeignKey("IdCiudad")
                        .HasConstraintName("FK_OBRAS_SOCIALES_CIUDADES");

                    b.Navigation("IdCiudadNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Proveedor", b =>
                {
                    b.HasOne("JurassicPharm.Models.Ciudad", "IdCiudadNavigation")
                        .WithMany("Proveedores")
                        .HasForeignKey("IdCiudad")
                        .HasConstraintName("FK_PROVEEDORES_CIUDADES");

                    b.Navigation("IdCiudadNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Receta", b =>
                {
                    b.HasOne("JurassicPharm.Models.ObraSocial", "IdObraSocialNavigation")
                        .WithMany("Recetas")
                        .HasForeignKey("IdObraSocial")
                        .HasConstraintName("FK_RECETAS_OBRAS_SOCIALES");

                    b.HasOne("JurassicPharm.Models.Medico", "MatriculaNavigation")
                        .WithMany("Recetas")
                        .HasForeignKey("Matricula")
                        .HasConstraintName("FK_RECETAS_MEDICOS");

                    b.Navigation("IdObraSocialNavigation");

                    b.Navigation("MatriculaNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Stock", b =>
                {
                    b.HasOne("JurassicPharm.Models.Proveedor", "IdProveedorNavigation")
                        .WithMany("Stocks")
                        .HasForeignKey("IdProveedor")
                        .HasConstraintName("FK_STOCKS_PROVEEDORES");

                    b.HasOne("JurassicPharm.Models.Sucursal", "IdSucursalNavigation")
                        .WithMany("Stocks")
                        .HasForeignKey("IdSucursal")
                        .HasConstraintName("FK_STOCKS_SUCURSALES");

                    b.HasOne("JurassicPharm.Models.Empleado", "LegajoEmpleadoNavigation")
                        .WithMany("Stocks")
                        .HasForeignKey("LegajoEmpleado")
                        .HasConstraintName("FK_STOCKS_EMPLEADOS");

                    b.Navigation("IdProveedorNavigation");

                    b.Navigation("IdSucursalNavigation");

                    b.Navigation("LegajoEmpleadoNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Sucursal", b =>
                {
                    b.HasOne("JurassicPharm.Models.Ciudad", "IdCiudadNavigation")
                        .WithMany("Sucursales")
                        .HasForeignKey("IdCiudad")
                        .HasConstraintName("FK_SUCURSALES_CIUDADES");

                    b.Navigation("IdCiudadNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Suministro", b =>
                {
                    b.HasOne("JurassicPharm.Models.Marca", "IdMarcaNavigation")
                        .WithMany("Suministros")
                        .HasForeignKey("IdMarca")
                        .HasConstraintName("FK_SUMINISTROS_MARCAS");

                    b.HasOne("JurassicPharm.Models.TipoDistribucion", "IdTipoDistribucionNavigation")
                        .WithMany("Suministros")
                        .HasForeignKey("IdTipoDistribucion")
                        .HasConstraintName("FK_SUMINISTROS_TIPOS_DISTRIBUCION");

                    b.HasOne("JurassicPharm.Models.TipoSuministro", "IdTipoSuministroNavigation")
                        .WithMany("Suministros")
                        .HasForeignKey("IdTipoSuministro")
                        .HasConstraintName("FK_SUMINISTROS_TIPOS_SUMINISTROS");

                    b.Navigation("IdMarcaNavigation");

                    b.Navigation("IdTipoDistribucionNavigation");

                    b.Navigation("IdTipoSuministroNavigation");
                });

            modelBuilder.Entity("JurassicPharm.Models.Ciudad", b =>
                {
                    b.Navigation("Clientes");

                    b.Navigation("Empleados");

                    b.Navigation("ObrasSociales");

                    b.Navigation("Proveedores");

                    b.Navigation("Sucursales");
                });

            modelBuilder.Entity("JurassicPharm.Models.Cliente", b =>
                {
                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("JurassicPharm.Models.Empleado", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("JurassicPharm.Models.Factura", b =>
                {
                    b.Navigation("DetallesFactura");
                });

            modelBuilder.Entity("JurassicPharm.Models.Localidad", b =>
                {
                    b.Navigation("Ciudades");
                });

            modelBuilder.Entity("JurassicPharm.Models.Marca", b =>
                {
                    b.Navigation("Suministros");
                });

            modelBuilder.Entity("JurassicPharm.Models.Medico", b =>
                {
                    b.Navigation("Recetas");
                });

            modelBuilder.Entity("JurassicPharm.Models.ObraSocial", b =>
                {
                    b.Navigation("Clientes");

                    b.Navigation("Recetas");
                });

            modelBuilder.Entity("JurassicPharm.Models.Proveedor", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("JurassicPharm.Models.Provincia", b =>
                {
                    b.Navigation("Localidades");
                });

            modelBuilder.Entity("JurassicPharm.Models.Receta", b =>
                {
                    b.Navigation("DetallesReceta");
                });

            modelBuilder.Entity("JurassicPharm.Models.Sucursal", b =>
                {
                    b.Navigation("Empleados");

                    b.Navigation("Facturas");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("JurassicPharm.Models.Suministro", b =>
                {
                    b.Navigation("DetallesFactura");

                    b.Navigation("DetallesReceta");
                });

            modelBuilder.Entity("JurassicPharm.Models.TipoDistribucion", b =>
                {
                    b.Navigation("Suministros");
                });

            modelBuilder.Entity("JurassicPharm.Models.TipoSuministro", b =>
                {
                    b.Navigation("Suministros");
                });
#pragma warning restore 612, 618
        }
    }
}
