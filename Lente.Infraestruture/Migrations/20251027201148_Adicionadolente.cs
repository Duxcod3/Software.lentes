using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lente.Infraestruture.Migrations
{
    /// <inheritdoc />
    public partial class Adicionadolente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horizontal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vertical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Diagonal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiagonalMaior = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lente_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lente_UsuarioId",
                table: "Lente",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lente");
        }
    }
}
