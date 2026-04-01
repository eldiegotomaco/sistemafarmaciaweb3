using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmaciaWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class PermitirEstanteNulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Estantes_EstanteId",
                table: "Medicamentos");

            migrationBuilder.AlterColumn<int>(
                name: "EstanteId",
                table: "Medicamentos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Estantes_EstanteId",
                table: "Medicamentos",
                column: "EstanteId",
                principalTable: "Estantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Estantes_EstanteId",
                table: "Medicamentos");

            migrationBuilder.AlterColumn<int>(
                name: "EstanteId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Estantes_EstanteId",
                table: "Medicamentos",
                column: "EstanteId",
                principalTable: "Estantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
