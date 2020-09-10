using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class CompleteMediatecaSupportSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advertisements",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    PlacardImageId = table.Column<Guid>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_advertisements_images_PlacardImageId",
                        column: x => x.PlacardImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscriptions_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "searchs",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubscriptionId = table.Column<string>(nullable: true),
                    TitleTemathic = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    ProducerHouseId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_searchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_searchs_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_searchs_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_searchs_organizations_ProducerHouseId",
                        column: x => x.ProducerHouseId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_searchs_subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subscriptiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    SubscriptionId = table.Column<string>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptiongenrebridges", x => new { x.SubscriptionId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_subscriptiongenrebridges_genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptiongenrebridges_subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptionproducerhousebridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    SubscriptionId = table.Column<string>(nullable: false),
                    ProducerHouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptionproducerhousebridges", x => new { x.SubscriptionId, x.ProducerHouseId });
                    table.ForeignKey(
                        name: "FK_subscriptionproducerhousebridges_organizations_ProducerHouse~",
                        column: x => x.ProducerHouseId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptionproducerhousebridges_subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptiontagbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    SubscriptionId = table.Column<string>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptiontagbridges", x => new { x.SubscriptionId, x.TagId });
                    table.ForeignKey(
                        name: "FK_subscriptiontagbridges_subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptiontagbridges_tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "searchgenrebridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    SearchId = table.Column<long>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_searchgenrebridges", x => new { x.SearchId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_searchgenrebridges_genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_searchgenrebridges_searchs_SearchId",
                        column: x => x.SearchId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "searchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "searchpersonalitybridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    SearchId = table.Column<long>(nullable: false),
                    PersonalityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_searchpersonalitybridges", x => new { x.SearchId, x.PersonalityId });
                    table.ForeignKey(
                        name: "FK_searchpersonalitybridges_persons_PersonalityId",
                        column: x => x.PersonalityId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_searchpersonalitybridges_searchs_SearchId",
                        column: x => x.SearchId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "searchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "searchtagbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    SearchId = table.Column<long>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_searchtagbridges", x => new { x.SearchId, x.TagId });
                    table.ForeignKey(
                        name: "FK_searchtagbridges_searchs_SearchId",
                        column: x => x.SearchId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "searchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_searchtagbridges_tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_advertisements_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "advertisements",
                column: "PlacardImageId");

            migrationBuilder.CreateIndex(
                name: "IX_searchgenrebridges_GenreId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchgenrebridges",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_searchpersonalitybridges_PersonalityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchpersonalitybridges",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_searchs_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_searchs_CountryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_searchs_ProducerHouseId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchs",
                column: "ProducerHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_searchs_SubscriptionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchs",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_searchtagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "searchtagbridges",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptiongenrebridges_GenreId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptiongenrebridges",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptionproducerhousebridges_ProducerHouseId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptionproducerhousebridges",
                column: "ProducerHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_ImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptiontagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptiontagbridges",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advertisements",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "searchgenrebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "searchpersonalitybridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "searchtagbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "subscriptiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "subscriptionproducerhousebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "subscriptiontagbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "searchs",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "subscriptions",
                schema: "cpcc_billboard_cinemabillboard");
        }
    }
}
