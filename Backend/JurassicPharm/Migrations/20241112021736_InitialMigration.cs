using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JurassicPharm.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MARCAS",
                columns: table => new
                {
                    id_marca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MARCAS__7E43E99E2916FEFF", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "MEDICOS",
                columns: table => new
                {
                    matricula = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    correo_electronico = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MEDICOS__30962D147DE3393F", x => x.matricula);
                });

            migrationBuilder.CreateTable(
                name: "PROVINCIAS",
                columns: table => new
                {
                    id_provincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROVINCI__66C18BFD1341DE3F", x => x.id_provincia);
                });

            migrationBuilder.CreateTable(
                name: "TIPOS_DISTRIBUCION",
                columns: table => new
                {
                    id_tipo_distribucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TIPOS_DI__CA6BE0F2CE3824E5", x => x.id_tipo_distribucion);
                });

            migrationBuilder.CreateTable(
                name: "TIPOS_SUMINISTRO",
                columns: table => new
                {
                    id_tipo_suministro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TIPOS_SU__22748C15A8DFDC5D", x => x.id_tipo_suministro);
                });

            migrationBuilder.CreateTable(
                name: "LOCALIDADES",
                columns: table => new
                {
                    id_localidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_provincia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LOCALIDA__9A5E82AABE0D072D", x => x.id_localidad);
                    table.ForeignKey(
                        name: "FK_LOCALIDADES_PROVINCIAS",
                        column: x => x.id_provincia,
                        principalTable: "PROVINCIAS",
                        principalColumn: "id_provincia");
                });

            migrationBuilder.CreateTable(
                name: "SUMINISTROS",
                columns: table => new
                {
                    id_suministro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    pre_unitario = table.Column<int>(type: "int", nullable: true),
                    id_tipo_suministro = table.Column<int>(type: "int", nullable: true),
                    id_tipo_distribucion = table.Column<int>(type: "int", nullable: true),
                    id_marca = table.Column<int>(type: "int", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: true),
                    stock_minimo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SUMINIST__ABFE9BD1FBD497AA", x => x.id_suministro);
                    table.ForeignKey(
                        name: "FK_SUMINISTROS_MARCAS",
                        column: x => x.id_marca,
                        principalTable: "MARCAS",
                        principalColumn: "id_marca");
                    table.ForeignKey(
                        name: "FK_SUMINISTROS_TIPOS_DISTRIBUCION",
                        column: x => x.id_tipo_distribucion,
                        principalTable: "TIPOS_DISTRIBUCION",
                        principalColumn: "id_tipo_distribucion");
                    table.ForeignKey(
                        name: "FK_SUMINISTROS_TIPOS_SUMINISTROS",
                        column: x => x.id_tipo_suministro,
                        principalTable: "TIPOS_SUMINISTRO",
                        principalColumn: "id_tipo_suministro");
                });

            migrationBuilder.CreateTable(
                name: "CIUDADES",
                columns: table => new
                {
                    id_ciudad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_localidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CIUDADES__B7DC4CD5C608193F", x => x.id_ciudad);
                    table.ForeignKey(
                        name: "FK_CIUDADES_LOCALIDADES",
                        column: x => x.id_localidad,
                        principalTable: "LOCALIDADES",
                        principalColumn: "id_localidad");
                });

            migrationBuilder.CreateTable(
                name: "OBRAS_SOCIALES",
                columns: table => new
                {
                    id_obra_social = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_ciudad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OBRAS_SO__89039DF61B424F56", x => x.id_obra_social);
                    table.ForeignKey(
                        name: "FK_OBRAS_SOCIALES_CIUDADES",
                        column: x => x.id_ciudad,
                        principalTable: "CIUDADES",
                        principalColumn: "id_ciudad");
                });

            migrationBuilder.CreateTable(
                name: "PROVEEDORES",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ciudad = table.Column<int>(type: "int", nullable: true),
                    razon_social = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROVEEDO__8D3DFE282ACC098E", x => x.id_proveedor);
                    table.ForeignKey(
                        name: "FK_PROVEEDORES_CIUDADES",
                        column: x => x.id_ciudad,
                        principalTable: "CIUDADES",
                        principalColumn: "id_ciudad");
                });

            migrationBuilder.CreateTable(
                name: "SUCURSALES",
                columns: table => new
                {
                    id_sucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ciudad = table.Column<int>(type: "int", nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    altura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SUCURSAL__4C7580136F7C1585", x => x.id_sucursal);
                    table.ForeignKey(
                        name: "FK_SUCURSALES_CIUDADES",
                        column: x => x.id_ciudad,
                        principalTable: "CIUDADES",
                        principalColumn: "id_ciudad");
                });

            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    id_obra_social = table.Column<int>(type: "int", nullable: true),
                    id_ciudad = table.Column<int>(type: "int", nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    correo_electronico = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    altura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CLIENTES__677F38F582F05F01", x => x.id_cliente);
                    table.ForeignKey(
                        name: "FK_CLIENTES_CIUDADES",
                        column: x => x.id_ciudad,
                        principalTable: "CIUDADES",
                        principalColumn: "id_ciudad");
                    table.ForeignKey(
                        name: "FK_CLIENTES_OBRAS_SOCIALES",
                        column: x => x.id_obra_social,
                        principalTable: "OBRAS_SOCIALES",
                        principalColumn: "id_obra_social");
                });

            migrationBuilder.CreateTable(
                name: "RECETAS",
                columns: table => new
                {
                    id_receta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_obra_social = table.Column<int>(type: "int", nullable: true),
                    matricula = table.Column<int>(type: "int", nullable: true),
                    fecha_vencimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    tipo_receta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RECETAS__11DB53ABF6E6DF1E", x => x.id_receta);
                    table.ForeignKey(
                        name: "FK_RECETAS_MEDICOS",
                        column: x => x.matricula,
                        principalTable: "MEDICOS",
                        principalColumn: "matricula");
                    table.ForeignKey(
                        name: "FK_RECETAS_OBRAS_SOCIALES",
                        column: x => x.id_obra_social,
                        principalTable: "OBRAS_SOCIALES",
                        principalColumn: "id_obra_social");
                });

            migrationBuilder.CreateTable(
                name: "EMPLEADOS",
                columns: table => new
                {
                    legajo_empleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_sucursal = table.Column<int>(type: "int", nullable: true),
                    id_ciudad = table.Column<int>(type: "int", nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    altura = table.Column<int>(type: "int", nullable: true),
                    correo_electronico = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    rol = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password_empleado = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EMPLEADO__DF787D2A40F63E7C", x => x.legajo_empleado);
                    table.ForeignKey(
                        name: "FK_EMPLEADOS_CIUDADES",
                        column: x => x.id_ciudad,
                        principalTable: "CIUDADES",
                        principalColumn: "id_ciudad");
                    table.ForeignKey(
                        name: "FK_EMPLEADOS_SUCURSALES",
                        column: x => x.id_sucursal,
                        principalTable: "SUCURSALES",
                        principalColumn: "id_sucursal");
                });

            migrationBuilder.CreateTable(
                name: "FACTURAS",
                columns: table => new
                {
                    nro_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cliente = table.Column<int>(type: "int", nullable: true),
                    id_sucursal = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FACTURAS__B31FA9AFDAA314A2", x => x.nro_factura);
                    table.ForeignKey(
                        name: "FK_FACTURAS_CLIENTES",
                        column: x => x.id_cliente,
                        principalTable: "CLIENTES",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "FK_FACTURAS_SUCURSALES",
                        column: x => x.id_sucursal,
                        principalTable: "SUCURSALES",
                        principalColumn: "id_sucursal");
                });

            migrationBuilder.CreateTable(
                name: "DETALLES_RECETA",
                columns: table => new
                {
                    id_detalle_receta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_receta = table.Column<int>(type: "int", nullable: true),
                    id_suministro = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DETALLES__2C99ACD3530A9E7C", x => x.id_detalle_receta);
                    table.ForeignKey(
                        name: "FK_DETALLES_RECETA_RECETAS",
                        column: x => x.id_receta,
                        principalTable: "RECETAS",
                        principalColumn: "id_receta");
                    table.ForeignKey(
                        name: "FK_DETALLES_RECETA_SUMINISTROS",
                        column: x => x.id_suministro,
                        principalTable: "SUMINISTROS",
                        principalColumn: "id_suministro");
                });

            migrationBuilder.CreateTable(
                name: "STOCKS",
                columns: table => new
                {
                    id_stock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_sucursal = table.Column<int>(type: "int", nullable: true),
                    id_proveedor = table.Column<int>(type: "int", nullable: true),
                    legajo_empleado = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STOCKS__3A39590A0C543D5F", x => x.id_stock);
                    table.ForeignKey(
                        name: "FK_STOCKS_EMPLEADOS",
                        column: x => x.legajo_empleado,
                        principalTable: "EMPLEADOS",
                        principalColumn: "legajo_empleado");
                    table.ForeignKey(
                        name: "FK_STOCKS_PROVEEDORES",
                        column: x => x.id_proveedor,
                        principalTable: "PROVEEDORES",
                        principalColumn: "id_proveedor");
                    table.ForeignKey(
                        name: "FK_STOCKS_SUCURSALES",
                        column: x => x.id_sucursal,
                        principalTable: "SUCURSALES",
                        principalColumn: "id_sucursal");
                });

            migrationBuilder.CreateTable(
                name: "DETALLES_FACTURA",
                columns: table => new
                {
                    id_detalle_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nro_factura = table.Column<int>(type: "int", nullable: true),
                    id_suministro = table.Column<int>(type: "int", nullable: true),
                    pre_venta = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DETALLES__F6BFE343A7703636", x => x.id_detalle_factura);
                    table.ForeignKey(
                        name: "FK_DETALLES_FACTURA_FACTURAS",
                        column: x => x.nro_factura,
                        principalTable: "FACTURAS",
                        principalColumn: "nro_factura");
                    table.ForeignKey(
                        name: "FK_DETALLES_FACTURA_SUMINISTROS",
                        column: x => x.id_suministro,
                        principalTable: "SUMINISTROS",
                        principalColumn: "id_suministro");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CIUDADES_id_localidad",
                table: "CIUDADES",
                column: "id_localidad");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_id_ciudad",
                table: "CLIENTES",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_id_obra_social",
                table: "CLIENTES",
                column: "id_obra_social");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLES_FACTURA_id_suministro",
                table: "DETALLES_FACTURA",
                column: "id_suministro");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLES_FACTURA_nro_factura",
                table: "DETALLES_FACTURA",
                column: "nro_factura");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLES_RECETA_id_receta",
                table: "DETALLES_RECETA",
                column: "id_receta");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLES_RECETA_id_suministro",
                table: "DETALLES_RECETA",
                column: "id_suministro");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLEADOS_id_ciudad",
                table: "EMPLEADOS",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLEADOS_id_sucursal",
                table: "EMPLEADOS",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_FACTURAS_id_cliente",
                table: "FACTURAS",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_FACTURAS_id_sucursal",
                table: "FACTURAS",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_LOCALIDADES_id_provincia",
                table: "LOCALIDADES",
                column: "id_provincia");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_SOCIALES_id_ciudad",
                table: "OBRAS_SOCIALES",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_PROVEEDORES_id_ciudad",
                table: "PROVEEDORES",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_RECETAS_id_obra_social",
                table: "RECETAS",
                column: "id_obra_social");

            migrationBuilder.CreateIndex(
                name: "IX_RECETAS_matricula",
                table: "RECETAS",
                column: "matricula");

            migrationBuilder.CreateIndex(
                name: "IX_STOCKS_id_proveedor",
                table: "STOCKS",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_STOCKS_id_sucursal",
                table: "STOCKS",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_STOCKS_legajo_empleado",
                table: "STOCKS",
                column: "legajo_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_SUCURSALES_id_ciudad",
                table: "SUCURSALES",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "IX_SUMINISTROS_id_marca",
                table: "SUMINISTROS",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_SUMINISTROS_id_tipo_distribucion",
                table: "SUMINISTROS",
                column: "id_tipo_distribucion");

            migrationBuilder.CreateIndex(
                name: "IX_SUMINISTROS_id_tipo_suministro",
                table: "SUMINISTROS",
                column: "id_tipo_suministro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DETALLES_FACTURA");

            migrationBuilder.DropTable(
                name: "DETALLES_RECETA");

            migrationBuilder.DropTable(
                name: "STOCKS");

            migrationBuilder.DropTable(
                name: "FACTURAS");

            migrationBuilder.DropTable(
                name: "RECETAS");

            migrationBuilder.DropTable(
                name: "SUMINISTROS");

            migrationBuilder.DropTable(
                name: "EMPLEADOS");

            migrationBuilder.DropTable(
                name: "PROVEEDORES");

            migrationBuilder.DropTable(
                name: "CLIENTES");

            migrationBuilder.DropTable(
                name: "MEDICOS");

            migrationBuilder.DropTable(
                name: "MARCAS");

            migrationBuilder.DropTable(
                name: "TIPOS_DISTRIBUCION");

            migrationBuilder.DropTable(
                name: "TIPOS_SUMINISTRO");

            migrationBuilder.DropTable(
                name: "SUCURSALES");

            migrationBuilder.DropTable(
                name: "OBRAS_SOCIALES");

            migrationBuilder.DropTable(
                name: "CIUDADES");

            migrationBuilder.DropTable(
                name: "LOCALIDADES");

            migrationBuilder.DropTable(
                name: "PROVINCIAS");
        }
    }
}
