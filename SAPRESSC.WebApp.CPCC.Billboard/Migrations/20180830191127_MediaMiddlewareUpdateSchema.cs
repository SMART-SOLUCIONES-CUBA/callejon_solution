using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class MediaMiddlewareUpdateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "cpcc_billboard_media",
                table: "images",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Height",
                schema: "cpcc_billboard_media",
                table: "images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                schema: "cpcc_billboard_media",
                table: "images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "videos",
                schema: "cpcc_billboard_media",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true),
                    AudioChannelOutput = table.Column<string>(nullable: false),
                    AudioFormat = table.Column<string>(nullable: false),
                    ColorModel = table.Column<string>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Format = table.Column<string>(nullable: false),
                    Fps = table.Column<double>(nullable: false),
                    FrameSize = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    ThumbnailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_videos_thumbnails_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "thumbnails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_videos_ThumbnailId",
                schema: "cpcc_billboard_media",
                table: "videos",
                column: "ThumbnailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "videos",
                schema: "cpcc_billboard_media");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "cpcc_billboard_media",
                table: "images");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "cpcc_billboard_media",
                table: "images");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "cpcc_billboard_media",
                table: "images");
        }
    }
}
