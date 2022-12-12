using Microsoft.EntityFrameworkCore.Migrations;

namespace PainKillerWeb.Migrations
{
    public partial class PKMK8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hechizos_personajes_Personajeid",
                table: "hechizos");

            migrationBuilder.DropIndex(
                name: "IX_hechizos_Personajeid",
                table: "hechizos");

            migrationBuilder.DropColumn(
                name: "Personajeid",
                table: "hechizos");

            migrationBuilder.CreateTable(
                name: "hechizosDePersonajes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personajeId = table.Column<int>(nullable: false),
                    HechizoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hechizosDePersonajes", x => x.id);
                    table.ForeignKey(
                        name: "FK_hechizosDePersonajes_hechizos_HechizoId",
                        column: x => x.HechizoId,
                        principalTable: "hechizos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hechizosDePersonajes_personajes_personajeId",
                        column: x => x.personajeId,
                        principalTable: "personajes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hechizosDePersonajes_HechizoId",
                table: "hechizosDePersonajes",
                column: "HechizoId");

            migrationBuilder.CreateIndex(
                name: "IX_hechizosDePersonajes_personajeId",
                table: "hechizosDePersonajes",
                column: "personajeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hechizosDePersonajes");

            migrationBuilder.AddColumn<int>(
                name: "Personajeid",
                table: "hechizos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_hechizos_Personajeid",
                table: "hechizos",
                column: "Personajeid");

            migrationBuilder.AddForeignKey(
                name: "FK_hechizos_personajes_Personajeid",
                table: "hechizos",
                column: "Personajeid",
                principalTable: "personajes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
