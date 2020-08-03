using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class RequireRenamingSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                newName: "ClasificationId");

            migrationBuilder.RenameIndex(
                name: "IX_productions_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                newName: "IX_productions_ClasificationId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_categories_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.RenameColumn(
                name: "ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_productions_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                newName: "IX_productions_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
