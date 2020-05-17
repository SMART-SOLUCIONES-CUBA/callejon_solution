using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class CinemaProductionMediaVideoRelationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productions_TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "TrailerVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_videos_TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "TrailerVideoId",
                principalSchema: "cpcc_billboard_media",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_videos_TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropIndex(
                name: "IX_productions_TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");
        }
    }
}
