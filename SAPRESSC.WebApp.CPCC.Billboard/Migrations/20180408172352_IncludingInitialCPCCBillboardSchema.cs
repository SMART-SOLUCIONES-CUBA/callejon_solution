using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class IncludingInitialCPCCBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_cpccbillboard");

            migrationBuilder.CreateTable(
                name: "advises",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: false),
                    PosterImageId = table.Column<Guid>(nullable: false),
                    PresentatorId = table.Column<long>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Thematic = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_images_PosterImageId",
                        column: x => x.PosterImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_persons_PresentatorId",
                        column: x => x.PresentatorId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixscreenclientconfigurations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdvisesChainPlaceholder = table.Column<string>(maxLength: 255, nullable: false),
                    AdvisesChainWellcome = table.Column<string>(maxLength: 100, nullable: true),
                    DailyRoomBillboardDisplayTime = table.Column<int>(nullable: false),
                    DailyRoomBillboardItemMaxCount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    EventDetailDisplayTime = table.Column<int>(nullable: false),
                    PresentationDetailDisplayTime = table.Column<int>(nullable: false),
                    ShowEvents = table.Column<bool>(nullable: false),
                    ShowPresentations = table.Column<bool>(nullable: false),
                    StageOverviewDisplayTime = table.Column<int>(nullable: false),
                    StartUpDelayTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fixscreenclientconfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eventoersonalitybridges",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    PersonalityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventoersonalitybridges", x => new { x.EventId, x.PersonalityId });
                    table.ForeignKey(
                        name: "FK_eventoersonalitybridges_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventoersonalitybridges_persons_PersonalityId",
                        column: x => x.PersonalityId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventoersonalitybridges_PersonalityId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventoersonalitybridges",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_events_PosterImageId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PosterImageId");

            migrationBuilder.CreateIndex(
                name: "IX_events_PresentatorId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PresentatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advises",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "eventoersonalitybridges",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "fixscreenclientconfigurations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "events",
                schema: "cpcc_billboard_cpccbillboard");
        }
    }
}
