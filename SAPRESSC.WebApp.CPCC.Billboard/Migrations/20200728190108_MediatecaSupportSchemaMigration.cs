using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class MediatecaSupportSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categories_categories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "collections",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PosterImageId = table.Column<Guid>(nullable: true),
                    PlacardImageId = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_collections_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collections_collections_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collections_images_PlacardImageId",
                        column: x => x.PlacardImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_collections_images_PosterImageId",
                        column: x => x.PosterImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productiontagbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    ProductionId = table.Column<long>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productiontagbridges", x => new { x.ProductionId, x.TagId });
                    table.ForeignKey(
                        name: "FK_productiontagbridges_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productiontagbridges_tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productions_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "PlacardImageId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_collections_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_collections_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_collections_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "PlacardImageId");

            migrationBuilder.CreateIndex(
                name: "IX_collections_PosterImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "PosterImageId");

            migrationBuilder.CreateIndex(
                name: "IX_productiontagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productiontagbridges",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_collections_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CollectionId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_images_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "PlacardImageId",
                principalSchema: "cpcc_billboard_media",
                principalTable: "images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_categories_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_collections_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_images_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropTable(
                name: "collections",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productiontagbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropIndex(
                name: "IX_productions_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropIndex(
                name: "IX_productions_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropIndex(
                name: "IX_productions_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");
        }
    }
}
