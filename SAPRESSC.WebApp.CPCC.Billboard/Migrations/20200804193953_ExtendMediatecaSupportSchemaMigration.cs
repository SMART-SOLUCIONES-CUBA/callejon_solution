using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendMediatecaSupportSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "collectioncheckouts",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubscriptorId = table.Column<string>(nullable: false),
                    At = table.Column<DateTime>(nullable: false),
                    CollectionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectioncheckouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_collectioncheckouts_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collectiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    CollectionId = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectiongenrebridges", x => new { x.CollectionId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_collectiongenrebridges_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collectiongenrebridges_genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collectionproducerhousebridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    CollectionId = table.Column<Guid>(nullable: false),
                    ProducerHouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectionproducerhousebridges", x => new { x.CollectionId, x.ProducerHouseId });
                    table.ForeignKey(
                        name: "FK_collectionproducerhousebridges_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collectionproducerhousebridges_organizations_ProducerHouseId",
                        column: x => x.ProducerHouseId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collectiontagbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    CollectionId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectiontagbridges", x => new { x.CollectionId, x.TagId });
                    table.ForeignKey(
                        name: "FK_collectiontagbridges_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collectiontagbridges_tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fetchorders",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    At = table.Column<DateTime>(nullable: false),
                    SubscriptorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fetchorders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productioncheckouts",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubscriptorId = table.Column<string>(nullable: false),
                    At = table.Column<DateTime>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productioncheckouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productioncheckouts_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fetchordercollectionbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    FetchOrderId = table.Column<long>(nullable: false),
                    CollectionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fetchordercollectionbridges", x => new { x.FetchOrderId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_fetchordercollectionbridges_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fetchordercollectionbridges_fetchorders_FetchOrderId",
                        column: x => x.FetchOrderId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "fetchorders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fetchorderproductionbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    FetchOrderId = table.Column<long>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fetchorderproductionbridges", x => new { x.FetchOrderId, x.ProductionId });
                    table.ForeignKey(
                        name: "FK_fetchorderproductionbridges_fetchorders_FetchOrderId",
                        column: x => x.FetchOrderId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "fetchorders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fetchorderproductionbridges_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_collections_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "ClasificationId");

            migrationBuilder.CreateIndex(
                name: "IX_collectioncheckouts_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collectioncheckouts",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_collectiongenrebridges_GenreId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collectiongenrebridges",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_collectionproducerhousebridges_ProducerHouseId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collectionproducerhousebridges",
                column: "ProducerHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_collectiontagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collectiontagbridges",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_fetchordercollectionbridges_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "fetchordercollectionbridges",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_fetchorderproductionbridges_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "fetchorderproductionbridges",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_productioncheckouts_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productioncheckouts",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_collections_clasifications_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "ClasificationId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "clasifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collections_clasifications_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections");

            migrationBuilder.DropTable(
                name: "collectioncheckouts",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "collectiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "collectionproducerhousebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "collectiontagbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "fetchordercollectionbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "fetchorderproductionbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productioncheckouts",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "fetchorders",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropIndex(
                name: "IX_collections_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections");

            migrationBuilder.DropColumn(
                name: "ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections");
        }
    }
}
