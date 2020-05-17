using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendFixScreenConfigurationEntitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaylistSidebarItemsCount",
                schema: "cpcc_billboard_cpccbillboard",
                table: "fixscreenclientconfigurations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistSidebarItemsCount",
                schema: "cpcc_billboard_cpccbillboard",
                table: "fixscreenclientconfigurations");
        }
    }
}
