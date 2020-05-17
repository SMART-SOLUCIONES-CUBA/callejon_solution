using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class UpdateClientConstrainsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
