using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nomenclatures.Migrations
{
    public partial class VersionRecup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamillesPremieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: true),
                    DureeOptimaleUtilisation = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamillesPremieres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<bool>(type: "boolean", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatieresPremieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PourcentageHumidite = table.Column<int>(type: "integer", nullable: false),
                    PoidsUnitaire = table.Column<double>(type: "double precision", nullable: false),
                    DureeConservation = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FamilleId = table.Column<int>(type: "integer", nullable: true),
                    Bio = table.Column<bool>(type: "boolean", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Qty = table.Column<double>(type: "double precision", nullable: false),
                    MPId = table.Column<int>(type: "integer", nullable: true),
                    PSFId = table.Column<int>(type: "integer", nullable: true),
                    ComposeId = table.Column<int>(type: "integer", nullable: true)
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
                        name: "FK_Composants_Produits_ComposeId",
                        column: x => x.ComposeId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Composants_Produits_PSFId",
                        column: x => x.PSFId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Composants_ComposeId",
                table: "Composants",
                column: "ComposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Composants_MPId",
                table: "Composants",
                column: "MPId");

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
