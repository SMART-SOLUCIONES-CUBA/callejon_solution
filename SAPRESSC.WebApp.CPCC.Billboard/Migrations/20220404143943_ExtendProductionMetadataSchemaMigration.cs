using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class ExtendProductionMetadataSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "productionaudiovisuals",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<long>(nullable: false),
                    Format = table.Column<string>(nullable: true),
                    Quality = table.Column<int>(nullable: false),
                    HasSubtitle = table.Column<bool>(nullable: false),
                    Resolution = table.Column<string>(nullable: true),
                    FileSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productionaudiovisuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productionaudiovisuals_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productions_AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "AudioVisualId");

            migrationBuilder.CreateIndex(
                name: "IX_productionaudiovisuals_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productionaudiovisuals",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_productionaudiovisuals_AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "AudioVisualId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "productionaudiovisuals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_productionaudiovisuals_AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropTable(
                name: "productionaudiovisuals",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropIndex(
                name: "IX_productions_AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");
        }
    }
}
