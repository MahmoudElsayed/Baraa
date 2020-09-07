using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Baraa.DAL.Contract.Migrations
{
    public partial class _000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UserType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission.PermissionAction",
                columns: table => new
                {
                    PermissionActionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionActionGuid = table.Column<Guid>(nullable: false),
                    PermissionActionNameAr = table.Column<string>(maxLength: 50, nullable: true),
                    PermissionActionNameEn = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission.PermissionAction", x => x.PermissionActionID);
                });

            migrationBuilder.CreateTable(
                name: "Permission.PermissionController",
                columns: table => new
                {
                    PermissionControllerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionControllerGuid = table.Column<Guid>(nullable: false),
                    PermissionControllerNameAr = table.Column<string>(maxLength: 50, nullable: true),
                    PermissionControllerNameEn = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission.PermissionController", x => x.PermissionControllerID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting.Country",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    CountryGuid = table.Column<Guid>(nullable: false),
                    NameAR = table.Column<string>(maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(maxLength: 75, nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Flag = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Long = table.Column<string>(nullable: true),
                    Zoom = table.Column<string>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting.Country", x => x.CountryID);
                    table.ForeignKey(
                        name: "FK_Setting.Country_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Setting.Nationality",
                columns: table => new
                {
                    NationalityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    NationalityGuid = table.Column<Guid>(nullable: false),
                    NameAR = table.Column<string>(maxLength: 50, nullable: true),
                    NameEN = table.Column<string>(maxLength: 50, nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting.Nationality", x => x.NationalityID);
                    table.ForeignKey(
                        name: "FK_Setting.Nationality_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Setting.Positions",
                columns: table => new
                {
                    PositionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    PositionGuid = table.Column<Guid>(nullable: false),
                    NameAR = table.Column<string>(maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(maxLength: 75, nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting.Positions", x => x.PositionID);
                    table.ForeignKey(
                        name: "FK_Setting.Positions_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Setting.Zone",
                columns: table => new
                {
                    ZoneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    ZoneGuid = table.Column<Guid>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 75, nullable: true),
                    NameEn = table.Column<string>(maxLength: 75, nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting.Zone", x => x.ZoneID);
                    table.ForeignKey(
                        name: "FK_Setting.Zone_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission.PermissionControllerAction",
                columns: table => new
                {
                    PermissionControllerActionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionControllerActionGuid = table.Column<Guid>(nullable: false),
                    PermissionControllerID = table.Column<int>(nullable: false),
                    PermissionActionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission.PermissionControllerAction", x => x.PermissionControllerActionID);
                    table.ForeignKey(
                        name: "FK_Permission.PermissionControllerAction_Permission.PermissionAction_PermissionActionID",
                        column: x => x.PermissionActionID,
                        principalTable: "Permission.PermissionAction",
                        principalColumn: "PermissionActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission.PermissionControllerAction_Permission.PermissionController_PermissionControllerID",
                        column: x => x.PermissionControllerID,
                        principalTable: "Permission.PermissionController",
                        principalColumn: "PermissionControllerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting.City",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    CityGuid = table.Column<Guid>(nullable: false),
                    CountryID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(maxLength: 75, nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Long = table.Column<string>(nullable: true),
                    Zoom = table.Column<string>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting.City", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Setting.City_Setting.Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Setting.Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Setting.City_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission.Permission",
                columns: table => new
                {
                    PermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionGuid = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    PermissionControllerActionID = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<string>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission.Permission", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_Permission.Permission_Permission.PermissionControllerAction_PermissionControllerActionID",
                        column: x => x.PermissionControllerActionID,
                        principalTable: "Permission.PermissionControllerAction",
                        principalColumn: "PermissionControllerActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission.Permission_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission.Permission_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operator.Agent",
                columns: table => new
                {
                    AgentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    CityID = table.Column<int>(nullable: false),
                    AgentGuid = table.Column<Guid>(nullable: false),
                    NameAR = table.Column<string>(maxLength: 75, nullable: false),
                    NameEn = table.Column<string>(maxLength: 75, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Lng = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    AgentManger = table.Column<string>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true),
                    ZoneID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator.Agent", x => x.AgentID);
                    table.ForeignKey(
                        name: "FK_Operator.Agent_Setting.City_CityID",
                        column: x => x.CityID,
                        principalTable: "Setting.City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operator.Agent_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operator.Agent_Setting.Zone_ZoneID",
                        column: x => x.ZoneID,
                        principalTable: "Setting.Zone",
                        principalColumn: "ZoneID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operator.OperatorUser",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    OperatorGUID = table.Column<Guid>(nullable: true),
                    NationalityID = table.Column<int>(nullable: false),
                    PositionID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 75, nullable: true),
                    NameEn = table.Column<string>(maxLength: 75, nullable: false),
                    MobileNo = table.Column<string>(maxLength: 20, nullable: false),
                    FileNo = table.Column<string>(maxLength: 10, nullable: false),
                    IDNo = table.Column<string>(maxLength: 10, nullable: false),
                    MainAddress = table.Column<string>(maxLength: 75, nullable: false),
                    AlternativeAddress = table.Column<string>(maxLength: 75, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<byte>(nullable: false),
                    ExpiryDateIDType = table.Column<byte>(nullable: true),
                    ExpiryDateID = table.Column<string>(nullable: true),
                    DateStartWork = table.Column<DateTime>(nullable: false),
                    IDImage = table.Column<string>(nullable: true),
                    PersonalImage = table.Column<string>(nullable: true),
                    IsEnable = table.Column<byte>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    PositionsPositionID = table.Column<int>(nullable: true),
                    IsMobileActivated = table.Column<bool>(nullable: false),
                    IsEmailActivated = table.Column<bool>(nullable: true),
                    ActivationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator.OperatorUser", x => x.OperatorID);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorUser_Setting.City_CityID",
                        column: x => x.CityID,
                        principalTable: "Setting.City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorUser_Setting.Nationality_NationalityID",
                        column: x => x.NationalityID,
                        principalTable: "Setting.Nationality",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorUser_Setting.Positions_PositionsPositionID",
                        column: x => x.PositionsPositionID,
                        principalTable: "Setting.Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorUser_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Setting.ZoneCity",
                columns: table => new
                {
                    ZoneCityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    ZoneID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting.ZoneCity", x => x.ZoneCityID);
                    table.ForeignKey(
                        name: "FK_Setting.ZoneCity_Setting.City_CityID",
                        column: x => x.CityID,
                        principalTable: "Setting.City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Setting.ZoneCity_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Setting.ZoneCity_Setting.Zone_ZoneID",
                        column: x => x.ZoneID,
                        principalTable: "Setting.Zone",
                        principalColumn: "ZoneID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operator.OperatorAgent",
                columns: table => new
                {
                    OperatorAgentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserUpdate = table.Column<int>(nullable: true),
                    UserDelete = table.Column<int>(nullable: true),
                    OperatorAgentGuid = table.Column<Guid>(nullable: false),
                    AgentID = table.Column<int>(nullable: false),
                    OperatorID = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    OperatorUserOperatorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator.OperatorAgent", x => x.OperatorAgentID);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorAgent_Operator.Agent_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Operator.Agent",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorAgent_Operator.OperatorUser_OperatorUserOperatorID",
                        column: x => x.OperatorUserOperatorID,
                        principalTable: "Operator.OperatorUser",
                        principalColumn: "OperatorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operator.OperatorAgent_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.Agent_CityID",
                table: "Operator.Agent",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.Agent_UserId1",
                table: "Operator.Agent",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.Agent_ZoneID",
                table: "Operator.Agent",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorAgent_AgentID",
                table: "Operator.OperatorAgent",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorAgent_OperatorUserOperatorID",
                table: "Operator.OperatorAgent",
                column: "OperatorUserOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorAgent_UserId1",
                table: "Operator.OperatorAgent",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorUser_CityID",
                table: "Operator.OperatorUser",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorUser_NationalityID",
                table: "Operator.OperatorUser",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorUser_PositionsPositionID",
                table: "Operator.OperatorUser",
                column: "PositionsPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Operator.OperatorUser_UserId1",
                table: "Operator.OperatorUser",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permission.Permission_PermissionControllerActionID",
                table: "Permission.Permission",
                column: "PermissionControllerActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission.Permission_RoleId1",
                table: "Permission.Permission",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permission.Permission_UserId1",
                table: "Permission.Permission",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permission.PermissionControllerAction_PermissionActionID",
                table: "Permission.PermissionControllerAction",
                column: "PermissionActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission.PermissionControllerAction_PermissionControllerID",
                table: "Permission.PermissionControllerAction",
                column: "PermissionControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.City_CountryID",
                table: "Setting.City",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.City_UserId1",
                table: "Setting.City",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.Country_UserId1",
                table: "Setting.Country",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.Nationality_UserId1",
                table: "Setting.Nationality",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.Positions_UserId1",
                table: "Setting.Positions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.Zone_UserId1",
                table: "Setting.Zone",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.ZoneCity_CityID",
                table: "Setting.ZoneCity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.ZoneCity_UserId1",
                table: "Setting.ZoneCity",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Setting.ZoneCity_ZoneID",
                table: "Setting.ZoneCity",
                column: "ZoneID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Operator.OperatorAgent");

            migrationBuilder.DropTable(
                name: "Permission.Permission");

            migrationBuilder.DropTable(
                name: "Setting.ZoneCity");

            migrationBuilder.DropTable(
                name: "Operator.Agent");

            migrationBuilder.DropTable(
                name: "Operator.OperatorUser");

            migrationBuilder.DropTable(
                name: "Permission.PermissionControllerAction");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Setting.Zone");

            migrationBuilder.DropTable(
                name: "Setting.City");

            migrationBuilder.DropTable(
                name: "Setting.Nationality");

            migrationBuilder.DropTable(
                name: "Setting.Positions");

            migrationBuilder.DropTable(
                name: "Permission.PermissionAction");

            migrationBuilder.DropTable(
                name: "Permission.PermissionController");

            migrationBuilder.DropTable(
                name: "Setting.Country");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
