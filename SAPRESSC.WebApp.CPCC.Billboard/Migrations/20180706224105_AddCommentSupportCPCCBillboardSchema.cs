using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class AddCommentSupportCPCCBillboardSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    Sex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "eventcomments",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Banish = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<Guid>(nullable: true),
                    Message = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventcomments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventcomments_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_eventcomments_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personalitycomments",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Banish = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 255, nullable: false),
                    PersonalityId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalitycomments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personalitycomments_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personalitycomments_persons_PersonalityId",
                        column: x => x.PersonalityId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productioncomments",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Banish = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 255, nullable: false),
                    ProductionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productioncomments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productioncomments_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productioncomments_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "commentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    EventCommentId = table.Column<Guid>(nullable: true),
                    Note = table.Column<string>(maxLength: 255, nullable: false),
                    PersonalityCommentId = table.Column<Guid>(nullable: true),
                    ProductionCommentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commentdelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_commentdelations_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_commentdelations_eventcomments_EventCommentId",
                        column: x => x.EventCommentId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "eventcomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_commentdelations_personalitycomments_PersonalityCommentId",
                        column: x => x.PersonalityCommentId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "personalitycomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_commentdelations_productioncomments_ProductionCommentId",
                        column: x => x.ProductionCommentId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "productioncomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_ImageId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_commentdelations_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_commentdelations_EventCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "EventCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_commentdelations_PersonalityCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "PersonalityCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_commentdelations_ProductionCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "ProductionCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_eventcomments_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_eventcomments_EventId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_personalitycomments_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_personalitycomments_PersonalityId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_productioncomments_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_productioncomments_ProductionId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                column: "ProductionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commentdelations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "eventcomments",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "personalitycomments",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "productioncomments",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "client",
                schema: "cpcc_billboard_cpccbillboard");
        }
    }
}
