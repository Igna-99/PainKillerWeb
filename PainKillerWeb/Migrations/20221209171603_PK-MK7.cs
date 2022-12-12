using Microsoft.EntityFrameworkCore.Migrations;

namespace PainKillerWeb.Migrations
{
    public partial class PKMK7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "distancias",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_distancias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "elementos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elementos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hechizos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false),
                    duracion = table.Column<string>(nullable: true),
                    costeExp = table.Column<int>(nullable: false),
                    costeUso = table.Column<int>(nullable: false),
                    tipoCoste = table.Column<int>(nullable: false),
                    efecto = table.Column<string>(nullable: false),
                    tiempo = table.Column<string>(nullable: false),
                    distanciaId = table.Column<int>(nullable: false),
                    elementoId = table.Column<int>(nullable: false),
                    Hechizoid = table.Column<int>(nullable: true),
                    Personajeid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hechizos", x => x.id);
                    table.ForeignKey(
                        name: "FK_hechizos_hechizos_Hechizoid",
                        column: x => x.Hechizoid,
                        principalTable: "hechizos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hechizos_personajes_Personajeid",
                        column: x => x.Personajeid,
                        principalTable: "personajes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hechizos_distancias_distanciaId",
                        column: x => x.distanciaId,
                        principalTable: "distancias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hechizos_elementos_elementoId",
                        column: x => x.elementoId,
                        principalTable: "elementos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hechizos_Hechizoid",
                table: "hechizos",
                column: "Hechizoid");

            migrationBuilder.CreateIndex(
                name: "IX_hechizos_Personajeid",
                table: "hechizos",
                column: "Personajeid");

            migrationBuilder.CreateIndex(
                name: "IX_hechizos_distanciaId",
                table: "hechizos",
                column: "distanciaId");

            migrationBuilder.CreateIndex(
                name: "IX_hechizos_elementoId",
                table: "hechizos",
                column: "elementoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hechizos");

            migrationBuilder.DropTable(
                name: "distancias");

            migrationBuilder.DropTable(
                name: "elementos");
        }
    }
}
