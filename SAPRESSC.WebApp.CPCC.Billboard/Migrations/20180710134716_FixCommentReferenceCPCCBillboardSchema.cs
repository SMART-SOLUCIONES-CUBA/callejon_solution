using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class FixCommentReferenceCPCCBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productioncomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_personalitycomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_eventcomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                column: "ReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_eventcomments_eventcomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                column: "ReferenceId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "eventcomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personalitycomments_personalitycomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                column: "ReferenceId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "personalitycomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productioncomments_productioncomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                column: "ReferenceId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "productioncomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventcomments_eventcomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments");

            migrationBuilder.DropForeignKey(
                name: "FK_personalitycomments_personalitycomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments");

            migrationBuilder.DropForeignKey(
                name: "FK_productioncomments_productioncomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments");

            migrationBuilder.DropIndex(
                name: "IX_productioncomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments");

            migrationBuilder.DropIndex(
                name: "IX_personalitycomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments");

            migrationBuilder.DropIndex(
                name: "IX_eventcomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments");
        }
    }
}
