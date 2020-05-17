using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendCPCCBillboardSchemaForEventPresenterPersonality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Header",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPresenter",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventoersonalitybridges",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPresenter",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventoersonalitybridges");

            migrationBuilder.AlterColumn<string>(
                name: "Header",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
