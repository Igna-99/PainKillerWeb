using Microsoft.EntityFrameworkCore.Migrations;

namespace PainKillerWeb.Migrations
{
    public partial class PKMK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atributosDePersonajes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personajeId = table.Column<int>(nullable: false),
                    atributoId = table.Column<int>(nullable: false),
                    nivel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atributosDePersonajes", x => x.id);
                    table.ForeignKey(
                        name: "FK_atributosDePersonajes_atributos_atributoId",
                        column: x => x.atributoId,
                        principalTable: "atributos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_atributosDePersonajes_personajes_personajeId",
                        column: x => x.personajeId,
                        principalTable: "personajes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "habilidades",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false),
                    atributoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_habilidades", x => x.id);
                    table.ForeignKey(
                        name: "FK_habilidades_atributos_atributoId",
                        column: x => x.atributoId,
                        principalTable: "atributos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "habilidadDePersonajes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personajeId = table.Column<int>(nullable: false),
                    HabilidadId = table.Column<int>(nullable: false),
                    Nivel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_habilidadDePersonajes", x => x.id);
                    table.ForeignKey(
                        name: "FK_habilidadDePersonajes_habilidades_HabilidadId",
                        column: x => x.HabilidadId,
                        principalTable: "habilidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_habilidadDePersonajes_personajes_personajeId",
                        column: x => x.personajeId,
                        principalTable: "personajes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atributosDePersonajes_atributoId",
                table: "atributosDePersonajes",
                column: "atributoId");

            migrationBuilder.CreateIndex(
                name: "IX_atributosDePersonajes_personajeId",
                table: "atributosDePersonajes",
                column: "personajeId");

            migrationBuilder.CreateIndex(
                name: "IX_habilidadDePersonajes_HabilidadId",
                table: "habilidadDePersonajes",
                column: "HabilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_habilidadDePersonajes_personajeId",
                table: "habilidadDePersonajes",
                column: "personajeId");

            migrationBuilder.CreateIndex(
                name: "IX_habilidades_atributoId",
                table: "habilidades",
                column: "atributoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atributosDePersonajes");

            migrationBuilder.DropTable(
                name: "habilidadDePersonajes");

            migrationBuilder.DropTable(
                name: "habilidades");
        }
    }
}
