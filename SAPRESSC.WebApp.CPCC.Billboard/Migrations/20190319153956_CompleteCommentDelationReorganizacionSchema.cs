using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class CompleteCommentDelationReorganizacionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentDelations_client_ClientId",
                table: "CommentDelations");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDelations_eventcomments_CommentId",
                table: "CommentDelations");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDelations_personalitycomments_PersonalityCommentDelat~",
                table: "CommentDelations");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDelations_productioncomments_ProductionCommentDelatio~",
                table: "CommentDelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentDelations",
                table: "CommentDelations");

            migrationBuilder.DropIndex(
                name: "IX_CommentDelations_PersonalityCommentDelation_CommentId",
                table: "CommentDelations");

            migrationBuilder.DropIndex(
                name: "IX_CommentDelations_ProductionCommentDelation_CommentId",
                table: "CommentDelations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CommentDelations");

            migrationBuilder.DropColumn(
                name: "PersonalityCommentDelation_CommentId",
                table: "CommentDelations");

            migrationBuilder.DropColumn(
                name: "ProductionCommentDelation_CommentId",
                table: "CommentDelations");

            migrationBuilder.RenameTable(
                name: "CommentDelations",
                newName: "productioncommentdelations",
                newSchema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDelations_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                newName: "IX_productioncommentdelations_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDelations_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                newName: "IX_productioncommentdelations_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productioncommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "eventcommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    CommentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventcommentdelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventcommentdelations_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_eventcommentdelations_eventcomments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "eventcomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personalitycommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    CommentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalitycommentdelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personalitycommentdelations_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personalitycommentdelations_personalitycomments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "personalitycomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventcommentdelations_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcommentdelations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_eventcommentdelations_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcommentdelations",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_personalitycommentdelations_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycommentdelations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_personalitycommentdelations_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycommentdelations",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_productioncommentdelations_client_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                column: "ClientId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productioncommentdelations_productioncomments_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                column: "CommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "productioncomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productioncommentdelations_client_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations");

            migrationBuilder.DropForeignKey(
                name: "FK_productioncommentdelations_productioncomments_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations");

            migrationBuilder.DropTable(
                name: "eventcommentdelations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "personalitycommentdelations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productioncommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations");

            migrationBuilder.RenameTable(
                name: "productioncommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                newName: "CommentDelations");

            migrationBuilder.RenameIndex(
                name: "IX_productioncommentdelations_CommentId",
                table: "CommentDelations",
                newName: "IX_CommentDelations_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_productioncommentdelations_ClientId",
                table: "CommentDelations",
                newName: "IX_CommentDelations_ClientId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CommentDelations",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalityCommentDelation_CommentId",
                table: "CommentDelations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionCommentDelation_CommentId",
                table: "CommentDelations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentDelations",
                table: "CommentDelations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDelations_PersonalityCommentDelation_CommentId",
                table: "CommentDelations",
                column: "PersonalityCommentDelation_CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDelations_ProductionCommentDelation_CommentId",
                table: "CommentDelations",
                column: "ProductionCommentDelation_CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDelations_client_ClientId",
                table: "CommentDelations",
                column: "ClientId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDelations_eventcomments_CommentId",
                table: "CommentDelations",
                column: "CommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "eventcomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDelations_personalitycomments_PersonalityCommentDelat~",
                table: "CommentDelations",
                column: "PersonalityCommentDelation_CommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "personalitycomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDelations_productioncomments_ProductionCommentDelatio~",
                table: "CommentDelations",
                column: "ProductionCommentDelation_CommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "productioncomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
