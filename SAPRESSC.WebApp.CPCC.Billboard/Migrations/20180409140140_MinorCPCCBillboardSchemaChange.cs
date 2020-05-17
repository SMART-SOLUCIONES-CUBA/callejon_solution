using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class MinorCPCCBillboardSchemaChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StageOverviewDisplayTime",
                schema: "cpcc_billboard_cpccbillboard",
                table: "fixscreenclientconfigurations",
                newName: "CinemaOverviewDisplayTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CinemaOverviewDisplayTime",
                schema: "cpcc_billboard_cpccbillboard",
                table: "fixscreenclientconfigurations",
                newName: "StageOverviewDisplayTime");
        }
    }
}
