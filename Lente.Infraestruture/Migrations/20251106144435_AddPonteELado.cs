using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lente.Infraestruture.Migrations
{
    /// <inheritdoc />
    public partial class AddPonteELado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Diagonal",
                table: "Lente",
                newName: "DiagonalMaior");

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "Lente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job",
                table: "Lente");

            migrationBuilder.RenameColumn(
                name: "DiagonalMaior",
                table: "Lente",
                newName: "Diagonal");
        }
    }
}
