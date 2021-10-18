using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomenclatures.Migrations
{
    public partial class DeleteCascadeCompose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Composants_Produits_ComposeId",
                table: "Composants");

            migrationBuilder.AddForeignKey(
                name: "FK_Composants_Produits_ComposeId",
                table: "Composants",
                column: "ComposeId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Composants_Produits_ComposeId",
                table: "Composants");

            migrationBuilder.AddForeignKey(
                name: "FK_Composants_Produits_ComposeId",
                table: "Composants",
                column: "ComposeId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
