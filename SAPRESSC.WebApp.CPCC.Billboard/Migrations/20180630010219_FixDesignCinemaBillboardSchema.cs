using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class FixDesignCinemaBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionRole",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "countrycoproductionbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    ProductionId = table.Column<long>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countrycoproductionbridges", x => new { x.ProductionId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_countrycoproductionbridges_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_countrycoproductionbridges_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_countrycoproductionbridges_CountryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "countrycoproductionbridges",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "countrycoproductionbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings");

            migrationBuilder.AddColumn<int>(
                name: "ProductionRole",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
