using Microsoft.EntityFrameworkCore.Migrations;

namespace PainKillerWeb.Migrations
{
    public partial class PKMK1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atributos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atributos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "raza",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false),
                    idAtributoRelevante = table.Column<int>(nullable: false),
                    atributoRelevanteid = table.Column<int>(nullable: true),
                    idAtributoRelevante2 = table.Column<int>(nullable: false),
                    atributoRelevante2id = table.Column<int>(nullable: true),
                    idAtributoPesimo = table.Column<int>(nullable: false),
                    atributoPesimoid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raza", x => x.id);
                    table.ForeignKey(
                        name: "FK_raza_atributos_atributoPesimoid",
                        column: x => x.atributoPesimoid,
                        principalTable: "atributos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_raza_atributos_atributoRelevante2id",
                        column: x => x.atributoRelevante2id,
                        principalTable: "atributos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_raza_atributos_atributoRelevanteid",
                        column: x => x.atributoRelevanteid,
                        principalTable: "atributos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personajes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(maxLength: 21, nullable: false),
                    razaId = table.Column<int>(nullable: false),
                    expActual = table.Column<int>(nullable: false),
                    expGastada = table.Column<int>(nullable: false),
                    vidaMax = table.Column<int>(nullable: false),
                    manaMax = table.Column<int>(nullable: false),
                    energiaMax = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personajes", x => x.id);
                    table.ForeignKey(
                        name: "FK_personajes_raza_razaId",
                        column: x => x.razaId,
                        principalTable: "raza",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_personajes_razaId",
                table: "personajes",
                column: "razaId");

            migrationBuilder.CreateIndex(
                name: "IX_raza_atributoPesimoid",
                table: "raza",
                column: "atributoPesimoid");

            migrationBuilder.CreateIndex(
                name: "IX_raza_atributoRelevante2id",
                table: "raza",
                column: "atributoRelevante2id");

            migrationBuilder.CreateIndex(
                name: "IX_raza_atributoRelevanteid",
                table: "raza",
                column: "atributoRelevanteid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personajes");

            migrationBuilder.DropTable(
                name: "raza");

            migrationBuilder.DropTable(
                name: "atributos");
        }
    }
}
