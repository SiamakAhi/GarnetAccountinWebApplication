using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class CuGaeOldSys_SaleDataService : ICuGaeOldSys_SaleDataService
    {
        private readonly AppDbContext _db;
        public CuGaeOldSys_SaleDataService(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<SelectList> SelectList_BillOfLadingGroupAsync(long sellerId)
        {
            var lst = await _db.KPOldSystemSales
                .Where(n => n.SellerId == sellerId)
                .Select(n => new { id = n.BillOfLadingGroupCode, name = n.BillOfLadingGroup })
                .Distinct()
                .ToListAsync();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var x in lst)
            {
                items.Add(new SelectListItem { Value = x.id.Value.ToString(), Text = x.name });
            }

            return new SelectList(items, "Value", "Text");
        }
        public async Task<SelectList> SelectList_DestinationRepresentativesAsync(long sellerId)
        {
            var lst = await _db.KPOldSystemSales
                .Where(n => n.SellerId == sellerId)
                .Select(n => new { name = n.DestinationRepresentative })
                .Distinct()
                .ToListAsync();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var x in lst)
            {
                items.Add(new SelectListItem { Value = x.name, Text = x.name });
            }

            return new SelectList(items, "Value", "Text");
        }
        public async Task<SelectList> SelectList_AgencyAsync(long sellerId)
        {
            var lst = await _db.KPOldSystemSales
                .Where(n => n.SellerId == sellerId)
                .Select(n => new { name = n.AgencyName })
                .Distinct()
                .ToListAsync();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var x in lst)
            {
                items.Add(new SelectListItem { Value = x.name, Text = x.name });
            }

            return new SelectList(items, "Value", "Text");
        }
        public async Task<List<KPOldSystemSaleReport>> GetSalesAsync(SaleFilterDto filter)
        {
            var query = _db.KPOldSystemSales.Where(n => n.SellerId == filter.SellerId).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Agency))
                query = query.Where(r => r.AgencyName == filter.Agency);


            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate?.PersianToLatin();
                query = query.Where(r => r.MiladiDate >= filter.StartDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(r => r.MiladiDate <= filter.EndDate.Value);
            }

            if (filter.BillOfLadingNumber.HasValue)
            {
                query = query.Where(r => r.BillOfLadingNumber == filter.BillOfLadingNumber.Value);
            }

            if (filter.BillOfLadingType.HasValue)
            {
                query = query.Where(r => r.BillOfLadingGroupCode == filter.BillOfLadingType);
            }

            if (!string.IsNullOrEmpty(filter.CreditCustomer))
            {
                query = query.Where(r => r.CreditCompany == filter.CreditCustomer);
            }

            if (!string.IsNullOrEmpty(filter.PaymentMethod))
            {
                query = query.Where(r => r.PaymentMethod == filter.PaymentMethod);
            }

            if (filter.HasAccountingDoc.HasValue)
            {
                if (filter.HasAccountingDoc.Value)
                {
                    query = query.Where(r => r.DocId.HasValue);
                }
                else
                {
                    query = query.Where(r => !r.DocId.HasValue);
                }
            }
            if (!string.IsNullOrEmpty(filter.DestinationRepresentative))
                query = query.Where(n => n.DestinationRepresentative == filter.DestinationRepresentative);

            return await query.ToListAsync();
        }
        public IQueryable<KPOldSystemSaleReport> GetSalesAsQuery(SaleFilterDto filter)
        {
            var query = _db.KPOldSystemSales.AsNoTracking().Where(n => n.SellerId == filter.SellerId).AsQueryable();

            if (!filter.ShowUnFinal)
                query = query.Where(r => r.RecordLock == true);

            if (!string.IsNullOrEmpty(filter.Agency))
                query = query.Where(r => r.AgencyName == filter.Agency);

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate?.PersianToLatin();
                query = query.Where(r => r.MiladiDate >= filter.StartDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(r => r.MiladiDate <= filter.EndDate.Value);
            }

            if (filter.BillOfLadingNumber.HasValue)
            {
                query = query.Where(r => r.BillOfLadingNumber == filter.BillOfLadingNumber.Value);
            }

            if (filter.BillOfLadingType.HasValue)
            {
                query = query.Where(r => r.BillOfLadingGroupCode == filter.BillOfLadingType);
            }

            if (!string.IsNullOrEmpty(filter.CreditCustomer))
            {
                query = query.Where(r => r.CreditCompany == filter.CreditCustomer);
            }

            if (!string.IsNullOrEmpty(filter.PaymentMethod))
            {
                query = query.Where(r => r.PaymentMethod == filter.PaymentMethod);
            }

            if (filter.HasAccountingDoc.HasValue)
            {
                if (filter.HasAccountingDoc.Value)
                {
                    query = query.Where(r => r.DocId.HasValue);
                }
                else
                {
                    query = query.Where(r => !r.DocId.HasValue);
                }
            }
            if (!string.IsNullOrEmpty(filter.DestinationRepresentative))
                query = query.Where(n => n.DestinationRepresentative == filter.DestinationRepresentative);

            return query.AsQueryable();
        }
        public IQueryable<CuOld_SaleReportGrouped> DailyReport(SaleFilterDto filter)
        {
            var data = GetSalesAsQuery(filter);
            var report = data.GroupBy(n => n.MiladiDate.Value)
                .Select(n => new CuOld_SaleReportGrouped
                {
                    MiladiDate = n.Max(x => x.MiladiDate.Value),
                    DestinationRepresentative = n.Max(x => x.DestinationRepresentative),
                    AgencyName = n.Max(x => x.AgencyName),
                    BillOfLadingGroup = n.Max(x => x.BillOfLadingGroup),
                    BillOfLadingGroupCode = n.Max(x => x.BillOfLadingGroupCode),
                    CreditCompany = n.Max(x => x.CreditCompany),
                    FromOrigin = n.Max(x => x.FromOrigin),
                    SellerId = n.Max(x => x.SellerId),
                    PaymentMethod = n.Max(x => x.PaymentMethod),
                    ServiceCode = n.Max(x => x.ServiceCode),
                    ToDestination = n.Max(x => x.ToDestination),
                    DocNumber = n.Max(x => x.DocNumber),
                    TotalAddedValue = n.Sum(x => x.AddedValue),
                    TotalBaseFare = n.Sum(x => x.BaseFare),
                    TotalBillOfLadingAmount = n.Sum(x => x.TotalBillOfLadingAmount),
                    TotalCargoFare = n.Sum(x => x.CargoFare),
                    TotalCollectionOrSeparationFee = n.Sum(x => x.CollectionOrSeparationFee),
                    TotalDiscount = n.Sum(x => x.Discount),
                    TotalInsuranceFee = n.Sum(x => x.InsuranceFee),
                    TotalMiscellaneousFee = n.Sum(x => x.MiscellaneousFee),
                    TotalOtherOriginFees = n.Sum(x => x.OtherOriginFees),
                    TotalPackagingFee = n.Sum(x => x.PackagingFee),
                    TotalRoundingAmount = n.Sum(x => x.RoundingAmount),
                    TotalStampFee = n.Sum(x => x.StampFee),
                    TotalTotalServiceFee = n.Sum(x => x.TotalServiceFee),
                    DistributionOrSeparationFee = n.Sum(x => x.DistributionOrSeparationFee),
                    TotalDestinationMiscellaneousFee = n.Sum(x => x.DestinationMiscellaneousFee),
                    TotalVAT = n.Sum(x => x.VAT),

                    // بارهای وارده
                    InQty = n.Where(z => z.BillOfLadingGroupCode == 100).Count(),
                    InGoodsCount = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.GoodsCount),
                    InActualCargoWeight = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.ActualCargoWeight),
                    InPasKarayeh = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "پسکرایه").Sum(x => x.TotalBillOfLadingAmount),
                    InCash = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "نقدی").Sum(x => x.TotalBillOfLadingAmount),
                    InCreditable = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "اعتباری").Sum(x => x.TotalBillOfLadingAmount),
                    InRepresentativeShare = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.DistributionOrSeparationFee + x.DestinationMiscellaneousFee),

                    // بارهای ارسالی
                    OutQty = n.Where(z => z.BillOfLadingGroupCode != 100).Count(),
                    OutGoodsCount = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.GoodsCount),
                    OutActualCargoWeight = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.ActualCargoWeight),
                    OutPasKarayeh = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "پسکرایه").Sum(x => x.TotalBillOfLadingAmount),
                    OutCash = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "نقدی").Sum(x => x.TotalBillOfLadingAmount),
                    OutCreditable = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "اعتباری").Sum(x => x.TotalBillOfLadingAmount),
                    OutRepresentativeShare = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.DistributionOrSeparationFee + x.DestinationMiscellaneousFee),

                }).OrderBy(n => n.MiladiDate).AsQueryable();

            return report;
        }
        public IQueryable<CuOld_SaleReportGrouped> DailyReportByRepresentative(SaleFilterDto filter)
        {
            var data = GetSalesAsQuery(filter);
            var report = data.GroupBy(n => new { n.MiladiDate.Value, n.DestinationRepresentative })
                .Select(n => new CuOld_SaleReportGrouped
                {
                    MiladiDate = n.Max(x => x.MiladiDate.Value),
                    DestinationRepresentative = n.Key.DestinationRepresentative,
                    AgencyName = n.Max(x => x.AgencyName),
                    BillOfLadingGroup = n.Max(x => x.BillOfLadingGroup),
                    BillOfLadingGroupCode = n.Max(x => x.BillOfLadingGroupCode),
                    CreditCompany = n.Max(x => x.CreditCompany),
                    FromOrigin = n.Max(x => x.FromOrigin),
                    SellerId = n.Max(x => x.SellerId),
                    PaymentMethod = n.Max(x => x.PaymentMethod),
                    ServiceCode = n.Max(x => x.ServiceCode),
                    ToDestination = n.Max(x => x.ToDestination),
                    DocNumber = n.Max(x => x.DocNumber),
                    TotalAddedValue = n.Sum(x => x.AddedValue),
                    TotalBaseFare = n.Sum(x => x.BaseFare),
                    TotalBillOfLadingAmount = n.Sum(x => x.TotalBillOfLadingAmount),
                    TotalCargoFare = n.Sum(x => x.CargoFare),
                    TotalCollectionOrSeparationFee = n.Sum(x => x.CollectionOrSeparationFee),
                    TotalDiscount = n.Sum(x => x.Discount),
                    TotalInsuranceFee = n.Sum(x => x.InsuranceFee),
                    TotalMiscellaneousFee = n.Sum(x => x.MiscellaneousFee),
                    TotalOtherOriginFees = n.Sum(x => x.OtherOriginFees),
                    TotalPackagingFee = n.Sum(x => x.PackagingFee),
                    TotalRoundingAmount = n.Sum(x => x.RoundingAmount),
                    TotalStampFee = n.Sum(x => x.StampFee),
                    TotalTotalServiceFee = n.Sum(x => x.TotalServiceFee),
                    DistributionOrSeparationFee = n.Sum(x => x.DistributionOrSeparationFee),
                    TotalDestinationMiscellaneousFee = n.Sum(x => x.DestinationMiscellaneousFee),
                    TotalVAT = n.Sum(x => x.VAT),

                    // بارهای وارده
                    InQty = n.Where(z => z.BillOfLadingGroupCode == 100).Count(),
                    InGoodsCount = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.GoodsCount),
                    InActualCargoWeight = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.ActualCargoWeight),
                    InPasKarayeh = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "پسکرایه").Sum(x => x.TotalBillOfLadingAmount),
                    InCash = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "نقدی").Sum(x => x.TotalBillOfLadingAmount),
                    InCreditable = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "اعتباری").Sum(x => x.TotalBillOfLadingAmount),
                    InRepresentativeShare = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.DistributionOrSeparationFee + x.DestinationMiscellaneousFee),

                    // بارهای ارسالی
                    OutQty = n.Where(z => z.BillOfLadingGroupCode != 100).Count(),
                    OutGoodsCount = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.GoodsCount),
                    OutActualCargoWeight = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.ActualCargoWeight),
                    OutPasKarayeh = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "پسکرایه").Sum(x => x.TotalBillOfLadingAmount),
                    OutCash = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "نقدی").Sum(x => x.TotalBillOfLadingAmount),
                    OutCreditable = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "اعتباری").Sum(x => x.TotalBillOfLadingAmount),
                    OutRepresentativeShare = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.DistributionOrSeparationFee + x.DestinationMiscellaneousFee),

                }).OrderBy(n => n.MiladiDate).AsQueryable();

            return report;
        }

        public IQueryable<CuOld_SaleReportGrouped> RepresentativeReportAsync(SaleFilterDto filter)
        {
            var data = GetSalesAsQuery(filter);

            var report = data.GroupBy(n => new { n.DestinationRepresentative })
                .Select(n => new CuOld_SaleReportGrouped
                {
                    MiladiDate = n.Max(x => x.MiladiDate.Value),
                    DestinationRepresentative = n.Key.DestinationRepresentative,
                    AgencyName = n.Max(x => x.AgencyName),
                    BillOfLadingGroup = n.Max(x => x.BillOfLadingGroup),
                    BillOfLadingGroupCode = n.Max(x => x.BillOfLadingGroupCode),
                    CreditCompany = n.Max(x => x.CreditCompany),
                    FromOrigin = n.Max(x => x.FromOrigin),
                    SellerId = n.Max(x => x.SellerId),
                    PaymentMethod = n.Max(x => x.PaymentMethod),
                    ServiceCode = n.Max(x => x.ServiceCode),
                    ToDestination = n.Max(x => x.ToDestination),
                    DocNumber = n.Max(x => x.DocNumber),
                    TotalAddedValue = n.Sum(x => x.AddedValue),
                    TotalBaseFare = n.Sum(x => x.BaseFare),
                    TotalBillOfLadingAmount = n.Sum(x => x.TotalBillOfLadingAmount),
                    TotalCargoFare = n.Sum(x => x.CargoFare),
                    TotalCollectionOrSeparationFee = n.Sum(x => x.CollectionOrSeparationFee),
                    TotalDiscount = n.Sum(x => x.Discount),
                    TotalInsuranceFee = n.Sum(x => x.InsuranceFee),
                    TotalMiscellaneousFee = n.Sum(x => x.MiscellaneousFee),
                    TotalOtherOriginFees = n.Sum(x => x.OtherOriginFees),
                    TotalPackagingFee = n.Sum(x => x.PackagingFee),
                    TotalRoundingAmount = n.Sum(x => x.RoundingAmount),
                    TotalStampFee = n.Sum(x => x.StampFee),
                    TotalTotalServiceFee = n.Sum(x => x.TotalServiceFee),
                    DistributionOrSeparationFee = n.Sum(x => x.DistributionOrSeparationFee),
                    TotalDestinationMiscellaneousFee = n.Sum(x => x.DestinationMiscellaneousFee),
                    TotalVAT = n.Sum(x => x.VAT),

                    // بارهای وارده
                    InQty = n.Where(z => z.BillOfLadingGroupCode == 100).Count(),
                    InGoodsCount = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.GoodsCount),
                    InActualCargoWeight = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.ActualCargoWeight),
                    InPasKarayeh = n.Where(a => a.BillOfLadingGroupCode == 100 && (a.PaymentMethod == "پسکرایه" || a.PaymentMethod == "اعتباری")).Sum(x => x.TotalBillOfLadingAmount),
                    InCash = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "نقدی").Sum(x => x.TotalBillOfLadingAmount),
                    InCreditable = n.Where(a => a.BillOfLadingGroupCode == 100 && a.PaymentMethod == "اعتباری").Sum(x => x.TotalBillOfLadingAmount),
                    InRepresentativeShare = n.Where(a => a.BillOfLadingGroupCode == 100).Sum(x => x.DistributionOrSeparationFee + x.DestinationMiscellaneousFee),

                    // بارهای ارسالی
                    OutQty = n.Where(z => z.BillOfLadingGroupCode != 100).Count(),
                    OutGoodsCount = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.GoodsCount),
                    OutActualCargoWeight = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.ActualCargoWeight),
                    OutPasKarayeh = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "پسکرایه").Sum(x => x.TotalBillOfLadingAmount),
                    OutCash = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "نقدی").Sum(x => x.TotalBillOfLadingAmount),
                    OutCreditable = n.Where(a => a.BillOfLadingGroupCode != 100 && a.PaymentMethod == "اعتباری").Sum(x => x.TotalBillOfLadingAmount),
                    OutRepresentativeShare = n.Where(a => a.BillOfLadingGroupCode != 100).Sum(x => x.DistributionOrSeparationFee + x.DestinationMiscellaneousFee),

                }).AsQueryable();


            return report;
        }
    }
}
