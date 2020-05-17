using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_cinemabillboard");

            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_security");

            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_identity");

            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_media");

            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_region");

            migrationBuilder.CreateTable(
                name: "traces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AfterData = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    BeforeData = table.Column<string>(nullable: false),
                    EntityName = table.Column<string>(nullable: false),
                    NextId = table.Column<long>(nullable: false),
                    Registered = table.Column<DateTime>(nullable: false),
                    Signature = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "awards",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_awards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "email_contact_items",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_contact_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phone_contact_items",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phone_contact_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "postal_contact_items",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postal_contact_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "social_contact_items",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(nullable: true),
                    SocialId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_contact_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "website_contact_items",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_website_contact_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                schema: "cpcc_billboard_media",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thumbnails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsoCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                schema: "cpcc_billboard_security",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "cpcc_billboard_security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "cpcc_billboard_security",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    HasFullAccess = table.Column<bool>(nullable: false),
                    LastSessionDate = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PasswordChangeRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "cpcc_billboard_media",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    ThumbnailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_thumbnails_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "thumbnails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "country_regions",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country_regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_country_regions_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "states",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.Id);
                    table.ForeignKey(
                        name: "FK_states_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grouprolebridges",
                schema: "cpcc_billboard_security",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grouprolebridges", x => new { x.GroupId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_grouprolebridges_groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "cpcc_billboard_security",
                        principalTable: "groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grouprolebridges_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cpcc_billboard_security",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usergroupbridges",
                schema: "cpcc_billboard_security",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usergroupbridges", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_usergroupbridges_groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "cpcc_billboard_security",
                        principalTable: "groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usergroupbridges_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "cpcc_billboard_security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productions",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(maxLength: 255, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    Duration = table.Column<long>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    LocalizedTitle = table.Column<string>(maxLength: 45, nullable: true),
                    PosterImageId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 45, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productions_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productions_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productions_languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productions_images_PosterImageId",
                        column: x => x.PosterImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    LocalizationLatitude = table.Column<long>(nullable: true),
                    LocalizationLongitude = table.Column<long>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(nullable: true),
                    BornDate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    OwnerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_organizations_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_organizations_organizations_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    BornDate = table.Column<DateTime>(nullable: false),
                    CivilStatus = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Sex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_persons_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipalities",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_municipalities_states_StateId",
                        column: x => x.StateId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "states",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "state_regions",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state_regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_state_regions_states_StateId",
                        column: x => x.StateId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "states",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "awardwinnings",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AwardId = table.Column<int>(nullable: false),
                    Edition = table.Column<string>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false),
                    ProductionRole = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_awardwinnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_awardwinnings_awards_AwardId",
                        column: x => x.AwardId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_awardwinnings_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    ProductionId = table.Column<long>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productiongenrebridges", x => new { x.ProductionId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_productiongenrebridges_genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productiongenrebridges_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "producerhouseproductionbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    ProducerHouseId = table.Column<long>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producerhouseproductionbridges", x => new { x.ProducerHouseId, x.ProductionId });
                    table.ForeignKey(
                        name: "FK_producerhouseproductionbridges_organizations_ProducerHouseId",
                        column: x => x.ProducerHouseId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producerhouseproductionbridges_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CinemaId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rooms_organizations_CinemaId",
                        column: x => x.CinemaId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationemailcontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationemailcontactitembridges", x => new { x.OrganizationId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_organizationemailcontactitembridges_email_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "email_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationemailcontactitembridges_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationphonecontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationphonecontactitembridges", x => new { x.OrganizationId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_organizationphonecontactitembridges_phone_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "phone_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationphonecontactitembridges_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationpostalcontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationpostalcontactitembridges", x => new { x.OrganizationId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_organizationpostalcontactitembridges_postal_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "postal_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationpostalcontactitembridges_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationsocialcontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationsocialcontactitembridges", x => new { x.OrganizationId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_organizationsocialcontactitembridges_social_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "social_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationsocialcontactitembridges_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationwebsitecontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationwebsitecontactitembridges", x => new { x.OrganizationId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_organizationwebsitecontactitembridges_website_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "website_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationwebsitecontactitembridges_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participations",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonalityId = table.Column<long>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_participations_persons_PersonalityId",
                        column: x => x.PersonalityId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participations_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    PersonId = table.Column<long>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberships", x => new { x.OrganizationId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_memberships_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_memberships_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personemailcontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personemailcontactitembridges", x => new { x.PersonId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_personemailcontactitembridges_email_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "email_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personemailcontactitembridges_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personphonecontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personphonecontactitembridges", x => new { x.PersonId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_personphonecontactitembridges_phone_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "phone_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personphonecontactitembridges_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personpostalcontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personpostalcontactitembridges", x => new { x.PersonId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_personpostalcontactitembridges_postal_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "postal_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personpostalcontactitembridges_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personsocialcontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personsocialcontactitembridges", x => new { x.PersonId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_personsocialcontactitembridges_social_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "social_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personsocialcontactitembridges_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personwebsitecontactitembridges",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false),
                    ContactItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personwebsitecontactitembridges", x => new { x.PersonId, x.ContactItemId });
                    table.ForeignKey(
                        name: "FK_personwebsitecontactitembridges_website_contact_items_ContactItemId",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "website_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personwebsitecontactitembridges_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MunicipalityId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_localities_municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "municipality_regions",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MunicipalityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipality_regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_municipality_regions_municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "presentations",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_presentations_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_presentations_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "locality_regions",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LocalityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locality_regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_locality_regions_localities_LocalityId",
                        column: x => x.LocalityId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_awardwinnings_AwardId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_awardwinnings_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_participations_PersonalityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "participations",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_participations_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "participations",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_presentations_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "presentations",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_presentations_RoomId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "presentations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_producerhouseproductionbridges_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "producerhouseproductionbridges",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_productiongenrebridges_GenreId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productiongenrebridges",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_CountryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_LanguageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_PosterImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "PosterImageId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_CinemaId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "rooms",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_memberships_PersonId",
                schema: "cpcc_billboard_identity",
                table: "memberships",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationemailcontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "organizationemailcontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationphonecontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "organizationphonecontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationpostalcontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "organizationpostalcontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_organizations_ImageId",
                schema: "cpcc_billboard_identity",
                table: "organizations",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_organizations_OwnerId",
                schema: "cpcc_billboard_identity",
                table: "organizations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationsocialcontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "organizationsocialcontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationwebsitecontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "organizationwebsitecontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_personemailcontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "personemailcontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_personphonecontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "personphonecontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_personpostalcontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "personpostalcontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_ImageId",
                schema: "cpcc_billboard_identity",
                table: "persons",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_personsocialcontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "personsocialcontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_personwebsitecontactitembridges_ContactItemId",
                schema: "cpcc_billboard_identity",
                table: "personwebsitecontactitembridges",
                column: "ContactItemId");

            migrationBuilder.CreateIndex(
                name: "IX_images_ThumbnailId",
                schema: "cpcc_billboard_media",
                table: "images",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_countries_IsoCode",
                schema: "cpcc_billboard_region",
                table: "countries",
                column: "IsoCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_Name",
                schema: "cpcc_billboard_region",
                table: "countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_country_regions_CountryId",
                schema: "cpcc_billboard_region",
                table: "country_regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_localities_MunicipalityId",
                schema: "cpcc_billboard_region",
                table: "localities",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_localities_Name_MunicipalityId",
                schema: "cpcc_billboard_region",
                table: "localities",
                columns: new[] { "Name", "MunicipalityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_locality_regions_LocalityId",
                schema: "cpcc_billboard_region",
                table: "locality_regions",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_municipalities_StateId",
                schema: "cpcc_billboard_region",
                table: "municipalities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_municipalities_Name_StateId",
                schema: "cpcc_billboard_region",
                table: "municipalities",
                columns: new[] { "Name", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipality_regions_MunicipalityId",
                schema: "cpcc_billboard_region",
                table: "municipality_regions",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_state_regions_StateId",
                schema: "cpcc_billboard_region",
                table: "state_regions",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_states_CountryId",
                schema: "cpcc_billboard_region",
                table: "states",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_grouprolebridges_RoleId",
                schema: "cpcc_billboard_security",
                table: "grouprolebridges",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_usergroupbridges_GroupId",
                schema: "cpcc_billboard_security",
                table: "usergroupbridges",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "awardwinnings",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "participations",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "presentations",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "producerhouseproductionbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "memberships",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "organizationemailcontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "organizationphonecontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "organizationpostalcontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "organizationsocialcontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "organizationwebsitecontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "personemailcontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "personphonecontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "personpostalcontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "personsocialcontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "personwebsitecontactitembridges",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "country_regions",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "locality_regions",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "municipality_regions",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "state_regions",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "grouprolebridges",
                schema: "cpcc_billboard_security");

            migrationBuilder.DropTable(
                name: "usergroupbridges",
                schema: "cpcc_billboard_security");

            migrationBuilder.DropTable(
                name: "awards",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "genres",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productions",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "email_contact_items",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "phone_contact_items",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "postal_contact_items",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "social_contact_items",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "website_contact_items",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "localities",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "cpcc_billboard_security");

            migrationBuilder.DropTable(
                name: "groups",
                schema: "cpcc_billboard_security");

            migrationBuilder.DropTable(
                name: "users",
                schema: "cpcc_billboard_security");

            migrationBuilder.DropTable(
                name: "organizations",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "languages",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "municipalities",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "images",
                schema: "cpcc_billboard_media");

            migrationBuilder.DropTable(
                name: "states",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "cpcc_billboard_media");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "cpcc_billboard_region");
        }
    }
}
