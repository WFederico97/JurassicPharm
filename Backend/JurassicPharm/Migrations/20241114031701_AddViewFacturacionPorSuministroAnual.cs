using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JurassicPharm.Migrations
{
    /// <inheritdoc />
    public partial class AddViewFacturacionPorSuministroAnual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[VIEW_FACTURACION_POR_SUMINISTRO_ANUAL]
                AS
                SELECT
                    S.nombre AS 'Suministro',
                    YEAR(F.fecha) AS 'Año',
                    SUM(DF.pre_venta * DF.cantidad) AS 'TotalFacturado'
                FROM DETALLES_FACTURA DF
                JOIN SUMINISTROS S ON DF.id_suministro = S.id_suministro
                JOIN FACTURAS F ON DF.nro_factura = F.nro_factura
                WHERE YEAR(F.fecha) = YEAR(GETDATE())
                GROUP BY S.nombre, YEAR(F.fecha);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[VIEW_FACTURACION_POR_SUMINISTRO_ANUAL]");
        }
    }
}
