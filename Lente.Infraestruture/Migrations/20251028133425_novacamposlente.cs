using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lente.Infraestruture.Migrations
{
    /// <inheritdoc />
    public partial class novacamposlente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagonalMaior",
                table: "Lente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiagonalMaior",
                table: "Lente",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
