using Microsoft.EntityFrameworkCore.Migrations;

namespace PainKillerWeb.Migrations
{
    public partial class PKM5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "energiaAct",
                table: "personajes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "manaAct",
                table: "personajes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "vidaAct",
                table: "personajes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "energiaAct",
                table: "personajes");

            migrationBuilder.DropColumn(
                name: "manaAct",
                table: "personajes");

            migrationBuilder.DropColumn(
                name: "vidaAct",
                table: "personajes");
        }
    }
}
