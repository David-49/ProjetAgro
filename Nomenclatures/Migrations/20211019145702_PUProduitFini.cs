using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomenclatures.Migrations
{
    public partial class PUProduitFini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrixUnitaire",
                table: "Produits",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrixUnitaire",
                table: "Produits");
        }
    }
}
