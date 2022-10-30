using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendSubscriptionSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailAuthenticationCount",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSignInAt",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PasswordChangeRequired",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_persons_IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                column: "IdentityId",
                principalSchema: "cpcc_billboard_identity",
                principalTable: "persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_persons_IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "FailAuthenticationCount",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "LastSignInAt",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "PasswordChangeRequired",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions");
        }
    }
}
