using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class FixSupscriptionSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_images_ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_images_ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                column: "ImageId",
                principalSchema: "cpcc_billboard_media",
                principalTable: "images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
