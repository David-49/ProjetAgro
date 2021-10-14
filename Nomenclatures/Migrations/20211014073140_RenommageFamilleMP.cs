using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomenclatures.Migrations
{
    public partial class RenommageFamilleMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatieresPremieres_FamilleMatierePremiere_FamilleId",
                table: "MatieresPremieres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilleMatierePremiere",
                table: "FamilleMatierePremiere");

            migrationBuilder.RenameTable(
                name: "FamilleMatierePremiere",
                newName: "FamillesPremieres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamillesPremieres",
                table: "FamillesPremieres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatieresPremieres_FamillesPremieres_FamilleId",
                table: "MatieresPremieres",
                column: "FamilleId",
                principalTable: "FamillesPremieres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatieresPremieres_FamillesPremieres_FamilleId",
                table: "MatieresPremieres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamillesPremieres",
                table: "FamillesPremieres");

            migrationBuilder.RenameTable(
                name: "FamillesPremieres",
                newName: "FamilleMatierePremiere");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilleMatierePremiere",
                table: "FamilleMatierePremiere",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatieresPremieres_FamilleMatierePremiere_FamilleId",
                table: "MatieresPremieres",
                column: "FamilleId",
                principalTable: "FamilleMatierePremiere",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
