using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cpcc_billboard_cpccbillboard");

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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Registered = table.Column<DateTime>(nullable: false),
                    EntityName = table.Column<string>(nullable: false),
                    BeforeData = table.Column<string>(nullable: false),
                    AfterData = table.Column<string>(nullable: true),
                    NextId = table.Column<long>(nullable: false),
                    Signature = table.Column<string>(nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clasifications",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clasifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fetchorders",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    At = table.Column<DateTime>(nullable: false),
                    SubscriptorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fetchorders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "advises",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Message = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "commentclassifiers",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Phrase = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commentclassifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fixscreenclientconfigurations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AdvisesChainPlaceholder = table.Column<string>(maxLength: 255, nullable: false),
                    AdvisesChainWellcome = table.Column<string>(maxLength: 100, nullable: true),
                    DailyRoomBillboardItemMaxCount = table.Column<int>(nullable: false),
                    DailyRoomBillboardDisplayTime = table.Column<int>(nullable: false),
                    PresentationDetailDisplayTime = table.Column<int>(nullable: false),
                    EventDetailDisplayTime = table.Column<int>(nullable: false),
                    CinemaOverviewDisplayTime = table.Column<int>(nullable: false),
                    StartUpDelayTime = table.Column<int>(nullable: false),
                    ShowPresentations = table.Column<bool>(nullable: false),
                    PlaylistSidebarItemsCount = table.Column<int>(nullable: false),
                    ShowEvents = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fixscreenclientconfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "portabledbdownloads",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserAgent = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portabledbdownloads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "email_contact_items",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Label = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Label = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    ContentType = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    IsoCode = table.Column<string>(nullable: false)
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
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
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
                    Enabled = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PasswordChangeRequired = table.Column<bool>(nullable: false),
                    HasFullAccess = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastSessionDate = table.Column<DateTime>(nullable: false)
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
                    CreateAt = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
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
                name: "videos",
                schema: "cpcc_billboard_media",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Format = table.Column<string>(nullable: false),
                    FrameSize = table.Column<string>(nullable: false),
                    ColorModel = table.Column<string>(nullable: false),
                    Fps = table.Column<double>(nullable: false),
                    AudioFormat = table.Column<string>(nullable: false),
                    AudioChannelOutput = table.Column<string>(nullable: false),
                    ThumbnailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_videos_thumbnails_ThumbnailId",
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
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
                name: "collections",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ClasificationId = table.Column<int>(nullable: true),
                    PosterImageId = table.Column<Guid>(nullable: true),
                    PlacardImageId = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(nullable: false)
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
                        name: "FK_collections_clasifications_ClasificationId",
                        column: x => x.ClasificationId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "clasifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "client",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    BornDate = table.Column<DateTime>(nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true)
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
                name: "organizations",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    BornDate = table.Column<DateTime>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    OwnerId = table.Column<long>(nullable: true),
                    ImageId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    LocalizationLongitude = table.Column<double>(nullable: true),
                    LocalizationLatitude = table.Column<double>(nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    BornDate = table.Column<DateTime>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    CivilStatus = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
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
                name: "events",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Header = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: false),
                    Thematic = table.Column<string>(nullable: false),
                    Organizer = table.Column<string>(nullable: false),
                    About = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PosterImageId = table.Column<Guid>(nullable: false),
                    PromoVideoId = table.Column<Guid>(nullable: true)
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
                        name: "FK_events_videos_PromoVideoId",
                        column: x => x.PromoVideoId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipalities",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "collectioncheckouts",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "rooms",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CinemaId = table.Column<long>(nullable: false)
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
                        name: "FK_organizationemailcontactitembridges_email_contact_items_Con~",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "email_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationemailcontactitembridges_organizations_Organizat~",
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
                        name: "FK_organizationphonecontactitembridges_phone_contact_items_Con~",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "phone_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationphonecontactitembridges_organizations_Organizat~",
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
                        name: "FK_organizationpostalcontactitembridges_postal_contact_items_C~",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "postal_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationpostalcontactitembridges_organizations_Organiza~",
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
                        name: "FK_organizationsocialcontactitembridges_social_contact_items_C~",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "social_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationsocialcontactitembridges_organizations_Organiza~",
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
                        name: "FK_organizationwebsitecontactitembridges_website_contact_items~",
                        column: x => x.ContactItemId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "website_contact_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizationwebsitecontactitembridges_organizations_Organiz~",
                        column: x => x.OrganizationId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PasswordChangeRequired = table.Column<bool>(nullable: false),
                    LastSignInAt = table.Column<DateTime>(nullable: true),
                    FailAuthenticationCount = table.Column<int>(nullable: false),
                    IdentityId = table.Column<long>(nullable: false),
                    ServiceAvailabilityFrom = table.Column<DateTime>(nullable: false),
                    ServiceAvailabilityTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscriptions_persons_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personalitycomments",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 255, nullable: false),
                    Banish = table.Column<bool>(nullable: false),
                    Syncronized = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    PersonalityId = table.Column<long>(nullable: true),
                    ReferenceId = table.Column<Guid>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_personalitycomments_personalitycomments_ReferenceId",
                        column: x => x.ReferenceId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "personalitycomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                schema: "cpcc_billboard_identity",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false),
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
                        name: "FK_personemailcontactitembridges_email_contact_items_ContactIt~",
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
                        name: "FK_personphonecontactitembridges_phone_contact_items_ContactIt~",
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
                        name: "FK_personpostalcontactitembridges_postal_contact_items_Contact~",
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
                        name: "FK_personsocialcontactitembridges_social_contact_items_Contact~",
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
                        name: "FK_personwebsitecontactitembridges_website_contact_items_Conta~",
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
                name: "eventcomments",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 255, nullable: false),
                    Banish = table.Column<bool>(nullable: false),
                    Syncronized = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    EventId = table.Column<Guid>(nullable: true),
                    ReferenceId = table.Column<Guid>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_eventcomments_eventcomments_ReferenceId",
                        column: x => x.ReferenceId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "eventcomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "eventoersonalitybridges",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    PersonalityId = table.Column<long>(nullable: false),
                    IsPresenter = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    MunicipalityId = table.Column<long>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "searchs",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    At = table.Column<DateTime>(nullable: false),
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
                        name: "FK_subscriptionproducerhousebridges_organizations_ProducerHous~",
                        column: x => x.ProducerHouseId,
                        principalSchema: "cpcc_billboard_identity",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptionproducerhousebridges_subscriptions_Subscription~",
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
                name: "personalitycommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Revoked = table.Column<bool>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "eventcommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Revoked = table.Column<bool>(nullable: false),
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
                name: "locality_regions",
                schema: "cpcc_billboard_region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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

            migrationBuilder.CreateTable(
                name: "awardwinnings",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Edition = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    AwardId = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "productions",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: false),
                    LocalizedTitle = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    About = table.Column<string>(nullable: false),
                    Argument = table.Column<string>(nullable: true),
                    PosterImageId = table.Column<Guid>(nullable: false),
                    PlacardImageId = table.Column<Guid>(nullable: true),
                    TrailerVideoId = table.Column<Guid>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    ClasificationId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    CollectionId = table.Column<Guid>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    AudioVisualId = table.Column<long>(nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productions_clasifications_ClasificationId",
                        column: x => x.ClasificationId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "clasifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productions_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_productions_images_PlacardImageId",
                        column: x => x.PlacardImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productions_images_PosterImageId",
                        column: x => x.PosterImageId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productions_videos_TrailerVideoId",
                        column: x => x.TrailerVideoId,
                        principalSchema: "cpcc_billboard_media",
                        principalTable: "videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "countrycoproductionbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    ProductionId = table.Column<long>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countrycoproductionbridges", x => new { x.ProductionId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_countrycoproductionbridges_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "cpcc_billboard_region",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_countrycoproductionbridges_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalSchema: "cpcc_billboard_cinemabillboard",
                        principalTable: "productions",
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

            migrationBuilder.CreateTable(
                name: "participations",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Role = table.Column<int>(nullable: false),
                    PersonalityId = table.Column<long>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false),
                    Specification = table.Column<string>(nullable: true)
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
                name: "presentations",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ProductionId = table.Column<long>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false)
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
                name: "producerhouseproductionbridges",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    ProductionId = table.Column<long>(nullable: false),
                    ProducerHouseId = table.Column<long>(nullable: false)
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
                name: "productionaudiovisuals",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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

            migrationBuilder.CreateTable(
                name: "productioncheckouts",
                schema: "cpcc_billboard_cinemabillboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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

            migrationBuilder.CreateTable(
                name: "productioncomments",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 255, nullable: false),
                    Banish = table.Column<bool>(nullable: false),
                    Syncronized = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    ProductionId = table.Column<long>(nullable: true),
                    ReferenceId = table.Column<Guid>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_productioncomments_productioncomments_ReferenceId",
                        column: x => x.ReferenceId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "productioncomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productioncommentdelations",
                schema: "cpcc_billboard_cpccbillboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Revoked = table.Column<bool>(nullable: false),
                    CommentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productioncommentdelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productioncommentdelations_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productioncommentdelations_productioncomments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "cpcc_billboard_cpccbillboard",
                        principalTable: "productioncomments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_advertisements_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "advertisements",
                column: "PlacardImageId");

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
                name: "IX_categories_ParentId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "categories",
                column: "ParentId");

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
                name: "IX_collections_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_collections_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections",
                column: "ClasificationId");

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
                name: "IX_collectiontagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collectiontagbridges",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_countrycoproductionbridges_CountryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "countrycoproductionbridges",
                column: "CountryId");

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
                name: "IX_productionaudiovisuals_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productionaudiovisuals",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_productioncheckouts_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productioncheckouts",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_productiongenrebridges_GenreId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productiongenrebridges",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_AudioVisualId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "AudioVisualId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_CategoryId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_ClasificationId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "ClasificationId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_CollectionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "CollectionId");

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
                name: "IX_productions_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "PlacardImageId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_PosterImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "PosterImageId");

            migrationBuilder.CreateIndex(
                name: "IX_productions_TrailerVideoId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions",
                column: "TrailerVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_productiontagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productiontagbridges",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_CinemaId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "rooms",
                column: "CinemaId");

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
                name: "IX_subscriptions_IdentityId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptions",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptiontagbridges_TagId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "subscriptiontagbridges",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_client_ImageId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "client",
                column: "ImageId");

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
                name: "IX_eventcomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "eventcomments",
                column: "ReferenceId");

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
                name: "IX_events_PromoVideoId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "events",
                column: "PromoVideoId");

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
                name: "IX_personalitycomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "personalitycomments",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_productioncommentdelations_ClientId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_productioncommentdelations_CommentId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncommentdelations",
                column: "CommentId");

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

            migrationBuilder.CreateIndex(
                name: "IX_productioncomments_ReferenceId",
                schema: "cpcc_billboard_cpccbillboard",
                table: "productioncomments",
                column: "ReferenceId");

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
                name: "IX_videos_ThumbnailId",
                schema: "cpcc_billboard_media",
                table: "videos",
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

            migrationBuilder.AddForeignKey(
                name: "FK_awardwinnings_productions_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "awardwinnings",
                column: "ProductionId",
                principalSchema: "cpcc_billboard_cinemabillboard",
                principalTable: "productions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_collections_images_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections");

            migrationBuilder.DropForeignKey(
                name: "FK_collections_images_PosterImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "collections");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_images_PlacardImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_images_PosterImageId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productionaudiovisuals_productions_ProductionId",
                schema: "cpcc_billboard_cinemabillboard",
                table: "productionaudiovisuals");

            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "advertisements",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "awardwinnings",
                schema: "cpcc_billboard_cinemabillboard");

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
                name: "countrycoproductionbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "fetchordercollectionbridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "fetchorderproductionbridges",
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
                name: "productioncheckouts",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productiongenrebridges",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productiontagbridges",
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
                name: "advises",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "commentclassifiers",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "eventcommentdelations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "eventoersonalitybridges",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "fixscreenclientconfigurations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "personalitycommentdelations",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "portabledbdownloads",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "productioncommentdelations",
                schema: "cpcc_billboard_cpccbillboard");

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
                name: "fetchorders",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "searchs",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "genres",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "cpcc_billboard_cinemabillboard");

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
                name: "subscriptions",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "events",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "client",
                schema: "cpcc_billboard_cpccbillboard");

            migrationBuilder.DropTable(
                name: "municipalities",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "cpcc_billboard_identity");

            migrationBuilder.DropTable(
                name: "states",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "images",
                schema: "cpcc_billboard_media");

            migrationBuilder.DropTable(
                name: "productions",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "productionaudiovisuals",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "collections",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "cpcc_billboard_region");

            migrationBuilder.DropTable(
                name: "languages",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "videos",
                schema: "cpcc_billboard_media");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "clasifications",
                schema: "cpcc_billboard_cinemabillboard");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "cpcc_billboard_media");
        }
    }
}
