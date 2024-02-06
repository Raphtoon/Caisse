using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caisse.Migrations
{
    public partial class DateSeedingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategorieId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategorieId",
                value: 1);
        }
    }
}
