using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lente.Infraestruture.Migrations
{
    /// <inheritdoc />
    public partial class novocamposlentemodelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lado",
                table: "Lente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Ponte",
                table: "Lente",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lado",
                table: "Lente");

            migrationBuilder.DropColumn(
                name: "Ponte",
                table: "Lente");
        }
    }
}
