using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendCPCCBillboardSchemaForRecordMobileDbDownload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "portabledbdownloads",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    UserAgent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portabledbdownloads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portabledbdownloads",
                schema: "cpcc_billboard_cpccbillboard");
        }
    }
}
