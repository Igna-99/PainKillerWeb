using Microsoft.EntityFrameworkCore.Migrations;

namespace PainKillerWeb.Migrations
{
    public partial class PKMK91 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "itemsDePersonajes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personajeId = table.Column<int>(nullable: false),
                    itemId = table.Column<int>(nullable: false),
                    cantidad = table.Column<int>(nullable: false),
                    descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemsDePersonajes", x => x.id);
                    table.ForeignKey(
                        name: "FK_itemsDePersonajes_items_itemId",
                        column: x => x.itemId,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemsDePersonajes_personajes_personajeId",
                        column: x => x.personajeId,
                        principalTable: "personajes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_itemsDePersonajes_itemId",
                table: "itemsDePersonajes",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_itemsDePersonajes_personajeId",
                table: "itemsDePersonajes",
                column: "personajeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemsDePersonajes");

            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
