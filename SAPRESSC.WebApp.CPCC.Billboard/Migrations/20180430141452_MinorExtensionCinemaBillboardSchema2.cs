using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class MinorExtensionCinemaBillboardSchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "cpcc_billboard_cinemabillboard",
                table: "presentations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "cpcc_billboard_cinemabillboard",
                table: "presentations");

            migrationBuilder.AlterColumn<long>(
                name: "Duration",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
