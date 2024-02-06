using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caisse.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produits_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Nom" },
                values: new object[] { 1, "High-Tech" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Nom" },
                values: new object[] { 2, "Electroménager" });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "CategorieId", "Description", "Nom", "PicturePath", "Price", "Quantite" },
                values: new object[] { 1, 1, "Pire Arnaque du siècle", "Iphone 15", "", 1300, 10 });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "CategorieId", "Description", "Nom", "PicturePath", "Price", "Quantite" },
                values: new object[] { 2, 1, "Super téléphone", "Iphone 12", "", 500, 120 });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "CategorieId", "Description", "Nom", "PicturePath", "Price", "Quantite" },
                values: new object[] { 3, 1, "Lave plus blanc que blanc", "Machine à laver", "", 500, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Produits_CategorieId",
                table: "Produits",
                column: "CategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
