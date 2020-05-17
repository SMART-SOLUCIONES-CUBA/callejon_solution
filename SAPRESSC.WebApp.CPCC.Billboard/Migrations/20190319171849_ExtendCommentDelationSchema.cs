using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendCommentDelationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Revoked",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Revoked",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycommentdelations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Revoked",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcommentdelations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revoked",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations");

            migrationBuilder.DropColumn(
                name: "Revoked",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycommentdelations");

            migrationBuilder.DropColumn(
                name: "Revoked",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcommentdelations");
        }
    }
}
