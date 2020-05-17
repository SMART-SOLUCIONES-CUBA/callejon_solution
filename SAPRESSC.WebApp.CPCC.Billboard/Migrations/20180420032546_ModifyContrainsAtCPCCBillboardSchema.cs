using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ModifyContrainsAtCPCCBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_persons_PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.DropColumn(
                name: "PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organizer",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_events_PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PresentatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_persons_PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PresentatorId",
                principalSchema: "cpcc_billboard_identity",
                principalTable: "persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
