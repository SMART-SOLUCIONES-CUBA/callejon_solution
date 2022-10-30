using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendSubscription3SchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceAvailabilityFrom",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceAvailabilityTo",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceAvailabilityFrom",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "ServiceAvailabilityTo",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");
        }
    }
}
