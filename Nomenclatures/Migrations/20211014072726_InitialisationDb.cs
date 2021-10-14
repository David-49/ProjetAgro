using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomenclatures.Migrations
{
    public partial class InitialisationDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamilleMatierePremiere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DureeOptimaleUtilisation = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilleMatierePremiere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatieresPremieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PourcentageHumidite = table.Column<int>(type: "int", nullable: false),
                    PoidsUnitaire = table.Column<double>(type: "float", nullable: false),
                    DureeConservation = table.Column<TimeSpan>(type: "time", nullable: true),
                    FamilleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatieresPremieres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatieresPremieres_FamilleMatierePremiere_FamilleId",
                        column: x => x.FamilleId,
                        principalTable: "FamilleMatierePremiere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatieresPremieres_FamilleId",
                table: "MatieresPremieres",
                column: "FamilleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatieresPremieres");

            migrationBuilder.DropTable(
                name: "FamilleMatierePremiere");
        }
    }
}
