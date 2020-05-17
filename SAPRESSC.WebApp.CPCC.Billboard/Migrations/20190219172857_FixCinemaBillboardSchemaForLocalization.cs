using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class FixCinemaBillboardSchemaForLocalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LocalizationLongitude",
                schema: "cpcc_billboard_identity",
                table: "organizations",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LocalizationLatitude",
                schema: "cpcc_billboard_identity",
                table: "organizations",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "LocalizationLongitude",
                schema: "cpcc_billboard_identity",
                table: "organizations",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LocalizationLatitude",
                schema: "cpcc_billboard_identity",
                table: "organizations",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
