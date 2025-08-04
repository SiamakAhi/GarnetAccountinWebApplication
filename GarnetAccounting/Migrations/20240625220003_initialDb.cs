using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acc_Coding_Groups",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Coding_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_Coding_TafsilGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    IsPerson = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Coding_TafsilGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_Coding_Tafsils",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupsId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    IsPerson = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Coding_Tafsils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_CostCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_CostCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_DocStatuses",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_DocStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_DocTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_DocTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_FinancialPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefualtVatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_FinancialPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LunchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpierDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSubsystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_fa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubsystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ishoghooghi = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EconomicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseCount = table.Column<int>(type: "int", nullable: false),
                    InvoiceCountLimit = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    licenseExpierDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VersionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productOrServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productOrServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettlementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StuffCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuffCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxPayerTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPayerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSellers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ActiveSellerId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveFinancePeriodId = table.Column<int>(type: "int", nullable: true),
                    AllowSellerManagement = table.Column<bool>(type: "bit", nullable: false),
                    AllowStuffManagement = table.Column<bool>(type: "bit", nullable: false),
                    AllowBuyerManagement = table.Column<bool>(type: "bit", nullable: false),
                    AllowSaleManagement = table.Column<bool>(type: "bit", nullable: false),
                    AllowUserManagement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acc_Coding_Kols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    KolCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nature = table.Column<short>(type: "smallint", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    GroupId = table.Column<short>(type: "smallint", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Coding_Kols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acc_Coding_Kols_Acc_Coding_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Acc_Coding_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acc_Coding_TafsilToGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TafsilId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Coding_TafsilToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acc_Coding_TafsilToGroups_Acc_Coding_TafsilGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Acc_Coding_TafsilGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Acc_Coding_TafsilToGroups_Acc_Coding_Tafsils_TafsilId",
                        column: x => x.TafsilId,
                        principalTable: "Acc_Coding_Tafsils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acc_Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    AutoDocNumber = table.Column<int>(type: "int", nullable: false),
                    AtfNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    SubsystemId = table.Column<int>(type: "int", nullable: true),
                    SubsystemRef = table.Column<long>(type: "bigint", nullable: true),
                    CreatorUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditorUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acc_Documents_Acc_FinancialPeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Acc_FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHABA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cvvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductOrServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    AccountingSystemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VATRate = table.Column<float>(type: "real", nullable: false),
                    UnitOfMeasurementId = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ForeignCurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LocalCurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    NetWeight = table.Column<float>(type: "real", nullable: true),
                    OtherLegalChargesSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLegalChargesRate = table.Column<float>(type: "real", nullable: true),
                    OtherLegalChargesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherTaxesSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherTaxesRate = table.Column<float>(type: "real", nullable: true),
                    OtherTaxesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrServices_Measurements_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrServices_StuffCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StuffCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "parties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uyer_SellerId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AccountingSystemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalStatus = table.Column<short>(type: "smallint", nullable: false),
                    TaxPayerType = table.Column<short>(type: "smallint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EconomicCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxMemoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPublicKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPublicKeyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPrivateKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPrivateAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCSRKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCSRKeyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLogoDisplayedOnInvoice = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_parties_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_parties_TaxPayerTypes_TaxPayerType",
                        column: x => x.TaxPayerType,
                        principalTable: "TaxPayerTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Acc_Coding_Moeins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoeinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoeinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nature = table.Column<short>(type: "smallint", nullable: false),
                    IsCurrencyAccount = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    MoeinContraryNatureId = table.Column<int>(type: "int", nullable: true),
                    Tafsil4_GroupIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil5_GroupIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil6_GroupIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil7_GroupIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil8_GroupIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    KolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Coding_Moeins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acc_Coding_Moeins_Acc_Coding_Kols_KolId",
                        column: x => x.KolId,
                        principalTable: "Acc_Coding_Kols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppIdentityUserAppRole",
                columns: table => new
                {
                    RolesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppIdentityUserAppRole", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppIdentityUserAppRole_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppIdentityUserAppRole_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceSubject = table.Column<int>(type: "int", nullable: true),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    AccountingSystemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceSellerCount = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    BuyerId = table.Column<long>(type: "bigint", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusId = table.Column<short>(type: "smallint", nullable: false),
                    invoiceReferenceTaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicePattern = table.Column<int>(type: "int", nullable: true),
                    sellerBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    buyerBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sellerCustomsLicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sellerCustomsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contractRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Billid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPreDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalVatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalOtherDutyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalBill = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    cashPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InstallmentPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalVatOfPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cdcn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cdcd = table.Column<int>(type: "int", nullable: true),
                    Tonw = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Torv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tocv = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_SettlementTypes_SettlementTypeId",
                        column: x => x.SettlementTypeId,
                        principalTable: "SettlementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_parties_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "parties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_parties_SellerId",
                        column: x => x.SellerId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyRepresentatives",
                columns: table => new
                {
                    PartyId = table.Column<long>(type: "bigint", nullable: false),
                    RepresentativeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRepresentatives", x => new { x.PartyId, x.RepresentativeId });
                    table.ForeignKey(
                        name: "FK_PartyRepresentatives_parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRepresentatives_parties_RepresentativeId",
                        column: x => x.RepresentativeId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Acc_Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    DocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    KolId = table.Column<int>(type: "int", nullable: true),
                    MoeinId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Bed = table.Column<long>(type: "bigint", nullable: false),
                    Bes = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil4Id = table.Column<int>(type: "int", nullable: true),
                    Tafsil4Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil5Id = table.Column<int>(type: "int", nullable: true),
                    Tafsil5Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil6Id = table.Column<int>(type: "int", nullable: true),
                    Tafsil6Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil7Id = table.Column<int>(type: "int", nullable: true),
                    Tafsil7Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tafsil8Id = table.Column<int>(type: "int", nullable: true),
                    Tafsil8Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditorUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acc_Articles_Acc_Coding_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Acc_Coding_Moeins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Acc_Articles_Acc_Documents_DocId",
                        column: x => x.DocId,
                        principalTable: "Acc_Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNumber = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProductOrServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProductOrServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currencyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyTypeId = table.Column<int>(type: "int", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    vatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    overDutyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    overDutyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    overDutyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    otherLegalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Olr = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    otherLegalRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    constructionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    sellerProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    brokerSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    totalConstructionProfitBrokerSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    cashOfPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    vatOfPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    buyerSRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nw = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ssrv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sscv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    pspd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Consfee_TalaOjrat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Spro_TalaSood = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tcpbs_TalaTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bros_TalaHagh = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Currencies_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_ProductOrServices_ProductOrServiceId",
                        column: x => x.ProductOrServiceId,
                        principalTable: "ProductOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Acc_Coding_Groups",
                columns: new[] { "Id", "Description", "GroupCode", "GroupName", "IsEditable", "SellerId", "TypeId" },
                values: new object[,]
                {
                    { (short)1, "", "1", "دارایی های غیرجاری", false, null, (short)1 },
                    { (short)2, "", "2", "دارایی های جاری", false, null, (short)1 },
                    { (short)3, "", "3", "حقوق مالکانه", false, null, (short)1 },
                    { (short)4, "", "4", "بدهی های غیرجاری", false, null, (short)1 },
                    { (short)5, "", "5", "بدهی های جاری", false, null, (short)1 },
                    { (short)6, "", "6", "فروش و درآمدها", false, null, (short)2 },
                    { (short)7, "", "7", "هزینه ها", false, null, (short)2 },
                    { (short)8, "", "8", "بهای تمام شده", false, null, (short)2 },
                    { (short)9, "", "9", "حسابهای انتظامی", false, null, (short)3 },
                    { (short)10, "", "0", "تراز افتتاحیه و اختتامیه", false, null, (short)3 }
                });

            migrationBuilder.InsertData(
                table: "Acc_Coding_TafsilGroups",
                columns: new[] { "Id", "Description", "GroupName", "IsEditable", "IsPerson", "SellerId" },
                values: new object[,]
                {
                    { 1, "مشتریان، تأمین کنندگان، کارمندان، شرکت های طرف قرارداد و ...", "اشخاص و شرکت ها", false, true, null },
                    { 2, null, "بانک ها", false, false, null },
                    { 3, null, "صندوق ها", false, false, null },
                    { 4, null, "حساب های بانکی", false, false, null },
                    { 5, null, "شعب", false, false, null },
                    { 6, null, "نمایندگی ها", false, false, null },
                    { 7, null, "مراکز هزینه", false, false, null }
                });

            migrationBuilder.InsertData(
                table: "Acc_DocStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "یادداشت" },
                    { (short)2, "ثبت موقت" },
                    { (short)3, "ثبت دائم" }
                });

            migrationBuilder.InsertData(
                table: "Acc_DocTypes",
                columns: new[] { "Id", "DocTypeName" },
                values: new object[,]
                {
                    { (short)1, "سند روزانه" },
                    { (short)2, "سند افتتاحیه" },
                    { (short)3, "سند اختتامیه" },
                    { (short)4, "سند بستن حسابهای موقت" },
                    { (short)5, "سند بستن حسابهای دائم" },
                    { (short)6, "سند طبقه بندی حسابها" }
                });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "AppName", "CompanyName", "ExpierDate", "LastUpdate", "LoginMessage", "LogoUrl", "LunchDate", "OwnerName", "Version" },
                values: new object[] { 1, "نرم افزار حسابداری گارنِت ", "آوا اندیش رستـا", null, null, "آوای تکنولوژی، آواز موفقیت", "../../img/aar.png", null, "سیامک آهی", "0.1" });

            migrationBuilder.InsertData(
                table: "AppSubsystems",
                columns: new[] { "Id", "Description", "Name_En", "Name_fa" },
                values: new object[,]
                {
                    { 1, "", "Accounting", "حسابداری" },
                    { 2, "", "Buy", "خرید" },
                    { 3, "", "Sale", "فروش" },
                    { 4, "", "Warehouse", "انبار" },
                    { 5, "", "Khazane", "خزانه داری" },
                    { 6, "", "Asset", "اموال" },
                    { 7, "", "Contract", "قراردادها" },
                    { 8, "", "Hoghoogh", "حقوق و دستمزد" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Name", "TafsilCode", "TafsilId" },
                values: new object[,]
                {
                    { 1, "بانک ملی ایران", null, null },
                    { 2, "ملت", null, null },
                    { 3, "تجارت", null, null },
                    { 4, "صادرات ایران", null, null },
                    { 5, "سامان", null, null },
                    { 6, "سپه", null, null },
                    { 7, "پارسیان", null, null },
                    { 8, "پاسارگاد", null, null },
                    { 9, "مهر اقتصاد", null, null },
                    { 10, "رفاه کارگران", null, null },
                    { 11, "آینده", null, null },
                    { 12, "شهر", null, null },
                    { 13, "رسالت", null, null },
                    { 14, "سینا", null, null },
                    { 15, "ایران زمین", null, null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "1", "دلار آمریکا" },
                    { 2, "2", "یورو" },
                    { 3, "3", "درهم امارات" },
                    { 4, "4", "ریال عمان" },
                    { 5, "5", "دینار کویت" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "فروش" },
                    { 2, 2, "اصلاحی" },
                    { 3, 3, "ابطال" },
                    { 4, 4, "برگشت از فروش" },
                    { 5, 5, "خرید" },
                    { 6, 6, "برگشت از خرید" }
                });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "1627", "عدد" },
                    { 2, "164", "کیلوگرم" },
                    { 3, "165", "متر" },
                    { 4, "1631", "دستگاه" },
                    { 5, "1637", "لیتر" },
                    { 6, "1628", "بسته" },
                    { 7, "1622", "گرم" },
                    { 8, "1676", "نفر" },
                    { 9, "16103", "ساعت" },
                    { 10, "169", "تن" },
                    { 11, "1614", "گالن" },
                    { 12, "1624", "کارتن" },
                    { 13, "1620", "دست" },
                    { 14, "161", "برگ" },
                    { 15, "1629", "پاکت" },
                    { 16, "1645", "مترمربع" },
                    { 17, "1643", "جفت" },
                    { 18, "1653", "واحد" },
                    { 19, "1640", "تخته" },
                    { 20, "1660", "شانه" },
                    { 21, "1654", "بندیل" },
                    { 22, "1687", "فاقد بسته بندی" },
                    { 23, "1638", "بطری" },
                    { 24, "1615", "کیسه" },
                    { 25, "1633", "سیلندر" },
                    { 26, "16121", "نفر-ساعت" },
                    { 27, "168", "حلب" },
                    { 28, "1665", "شیت" },
                    { 29, "1636", "جام" },
                    { 30, "16113", "سال" },
                    { 31, "16112", "ماه" },
                    { 32, "16104", "روز" },
                    { 33, "16111", "دقیقه" },
                    { 34, "16115", "سانتی متر" },
                    { 35, "16114", "قطعه" },
                    { 36, "16119", "گیگابایت بر ثانیه" },
                    { 37, "90", "نسخه (جلد)" },
                    { 38, "1611", "لنگه" },
                    { 39, "1612", "عدل" },
                    { 40, "1613", "جعبه" },
                    { 41, "1618", "توپ" },
                    { 42, "1619", "ست" },
                    { 43, "1641", "رول" }
                });

            migrationBuilder.InsertData(
                table: "PartyTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "1", "حقیقی" },
                    { 2, "2", "حقوقی" },
                    { 3, "3", "مشارکت مدنی" },
                    { 4, "4", "اتباع غیرایرانی" },
                    { 5, "5", "مصرف کننده نهایی" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "نقدی" },
                    { 2, 2, "واریز به حساب" },
                    { 3, 3, "چک" },
                    { 4, 4, "چک واگذاری" }
                });

            migrationBuilder.InsertData(
                table: "SettlementTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "نقد" },
                    { 2, 2, "نسیه" },
                    { 3, 3, "نقد/نسیه" }
                });

            migrationBuilder.InsertData(
                table: "productOrServiceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "1", "عمومی" },
                    { 2, "2", "قرارداد پیمانکاری" },
                    { 3, "3", "طلا، جواهر و پلاتین" },
                    { 4, "4", "قبوض خدماتی" },
                    { 5, "5", "بلیط هواپیما" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Articles_DocId",
                table: "Acc_Articles",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Articles_MoeinId",
                table: "Acc_Articles",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Coding_Kols_GroupId",
                table: "Acc_Coding_Kols",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Coding_Moeins_KolId",
                table: "Acc_Coding_Moeins",
                column: "KolId");

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Coding_TafsilToGroups_GroupId",
                table: "Acc_Coding_TafsilToGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Coding_TafsilToGroups_TafsilId",
                table: "Acc_Coding_TafsilToGroups",
                column: "TafsilId");

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Documents_PeriodId",
                table: "Acc_Documents",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AppIdentityUserAppRole_UsersId",
                table: "AppIdentityUserAppRole",
                column: "UsersId");

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
                name: "IX_AspNetUsers_CustomerId",
                table: "AspNetUsers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_CurrencyTypeId",
                table: "InvoiceItems",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductOrServiceId",
                table: "InvoiceItems",
                column: "ProductOrServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BuyerId",
                table: "Invoices",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SellerId",
                table: "Invoices",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SettlementTypeId",
                table: "Invoices",
                column: "SettlementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_parties_CustomerId",
                table: "parties",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_parties_TaxPayerType",
                table: "parties",
                column: "TaxPayerType");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRepresentatives_RepresentativeId",
                table: "PartyRepresentatives",
                column: "RepresentativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrServices_CategoryId",
                table: "ProductOrServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrServices_UnitOfMeasurementId",
                table: "ProductOrServices",
                column: "UnitOfMeasurementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acc_Articles");

            migrationBuilder.DropTable(
                name: "Acc_Coding_TafsilToGroups");

            migrationBuilder.DropTable(
                name: "Acc_CostCenters");

            migrationBuilder.DropTable(
                name: "Acc_DocStatuses");

            migrationBuilder.DropTable(
                name: "Acc_DocTypes");

            migrationBuilder.DropTable(
                name: "AppIdentityUserAppRole");

            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "AppSubsystems");

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
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "PartyRepresentatives");

            migrationBuilder.DropTable(
                name: "PartyTypes");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "productOrServiceTypes");

            migrationBuilder.DropTable(
                name: "UserSellers");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Acc_Coding_Moeins");

            migrationBuilder.DropTable(
                name: "Acc_Documents");

            migrationBuilder.DropTable(
                name: "Acc_Coding_TafsilGroups");

            migrationBuilder.DropTable(
                name: "Acc_Coding_Tafsils");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "ProductOrServices");

            migrationBuilder.DropTable(
                name: "Acc_Coding_Kols");

            migrationBuilder.DropTable(
                name: "Acc_FinancialPeriods");

            migrationBuilder.DropTable(
                name: "InvoiceTypes");

            migrationBuilder.DropTable(
                name: "SettlementTypes");

            migrationBuilder.DropTable(
                name: "parties");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "StuffCategories");

            migrationBuilder.DropTable(
                name: "Acc_Coding_Groups");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "TaxPayerTypes");
        }
    }
}
