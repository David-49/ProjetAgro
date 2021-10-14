using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomenclatures.Migrations
{
    public partial class MigrationInitiale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamillesPremieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DureeOptimaleUtilisation = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamillesPremieres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
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
                        name: "FK_MatieresPremieres_FamillesPremieres_FamilleId",
                        column: x => x.FamilleId,
                        principalTable: "FamillesPremieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Composants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<double>(type: "float", nullable: false),
                    MPId = table.Column<int>(type: "int", nullable: true),
                    PSFId = table.Column<int>(type: "int", nullable: true),
                    ProduitFiniId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Composants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Composants_MatieresPremieres_MPId",
                        column: x => x.MPId,
                        principalTable: "MatieresPremieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Composants_Produits_ProduitFiniId",
                        column: x => x.ProduitFiniId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Composants_Produits_PSFId",
                        column: x => x.PSFId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Composants_MPId",
                table: "Composants",
                column: "MPId");

            migrationBuilder.CreateIndex(
                name: "IX_Composants_ProduitFiniId",
                table: "Composants",
                column: "ProduitFiniId");

            migrationBuilder.CreateIndex(
                name: "IX_Composants_PSFId",
                table: "Composants",
                column: "PSFId");

            migrationBuilder.CreateIndex(
                name: "IX_MatieresPremieres_FamilleId",
                table: "MatieresPremieres",
                column: "FamilleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Composants");

            migrationBuilder.DropTable(
                name: "MatieresPremieres");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "FamillesPremieres");
        }
    }
}
