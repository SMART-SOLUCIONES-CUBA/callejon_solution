using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendClientCPCCBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BornDate",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client");

            migrationBuilder.DropColumn(
                name: "BornDate",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client");
        }
    }
}
