using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicoss.Migrations
{
    /// <inheritdoc />
    public partial class Inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestamos",
                table: "Prestamos");

            migrationBuilder.RenameTable(
                name: "Prestamos",
                newName: "Tecnico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnico",
                table: "Tecnico",
                column: "TecnicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnico",
                table: "Tecnico");

            migrationBuilder.RenameTable(
                name: "Tecnico",
                newName: "Prestamos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestamos",
                table: "Prestamos",
                column: "TecnicoId");
        }
    }
}
