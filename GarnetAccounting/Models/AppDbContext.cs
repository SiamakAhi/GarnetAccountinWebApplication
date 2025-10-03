using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Areas.Accounting.Models.Mapping;
using GarnetAccounting.Areas.AutoServiceAgency.Models.Enities;
using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;
using GarnetAccounting.Areas.AutoServiceAgency.Models.EntitiesConfig;
using GarnetAccounting.Areas.AvaRasta.Models.Entities;
using GarnetAccounting.Areas.AvaRasta.Models.Mapping;
using GarnetAccounting.Areas.Commercial.Models.Entities;
using GarnetAccounting.Areas.Commercial.Models.Mapping;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Areas.Courier.Models.Mapping;
using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Areas.Geolocation.Models.Entities;
using GarnetAccounting.Areas.Geolocation.Models.EntityConfig;
using GarnetAccounting.Areas.Organization.Models.Entities;
using GarnetAccounting.Areas.Organization.Models.modelConfig;
using GarnetAccounting.Areas.Projects.Models.Entities;
using GarnetAccounting.Areas.Projects.Models.EntityConfigs;
using GarnetAccounting.Areas.Treasury.Models.Entities;
using GarnetAccounting.Areas.Treasury.Models.Mapping;
using GarnetAccounting.Areas.Warehouse.Models.Entities;
using GarnetAccounting.Areas.Warehouse.Models.Mapping;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.Models.Identity;
using GarnetAccounting.Models.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Models
{
    public class AppDbContext : IdentityDbContext<
        AppIdentityUser
        , AppRole
        , string
        , IdentityUserClaim<string>
        , IdentityUserRole<string>
        , IdentityUserLogin<string>
        , IdentityRoleClaim<string>
        , IdentityUserToken<string>
        >
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)

        {
            base.OnModelCreating(builder);

            //.......
            builder.ApplyConfiguration<License>(new LicenseMapp());
            builder.ApplyConfiguration<AppSubsystem>(new AppSubsystem_Mapp());
            builder.ApplyConfiguration<AppSettings>(new AppSettingMapp());
            builder.ApplyConfiguration<AppTheme>(new AppThemeMapp());


            builder.ApplyConfiguration<AppIdentityUser>(new AppIdentityUser_Map());
            builder.ApplyConfiguration<Currency>(new Currency_Mapp());
            builder.ApplyConfiguration<PartyType>(new PartyType_Mapp());
            builder.ApplyConfiguration<Party>(new Party_Mapp());
            builder.ApplyConfiguration<PartyRepresentative>(new PartyRepresentative_Map());

            //Organization
            builder.ApplyConfiguration<OrgDepartment>(new OrgDepartmentMapp());
            builder.ApplyConfiguration<OrgPosition>(new OrgPositionMapp());
            builder.ApplyConfiguration<OrgEmployee>(new OrgEmployeeMapp());

            //Accountin
            builder.ApplyConfiguration<Acc_Coding_Group>(new Acc_Coding_Group_Mapp());
            builder.ApplyConfiguration<Acc_Coding_Kol>(new Acc_Coding_Kol_Mapp());
            builder.ApplyConfiguration<Acc_Coding_Moein>(new Acc_Coding_Moein_Mapp());
            builder.ApplyConfiguration<Acc_Coding_TafsilGroup>(new Acc_Coding_TafsilGroup_Mapp());
            builder.ApplyConfiguration<Acc_Coding_Tafsil>(new Acc_Coding_Tafsil_Mapp());
            builder.ApplyConfiguration<Acc_Coding_TafsilToGroup>(new Acc_Coding_TafsilToGroup_Mapp());
            builder.ApplyConfiguration<Acc_DocType>(new Acc_DocType_Map());
            builder.ApplyConfiguration<Acc_DocStatus>(new Acc_DocStatus_Mapp());
            builder.ApplyConfiguration<Acc_Document>(new Acc_Document_Mapp());
            builder.ApplyConfiguration<Acc_Article>(new Acc_Article_Mapp());
            builder.ApplyConfiguration<Con_Project>(new Con_Project_Config());

            //Treasury
            builder.ApplyConfiguration<kh_Bank>(new Kh_Bank_Map());
            builder.ApplyConfiguration<kh_BankAccount>(new Kh_BankAccount_Map());
            builder.ApplyConfiguration<TreCarrency>(new TreCarrency_Map());



            //Warehouse
            builder.ApplyConfiguration<Wh_ProductCategory>(new Wh_ProductCategoryMapp());
            builder.ApplyConfiguration<Wh_Product>(new Wh_ProductMapp());
            builder.ApplyConfiguration<Wh_ProductUnit>(new Wh_ProductUnitMapp());
            builder.ApplyConfiguration<Wh_Warehouse>(new Wh_WarehouseMapp());
            builder.ApplyConfiguration<Wh_WarehouseLocation>(new Wh_WarehouseLocationMapp());
            builder.ApplyConfiguration<Wh_WarehouseDocument>(new Wh_WarehouseDocumentMapp());
            builder.ApplyConfiguration<Wh_WarehouseDocumentItem>(new Wh_WarehouseDocumentItemMapp());
            builder.ApplyConfiguration<Wh_Inventory>(new Wh_InventoryMapp());
            builder.ApplyConfiguration<Wh_InventoryTransaction>(new Wh_InventoryTransactionMapp());

            // Commercial
            builder.ApplyConfiguration<com_Invoice>(new Com_InvoiceMapp());
            builder.ApplyConfiguration<com_InvoiceItem>(new Com_InvoiceItemMapp());

            // Courier
            builder.ApplyConfiguration<Cu_Representative>(new Cu_RepresentativeMap());
            builder.ApplyConfiguration<Cu_Branch>(new Cu_BranchMap());
            builder.ApplyConfiguration<Cu_BranchUser>(new Cu_BranchUserMapp());
            builder.ApplyConfiguration<Cu_BillCost>(new Cu_BillCost_Config());
            builder.ApplyConfiguration<Cu_BillOfLading>(new Cu_BillOfLading_Config());
            builder.ApplyConfiguration<Cu_BillOfLadingStatus>(new Cu_BillOfLadingStatus_Config());
            builder.ApplyConfiguration<Cu_Consignment>(new Cu_Consignment_Config());
            builder.ApplyConfiguration<Cu_ConsignmentNature>(new Cu_ConsignmentNature_Config());
            builder.ApplyConfiguration<Cu_Hub>(new Cu_HubMapp());
            builder.ApplyConfiguration<Cu_PaymentType>(new Cu_PaymentType_Config());
            builder.ApplyConfiguration<Cu_RateBaseKValue>(new Cu_RateBaseKValue_Config());
            builder.ApplyConfiguration<Cu_RateImpactType>(new Cu_RateImpactType_Config());
            builder.ApplyConfiguration<Cu_RateWeightRange>(new Cu_RateWeightRange_Config());
            builder.ApplyConfiguration<Cu_RateZone>(new Cu_RateZone_Config());
            builder.ApplyConfiguration<Cu_Route>(new Cu_Route_Config());
            builder.ApplyConfiguration<Cu_Service>(new Cu_Service_Config());
            builder.ApplyConfiguration<Cu_ShipmentType>(new Cu_ShipmentType_Config());
            builder.ApplyConfiguration<Cu_Packaging>(new Cu_Packaging_Config());

            // Geolocation
            builder.ApplyConfiguration<Geo_Country>(new Geo_CountryConfiguration());
            builder.ApplyConfiguration<Geo_Province>(new Geo_ProvinceConfiguration());
            builder.ApplyConfiguration<Geo_City>(new Geo_CityConfiguration());

            //================================================= AutoServiceAgency =================================
            builder.ApplyConfiguration<asa_Contractor>(new asa_Contractor_Config());
            builder.ApplyConfiguration<asa_Reception>(new asa_Reception_Config());
        }

        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppIdentityUser>>();

                // ایجاد نقش‌ها
                List<AppRole> roleList = new List<AppRole>();
                roleList.Add(new AppRole { Name = "Admin", Description = "مدیر سیستم" });
                roleList.Add(new AppRole { Name = "User", Description = "کاربر" });
                roleList.Add(new AppRole { Name = "CustomerOwner", Description = "کاربر مشتری" });
                roleList.Add(new AppRole { Name = "CustomerUser", Description = "مشتری" });
                roleList.Add(new AppRole { Name = "OnlineShop", Description = "انلاین شاپ" });
                roleList.Add(new AppRole { Name = "Support", Description = "پشتیبانی" });
                roleList.Add(new AppRole { Name = "AccountingManager", Description = "مدیرامور مالی" });
                roleList.Add(new AppRole { Name = "AccountingBoss", Description = "رئیس حسابداری" });
                roleList.Add(new AppRole { Name = "AccountingUser", Description = "کمک حسابدار" });
                roleList.Add(new AppRole { Name = "SaleLanager", Description = "مدیر فروش" });
                roleList.Add(new AppRole { Name = "SaleSupport", Description = "پشتیبانی فروش" });
                roleList.Add(new AppRole { Name = "Seller", Description = "کارشناس فروش" });
                roleList.Add(new AppRole { Name = "BuyerExpert", Description = "کارشناس خرید" });
                roleList.Add(new AppRole { Name = "CommercialManager", Description = "مدیر بازرگانی" });
                roleList.Add(new AppRole { Name = "Peyk", Description = "پیک" });
                roleList.Add(new AppRole { Name = "Manager", Description = "مدیر" });
                roleList.Add(new AppRole { Name = "courier", Description = "شرکت خدمات بار" });
                roleList.Add(new AppRole { Name = "CourierMan", Description = "پیک موتوری" });
                roleList.Add(new AppRole { Name = "BranchManager", Description = "مسئول امور شعب و نمایندگی‌ها" });
                roleList.Add(new AppRole { Name = "InterCityDriver", Description = "ماشین حمل بار برون شهری" });
                roleList.Add(new AppRole { Name = "Distributor", Description = "نماینده توزیع" });
                roleList.Add(new AppRole { Name = "Representative", Description = "نماینده فروش" });
                roleList.Add(new AppRole { Name = "HubManager", Description = "تجزیه مبادلات کالا" });
                roleList.Add(new AppRole { Name = "RepresentativeUser", Description = "کاربر نماینده" });
                roleList.Add(new AppRole { Name = "IsIntercityFleet", Description = "ناوگان حمل بین شهری" });

                foreach (var roleName in roleList)
                {
                    if (!await roleManager.RoleExistsAsync(roleName.Name))
                    {
                        var role = new AppRole
                        {
                            Name = roleName.Name,
                            Description = roleName.Description,
                        };
                        await roleManager.CreateAsync(role);
                    }
                }

                // ایجاد کاربر ادمین
                var adminUser = new AppIdentityUser
                {
                    UserName = "admin",
                    Email = "Ahi.siamak@gmail.com",
                    FName = "آوا اندیش",
                    Family = "رستـا",
                    Mobile = "09161114954",
                    PhoneNumber = "06131010369",
                    IsActive = true,
                    Gender = 1,
                    RegistrDate = DateTime.Now,
                };

                if (userManager.Users.All(u => u.UserName != adminUser.UserName))
                {
                    var createUser = await userManager.CreateAsync(adminUser, "Ava@123456");
                    if (createUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }

        //Licenses 
        public DbSet<License> Licenses { get; set; }
        public DbSet<Module> Modules { get; set; }
        //.....


        public DbSet<AppSubsystem> AppSubsystems { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<AppTheme> AppThemes { get; set; }
        //------------
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserSeller> UserSellers { get; set; }
        public DbSet<TaxPayerType> TaxPayerTypes { get; set; }
        public DbSet<PartyType> PartyTypes { get; set; }
        public DbSet<Party> parties { get; set; }
        public DbSet<TaxpayerInfo> TaxpayerInfos { get; set; }

        public DbSet<PartyRepresentative> PartyRepresentatives { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        //Organization
        public DbSet<OrgDepartment> OrgDepartments { get; set; }
        public DbSet<OrgPosition> OrgPositions { get; set; }
        public DbSet<OrgEmployee> OrgEmployees { get; set; }

        //Accounting
        public DbSet<Acc_Coding_Group> Acc_Coding_Groups { get; set; }
        public DbSet<Acc_Coding_Kol> Acc_Coding_Kols { get; set; }
        public DbSet<Acc_Coding_Moein> Acc_Coding_Moeins { get; set; }
        public DbSet<Acc_Coding_TafsilGroup> Acc_Coding_TafsilGroups { get; set; }
        public DbSet<Acc_Coding_Tafsil> Acc_Coding_Tafsils { get; set; }
        public DbSet<Acc_Coding_TafsilToGroup> Acc_Coding_TafsilToGroups { get; set; }
        public DbSet<Acc_FinancialPeriod> Acc_FinancialPeriods { get; set; }
        public DbSet<Acc_CostCenter> Acc_CostCenters { get; set; }
        public DbSet<Acc_DocType> Acc_DocTypes { get; set; }
        public DbSet<Acc_DocStatus> Acc_DocStatuses { get; set; }
        public DbSet<Acc_Document> Acc_Documents { get; set; }
        public DbSet<Acc_Article> Acc_Articles { get; set; }
        public DbSet<Acc_Setting> Acc_Settings { get; set; }

        public DbSet<Acc_MoadianReport> Acc_ModianReports { get; set; }
        public DbSet<Con_Project> Con_Projects { get; set; }
        public DbSet<Acc_ArchiveNumber> Acc_ArchiveNumbers { get; set; }
        public DbSet<Acc_ElectronicBookMetaData> Acc_ElectronicBookMetaData { get; set; }



        //--Treasury
        public DbSet<kh_Bank> Banks { get; set; }
        public DbSet<kh_BankAccount> BankAccounts { get; set; }
        public DbSet<TreCarrency> TreCarrencies { get; set; }
        public DbSet<TreBankTransaction> TreBankTransactions { get; set; }



        //Warehouse
        public DbSet<Wh_ProductCategory> Wh_ProductCategories { get; set; }
        public DbSet<Wh_Product> Wh_Products { get; set; }
        public DbSet<Wh_ProductUnit> Wh_ProductUnits { get; set; }
        public DbSet<Wh_UnitOfMeasure> Wh_UnitOfMeasures { get; set; }
        public DbSet<Wh_Warehouse> Wh_Warehouses { get; set; }
        public DbSet<Wh_WarehouseLocation> Wh_WarehouseLocations { get; set; }
        public DbSet<Wh_WarehouseDocument> Wh_WarehouseDocuments { get; set; }
        public DbSet<Wh_WarehouseDocumentItem> Wh_WarehouseDocumentItems { get; set; }
        public DbSet<Wh_Inventory> Wh_Inventories { get; set; }
        public DbSet<Wh_InventoryTransaction> Wh_InventoryTransactions { get; set; }


        //--Commertial
        public DbSet<com_Invoice> Invoices { get; set; }
        public DbSet<com_InvoiceItem> InvoiceItems { get; set; }

        //Geolocation
        public DbSet<Geo_Country> Geo_Countries { get; set; }
        public DbSet<Geo_Province> Geo_Provinces { get; set; }
        public DbSet<Geo_City> Geo_Cities { get; set; }

        //--Courier
        public DbSet<KPOldSystemSaleReport> KPOldSystemSales { get; set; }
        public DbSet<Cu_Representative> Representatives { get; set; }
        public DbSet<Cu_Branch> Cu_Branch { get; set; }
        public DbSet<Cu_BranchUser> Cu_BranchUser { get; set; }
        public DbSet<Cu_BillCost> Cu_BillCosts { get; set; }
        public DbSet<Cu_BillOfLading> Cu_BillOfLadings { get; set; }
        public DbSet<Cu_BillOfLadingCostItem> Cu_BillOfLadingCostItems { get; set; }
        public DbSet<Cu_BillOfLadingStatus> Cu_BillOfLadingStatuses { get; set; }
        public DbSet<Cu_Consignment> Cu_Consignments { get; set; }
        public DbSet<Cu_ConsignmentNature> Cu_ConsignmentNatures { get; set; }
        public DbSet<Cu_FinancialTransaction> Cu_FinancialTransactions { get; set; }
        public DbSet<Cu_FinancialTransactionOperation> Cu_FinancialTransactionOperations { get; set; }
        public DbSet<Cu_FinancialTransactionType> Cu_FinancialTransactionTypes { get; set; }
        public DbSet<Cu_PaymentType> Cu_PaymentTypes { get; set; }
        public DbSet<Cu_Hub> Cu_Hubs { get; set; }
        public DbSet<Cu_RateBaseKValue> Cu_RateBaseKValues { get; set; }
        public DbSet<Cu_RateImpactType> Cu_RateImpactTypes { get; set; }
        public DbSet<Cu_RateWeightRange> Cu_RateWeightRanges { get; set; }
        public DbSet<Cu_RateZone> Cu_RateZones { get; set; }
        public DbSet<Cu_Route> Cu_Routes { get; set; }
        public DbSet<Cu_Service> Cu_Services { get; set; }
        public DbSet<Cu_ShipmentType> Cu_Shipments { get; set; }
        public DbSet<Cu_Packaging> Cu_Packagings { get; set; }

        //-----------------------------------------------------------------------------------------------------
        //================================================= AutoServiceAgency =================================
        public DbSet<asa_Contractor> Asa_Contractors { get; set; }
        public DbSet<asa_Reception> Asa_Receptions { get; set; }
        public DbSet<asa_Services> Asa_Services { get; set; }
        public DbSet<asa_Setting> Asa_Settings { get; set; }
        public DbSet<asa_LamariService> Asa_LamariServices { get; set; }


    }
}
