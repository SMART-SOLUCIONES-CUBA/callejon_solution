using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class CommentDelationReorganizacionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commentdelations_client_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations");

            migrationBuilder.DropForeignKey(
                name: "FK_commentdelations_eventcomments_EventCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations");

            migrationBuilder.DropForeignKey(
                name: "FK_commentdelations_personalitycomments_PersonalityCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations");

            migrationBuilder.DropForeignKey(
                name: "FK_commentdelations_productioncomments_ProductionCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_commentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations");

            migrationBuilder.RenameTable(
                name: "commentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                newName: "CommentDelations");

            migrationBuilder.RenameColumn(
                name: "ProductionCommentId",
                table: "CommentDelations",
                newName: "ProductionCommentDelation_CommentId");

            migrationBuilder.RenameColumn(
                name: "PersonalityCommentId",
                table: "CommentDelations",
                newName: "PersonalityCommentDelation_CommentId");

            migrationBuilder.RenameColumn(
                name: "EventCommentId",
                table: "CommentDelations",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_commentdelations_ClientId",
                table: "CommentDelations",
                newName: "IX_CommentDelations_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_commentdelations_ProductionCommentId",
                table: "CommentDelations",
                newName: "IX_CommentDelations_ProductionCommentDelation_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_commentdelations_PersonalityCommentId",
                table: "CommentDelations",
                newName: "IX_CommentDelations_PersonalityCommentDelation_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_commentdelations_EventCommentId",
                table: "CommentDelations",
                newName: "IX_CommentDelations_CommentId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CommentDelations",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentDelations",
                table: "CommentDelations",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CommentDelations");

            migrationBuilder.RenameTable(
                name: "CommentDelations",
                newName: "commentdelations",
                newSchema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.RenameColumn(
                name: "ProductionCommentDelation_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "ProductionCommentId");

            migrationBuilder.RenameColumn(
                name: "PersonalityCommentDelation_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "PersonalityCommentId");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "EventCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDelations_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "IX_commentdelations_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDelations_ProductionCommentDelation_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "IX_commentdelations_ProductionCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDelations_PersonalityCommentDelation_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "IX_commentdelations_PersonalityCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDelations_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                newName: "IX_commentdelations_EventCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_commentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_commentdelations_client_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "ClientId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_commentdelations_eventcomments_EventCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "EventCommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "eventcomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_commentdelations_personalitycomments_PersonalityCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "PersonalityCommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "personalitycomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_commentdelations_productioncomments_ProductionCommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "commentdelations",
                column: "ProductionCommentId",
                principalSchema: "cpcc_billboard_cpccbillboard",
                principalTable: "productioncomments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
