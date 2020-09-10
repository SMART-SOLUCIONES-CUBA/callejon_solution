using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class MinorAdjustmentMediatecaSupportSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories");

            migrationBuilder.AddColumn<DateTime>(
                name: "At",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                column: "ParentId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "At",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchs");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                column: "ParentId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
