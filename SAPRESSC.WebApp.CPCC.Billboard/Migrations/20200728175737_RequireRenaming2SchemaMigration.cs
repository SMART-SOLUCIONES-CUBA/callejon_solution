using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class RequireRenaming2SchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_categories_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                schema: "cpcc_billboard_cinemabillboard",
                newName: "clasifications",
                newSchema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clasifications",
                schema: "cpcc_billboard_cinemabillboard",
                table: "clasifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_clasifications_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "ClasificationId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "clasifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_clasifications_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clasifications",
                schema: "cpcc_billboard_cinemabillboard",
                table: "clasifications");

            migrationBuilder.RenameTable(
                name: "clasifications",
                schema: "cpcc_billboard_cinemabillboard",
                newName: "categories",
                newSchema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_categories_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "ClasificationId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
