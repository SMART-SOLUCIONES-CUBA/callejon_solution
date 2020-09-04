using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class UpgradeCompatibilitySupportSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_collections_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_collections_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CollectionId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_collections_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_collections_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CollectionId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
