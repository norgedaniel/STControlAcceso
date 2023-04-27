using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STCA_DataLib.Migrations
{
    /// <inheritdoc />
    public partial class ZonaHoraria_AK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ZonaHoraria",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ZonaHoraria_Nombre",
                table: "ZonaHoraria",
                column: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ZonaHoraria_Nombre",
                table: "ZonaHoraria");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ZonaHoraria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
