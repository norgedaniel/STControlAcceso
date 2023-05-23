using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STCA_DataLib.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RANGO_TIEMPO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoraInicial = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFinal = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RANGO_TIEMPO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZONA_HORARIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZONA_HORARIA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZONA_HORARIA_RANGO_TIEMPO",
                columns: table => new
                {
                    ZonaHorariaId = table.Column<int>(type: "int", nullable: false),
                    RangoTiempoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZONA_HORARIA_RANGO_TIEMPO", x => new { x.ZonaHorariaId, x.RangoTiempoId });
                    table.ForeignKey(
                        name: "FK_ZONA_HORARIA_RANGO_TIEMPO_RANGO_TIEMPO_RangoTiempoId",
                        column: x => x.RangoTiempoId,
                        principalTable: "RANGO_TIEMPO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZONA_HORARIA_RANGO_TIEMPO_ZONA_HORARIA_ZonaHorariaId",
                        column: x => x.ZonaHorariaId,
                        principalTable: "ZONA_HORARIA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RANGO_TIEMPO_DiaSemana_HoraInicial_HoraFinal",
                table: "RANGO_TIEMPO",
                columns: new[] { "DiaSemana", "HoraInicial", "HoraFinal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZONA_HORARIA_Nombre",
                table: "ZONA_HORARIA",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZONA_HORARIA_RANGO_TIEMPO_RangoTiempoId",
                table: "ZONA_HORARIA_RANGO_TIEMPO",
                column: "RangoTiempoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZONA_HORARIA_RANGO_TIEMPO");

            migrationBuilder.DropTable(
                name: "RANGO_TIEMPO");

            migrationBuilder.DropTable(
                name: "ZONA_HORARIA");
        }
    }
}
