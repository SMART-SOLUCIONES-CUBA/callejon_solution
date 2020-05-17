using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class CpccEventMediaVideoRelationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PromoVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_videos_PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PromoVideoId",
                principalSchema: "cpcc_billboard_media",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_videos_PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");

            migrationBuilder.DropColumn(
                name: "PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events");
        }
    }
}
