using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class EventsMinorExtensionCinemaBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Organizer",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Header",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Header",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.AlterColumn<string>(
                name: "Organizer",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
