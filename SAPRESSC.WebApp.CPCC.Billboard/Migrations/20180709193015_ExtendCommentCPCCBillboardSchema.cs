using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendCommentCPCCBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Syncronized",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Syncronized",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Syncronized",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Syncronized",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments");

            migrationBuilder.DropColumn(
                name: "Syncronized",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments");

            migrationBuilder.DropColumn(
                name: "Syncronized",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments");
        }
    }
}
