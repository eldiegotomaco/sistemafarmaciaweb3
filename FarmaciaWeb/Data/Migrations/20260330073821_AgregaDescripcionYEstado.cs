using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmaciaWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregaDescripcionYEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Medicamentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Medicamentos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Medicamentos");
        }
    }
}
