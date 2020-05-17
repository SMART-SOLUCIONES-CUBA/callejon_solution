using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ModifyContrainsAtCinemaBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "LocalizedTitle",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LocalizedTitle",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
