using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendBillboardsForAboutExtendedSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Argument",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.DropColumn(
                name: "Argument",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");
        }
    }
}
