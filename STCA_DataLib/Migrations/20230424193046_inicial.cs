using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STCA_DataLib.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RangoTiempo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicial = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFinal = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangoTiempo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZonaHoraria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonaHoraria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZonaHoraria_RangoTiempo",
                columns: table => new
                {
                    ZonaHorariaId = table.Column<int>(type: "int", nullable: false),
                    RangoTiempoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonaHoraria_RangoTiempo", x => new { x.ZonaHorariaId, x.RangoTiempoId });
                    table.ForeignKey(
                        name: "FK_ZonaHoraria_RangoTiempo_RangoTiempo_RangoTiempoId",
                        column: x => x.RangoTiempoId,
                        principalTable: "RangoTiempo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZonaHoraria_RangoTiempo_ZonaHoraria_ZonaHorariaId",
                        column: x => x.ZonaHorariaId,
                        principalTable: "ZonaHoraria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZonaHoraria_RangoTiempo_RangoTiempoId",
                table: "ZonaHoraria_RangoTiempo",
                column: "RangoTiempoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZonaHoraria_RangoTiempo");

            migrationBuilder.DropTable(
                name: "RangoTiempo");

            migrationBuilder.DropTable(
                name: "ZonaHoraria");
        }
    }
}
