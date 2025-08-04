using ClosedXML.Excel;
using EFCore.BulkExtensions;
using GarnetAccounting.Areas.DataTransfer.DataTransferInterfaces;
using GarnetAccounting.Areas.DataTransfer.Dto;
using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Areas.Representatives.ViewModels;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// Status
// 1 - جدید
// 2 - 
namespace GarnetAccounting.Areas.DataTransfer.DataTransferServices
{
    public class KPDataTransferService : IKPDataTransferService
    {
        private readonly AppDbContext _db;
        public KPDataTransferService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<clsResult> ImportFromExcelAsync(IFormFile ExcelFile, long sellerId)
        {
            var result = new clsResult();
            var reports = new List<KPOldSystemSaleReport>();

            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                result.Success = false;
                result.Message = "فرمت فایل معتبر نیست.";
                return result;
            }
            try
            {
                using (var stream = new MemoryStream())
                {
                    ExcelFile.CopyTo(stream);
                    stream.Position = 0;
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rowCount = worksheet.RowsUsed().Count();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string shamsiDate = worksheet.Cell(row, 6).GetValue<string>();
                            DateTime? miladiDate = null;

                            if (!string.IsNullOrEmpty(shamsiDate) && shamsiDate.Length == 8)
                            {
                                int year = int.Parse(shamsiDate.Substring(0, 4));
                                int month = int.Parse(shamsiDate.Substring(4, 2));
                                int day = int.Parse(shamsiDate.Substring(6, 2));
                                string persianDate = year.ToString("0000") + "/" + month.ToString("00") + "/" + day.ToString("00");
                                miladiDate = persianDate.PersianToLatin();
                            }
                            else
                                continue;

                            long? number = GetNullableLong(worksheet.Cell(row, 5));
                            if (number == null)
                                continue;

                            bool isDupp = await isDupplicateBillOfLandingAsync(number.Value, sellerId);
                            if (isDupp)
                            {
                                result.Message += $"<br> - بارنامه {number} قبلا ثبت شده است";
                                continue;
                            }
                            bool? cancelation = worksheet.Cell(row, 64).GetValue<bool?>();
                            if (cancelation == true)
                                continue;

                            bool? recordLock = worksheet.Cell(row, 70).GetValue<bool?>();
                            long? serviceGroupCode = GetNullableLong(worksheet.Cell(row, 71));
                            //if (recordLock != true && serviceGroupCode != 100)
                            //    continue;

                            try
                            {
                                var report = new KPOldSystemSaleReport
                                {
                                    SellerId = sellerId,
                                    MachineCode = GetNullableLong(worksheet.Cell(row, 1)),
                                    BillOfLadingGroup = worksheet.Cell(row, 2).GetValue<string>(),
                                    RegionName = worksheet.Cell(row, 3).GetValue<string>(),
                                    BillOfLadingNumber = GetNullableLong(worksheet.Cell(row, 5)),
                                    NonSystemicBillOfLadingNumber = GetNullableLong(worksheet.Cell(row, 5)),
                                    ShamsiDate = worksheet.Cell(row, 6).GetValue<string>(),
                                    MiladiDate = miladiDate,
                                    Time = worksheet.Cell(row, 8).GetValue<string>(),
                                    AgencyName = worksheet.Cell(row, 9).GetValue<string>(),
                                    CargoFare = GetNullableLong(worksheet.Cell(row, 10)),
                                    StampFee = GetNullableLong(worksheet.Cell(row, 11)),
                                    CollectionOrSeparationFee = GetNullableLong(worksheet.Cell(row, 12)),
                                    DeclaredGoodsValue = GetNullableLong(worksheet.Cell(row, 13)),
                                    PackagingFee = GetNullableLong(worksheet.Cell(row, 14)),
                                    InsuranceFee = GetNullableLong(worksheet.Cell(row, 15)),
                                    Discount = GetNullableLong(worksheet.Cell(row, 16)),
                                    VAT = GetNullableLong(worksheet.Cell(row, 17)),
                                    OtherOriginFees = GetNullableLong(worksheet.Cell(row, 18)),
                                    RoundingAmount = GetNullableLong(worksheet.Cell(row, 19)),
                                    MiscellaneousFee = GetNullableLong(worksheet.Cell(row, 20)),
                                    TransitCargoFare = GetNullableLong(worksheet.Cell(row, 21)),
                                    TransitSeparationFee = GetNullableLong(worksheet.Cell(row, 22)),
                                    TransitMiscellaneousFee = GetNullableLong(worksheet.Cell(row, 23)),
                                    DistributionOrSeparationFee = GetNullableLong(worksheet.Cell(row, 24)),
                                    OldSeparationFee = GetNullableLong(worksheet.Cell(row, 25)),
                                    DestinationMiscellaneousFee = GetNullableLong(worksheet.Cell(row, 26)),
                                    BaseFare = GetNullableLong(worksheet.Cell(row, 27)),
                                    AddedValue = GetNullableLong(worksheet.Cell(row, 28)),
                                    TotalServiceFee = GetNullableLong(worksheet.Cell(row, 29)),
                                    TotalBillOfLadingAmount = GetNullableLong(worksheet.Cell(row, 30)),
                                    FromOrigin = worksheet.Cell(row, 31).GetValue<string>(),
                                    ToTransitDestination = worksheet.Cell(row, 32).GetValue<string>(),
                                    TransitRepresentative = worksheet.Cell(row, 33).GetValue<string>(),
                                    ToDestination = worksheet.Cell(row, 34).GetValue<string>(),
                                    DestinationRepresentative = worksheet.Cell(row, 35).GetValue<string>(),
                                    CustomerCode = GetNullableLong(worksheet.Cell(row, 36)),
                                    SenderName = worksheet.Cell(row, 37).GetValue<string>(),
                                    SenderAddress = worksheet.Cell(row, 38).GetValue<string>(),
                                    SenderPhone = worksheet.Cell(row, 39).GetValue<string>(),
                                    SenderNationalCode = worksheet.Cell(row, 40).GetValue<string>(),
                                    RecipientCustomerCode = GetNullableLong(worksheet.Cell(row, 41)),
                                    RecipientName = worksheet.Cell(row, 42).GetValue<string>(),
                                    RecipientAddress = worksheet.Cell(row, 43).GetValue<string>(),
                                    RecipientPhone = worksheet.Cell(row, 44).GetValue<string>(),
                                    RecipientCode = worksheet.Cell(row, 45).GetValue<string>(),
                                    RecipientZoneAddress = worksheet.Cell(row, 46).GetValue<string>(),
                                    RecipientZoneName = worksheet.Cell(row, 47).GetValue<string>(),
                                    SenderZoneCode = worksheet.Cell(row, 48).GetValue<string>(),
                                    SenderZoneAddress = worksheet.Cell(row, 49).GetValue<string>(),
                                    SenderZoneName = worksheet.Cell(row, 50).GetValue<string>(),
                                    RateType = worksheet.Cell(row, 51).GetValue<string>(),
                                    GoodsCount = GetNullableLong(worksheet.Cell(row, 52)),
                                    VolumetricWeight = GetNullableFloat(worksheet.Cell(row, 53)),
                                    ChargeableWeight = GetNullableFloat(worksheet.Cell(row, 54)),
                                    ActualCargoWeight = GetNullableFloat(worksheet.Cell(row, 55)),
                                    Contents = worksheet.Cell(row, 56).GetValue<string>(),
                                    PaymentMethod = worksheet.Cell(row, 57).GetValue<string>(),
                                    CreditCompany = worksheet.Cell(row, 58).GetValue<string>(),
                                    ServiceInformation = worksheet.Cell(row, 59).GetValue<string>(),
                                    FinancialInformation = worksheet.Cell(row, 60).GetValue<string>(),
                                    CourierName = worksheet.Cell(row, 61).GetValue<string>(),
                                    POSReceiptNumber = worksheet.Cell(row, 62).GetValue<string>(),
                                    DeliveryConfirmation = worksheet.Cell(row, 63).GetValue<string>(),
                                    Cancellation = worksheet.Cell(row, 64).GetValue<bool?>(),
                                    CancellationDate = GetNullableDateTime(worksheet.Cell(row, 65)),
                                    CancellationUser = worksheet.Cell(row, 66).GetValue<string>(),
                                    CancellationPenalty = GetNullableLong(worksheet.Cell(row, 67)),
                                    ServiceCode = GetNullableLong(worksheet.Cell(row, 68)),
                                    DataEntryDate = worksheet.Cell(row, 69).GetValue<string>(),
                                    RecordLock = recordLock,
                                    BillOfLadingGroupCode = GetNullableLong(worksheet.Cell(row, 71)),
                                    DataEntryUserCode = GetNullableLong(worksheet.Cell(row, 72)),
                                    DataEntryUserName = worksheet.Cell(row, 73).GetValue<string>(),
                                    EditDate = GetNullableDateTime(worksheet.Cell(row, 74)),
                                    EditUserName = worksheet.Cell(row, 75).GetValue<string>(),
                                    Remarks = worksheet.Cell(row, 76).GetValue<string>(),
                                    ErrorMessage = string.Empty
                                };

                                reports.Add(report);
                            }
                            catch
                            {
                                result.Success = false;
                                result.Message += $"<br> - خطایی در ردیف {row} وجود دارد";
                            }
                        }
                    }

                    List<KPOldSystemSaleReport> dataToSave = reports.Where(n =>
                    n.BillOfLadingGroup != null
                    && n.RegionName != null).ToList();

                    if (dataToSave.Count > 0)
                    {
                        //if (sellerId == 120)
                        //{
                        //    dataToSave = reports.Where(n =>
                        //        n.BillOfLadingGroup != null
                        //        && n.RegionName != null
                        //       && n.BillOfLadingNumber.ToString().StartsWith("5160")
                        //       || n.BillOfLadingNumber.ToString().StartsWith("5163")).ToList();
                        //}
                        //else
                        //{
                        //    dataToSave = reports.Where(n =>
                        //       n.BillOfLadingGroup != null
                        //       && n.RegionName != null
                        //      && !n.BillOfLadingNumber.ToString().StartsWith("5160")
                        //      && !n.BillOfLadingNumber.ToString().StartsWith("5163")).ToList();
                        //}
                        _db.KPOldSystemSales.AddRange(dataToSave);
                        await _db.SaveChangesAsync();

                        result.Success = true;
                        result.Message += $"<br> - تعداد {dataToSave.Count} رکورد با موفقیت ثبت شد";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"<br>  مشکلی در ورود اطلاعات رخ داده است: {ex.Message}";
            }

            return result;
        }
        private static long? GetNullableLong(IXLCell cell)
        {
            if (cell.IsEmpty() || string.IsNullOrEmpty(cell.GetString()))
                return null;

            if (long.TryParse(cell.GetString(), out long result))
                return result;

            return null;
        }
        private static float? GetNullableFloat(IXLCell cell)
        {
            if (cell.IsEmpty() || string.IsNullOrEmpty(cell.GetString()))
                return null;

            if (float.TryParse(cell.GetString(), out float result))
                return result;

            return null;
        }
        private static DateTime? GetNullableDateTime(IXLCell cell)
        {
            if (cell.IsEmpty() || string.IsNullOrEmpty(cell.GetString()))
                return null;

            if (DateTime.TryParse(cell.GetString(), out DateTime result))
                return result;

            return null;
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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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

            if (!string.IsNullOrEmpty(filter.OldDistRepName))
                query = query.Where(n => n.DestinationRepresentative == filter.OldDistRepName);

            if (!string.IsNullOrEmpty(filter.OldBranchName))
                query = query.Where(n => n.AgencyName == filter.OldBranchName);

            if (!string.IsNullOrEmpty(filter.DestinationRepresentative))
                query = query.Where(n => n.DestinationRepresentative == filter.DestinationRepresentative);

            return await query.ToListAsync();
        }
        public async Task<KPOldSystemSaleReport?> FindBillofladdingByIdAsync(long Id)
        {
            return await _db.KPOldSystemSales.AsNoTracking().FirstOrDefaultAsync(n => n.Id == Id);
        }
        public async Task<bool> isDupplicateBillOfLandingAsync(long landingNulmber, long sellerId)
        {
            var result = await _db.KPOldSystemSales.SingleOrDefaultAsync(n =>
            n.BillOfLadingNumber == landingNulmber
            && n.SellerId == sellerId);
            if (result == null)
            {
                return false;
            }
            else
            {
                if (result.DocId != null)
                {
                    return true;
                }
                _db.KPOldSystemSales.Remove(result);
                return !Convert.ToBoolean(await _db.SaveChangesAsync());
            }
        }
        public async Task<List<ImportSaleDocDto>> PrepareSalesForAccountingAsync(long[] ids, long sellerId)
        {
            List<ImportSaleDocDto> lst = new List<ImportSaleDocDto>();
            if (ids.Length == 0 || ids == null)
                return lst;

            foreach (long id in ids)
            {
                var sale = await _db.KPOldSystemSales.FindAsync(id);
                if (sale.RecordLock != true && sale.BillOfLadingGroupCode != 100)
                {
                    await SetErrorAsync(id, "بارنامه نهایی نشده است");
                    continue;
                }

                if (sale.DocNumber > 0 || sale.DocId != null)
                    continue;

                // درآمد حمل بار
                long hamleBar = 0;
                if (sale.CargoFare.HasValue)
                    hamleBar += sale.CargoFare.Value;
                if (sale.StampFee.HasValue)
                    hamleBar += sale.StampFee.Value;

                if (sale.InsuranceFee.HasValue)
                    hamleBar += sale.InsuranceFee.Value;
                if (sale.Discount.HasValue)
                    hamleBar -= sale.Discount.Value;

                // مبلغ روند فاکتور
                long roundingAmount = 0;
                if (sale.RoundingAmount.HasValue)
                    roundingAmount = sale.RoundingAmount.Value;
                // درآمد جمع اوری و خدمات مبدأ
                long pickup = 0;
                if (sale.CollectionOrSeparationFee.HasValue)
                    pickup += sale.CollectionOrSeparationFee.Value;
                if (sale.OtherOriginFees.HasValue)
                    pickup += sale.OtherOriginFees.Value;
                // درآمد بسته بندی
                long pakagingPrice = 0;
                if (sale.PackagingFee.HasValue)
                    pakagingPrice += sale.PackagingFee.Value;
                // ارزش افزوده
                long vat = 0;
                if (sale.VAT.HasValue)
                    vat += sale.VAT.Value;
                // توزیع و خدمات مقصد
                long tozi = 0;
                if (sale.DistributionOrSeparationFee.HasValue)
                    tozi += sale.DistributionOrSeparationFee.Value;
                if (sale.DestinationMiscellaneousFee.HasValue)
                    tozi += sale.DestinationMiscellaneousFee.Value;

                long total = hamleBar + pickup + pakagingPrice + vat + tozi;

                long totalInReport = sale.TotalBillOfLadingAmount ?? 0;

                if (totalInReport <= 0)
                {
                    await SetErrorAsync(id, "مبلغ بارنامه اشتباه است");
                    continue;
                }


                long transit = 0;
                if (sale.TransitSeparationFee.HasValue)
                    transit += sale.TransitSeparationFee.Value;
                if (sale.TransitMiscellaneousFee.HasValue)
                    transit += sale.TransitMiscellaneousFee.Value;
                if (sale.TransitCargoFare.HasValue)
                    transit += sale.TransitCargoFare.Value;
                if (transit > 0)
                {
                    await SetErrorAsync(id, "در فیلد ترانزیت نباید مبلغی درج شود");
                    continue;
                }

                //آماده سازی اطلاعات برای ایجاد آرتیکل های سند

                if (sale.BillOfLadingGroupCode == 100 && tozi <= 0)
                {
                    await SetErrorAsync(id, "هزینه های مقصد در بارنامه اشتباه است");
                    continue;
                }

                int rownumber = 1;
                if (sellerId != 120)
                {
                    if (sale.BillOfLadingGroupCode == 1 || sale.BillOfLadingGroupCode == 112)
                    {
                        // درآمد حاصل از حمل بار داخلی
                        ImportSaleDocDto incom = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                            Bed = 0,
                            Bes = hamleBar,
                            KolCode = "605",
                            MoeinCod = "6050001",
                            Tafsil4Name = sale.BillOfLadingGroup,
                            Tafsil5Name = sale.AgencyName,
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(incom);
                        rownumber++;
                        //
                        //--  درآمد حااصل از جمع آوری و تفکیک بار در مبدأ
                        if (pickup > 0)
                        {
                            ImportSaleDocDto artPickup = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت جمع آوری و هزینه های مبدأ   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = pickup,
                                KolCode = "605",
                                MoeinCod = "6050002",
                                Tafsil4Name = sale.BillOfLadingGroup,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(artPickup);
                            rownumber++;
                        }

                        //درآمد حاصل از بسته بندی بار
                        if (pakagingPrice > 0)
                        {
                            ImportSaleDocDto artPakagingCost = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت هزینه بسته بندی  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = pakagingPrice,
                                KolCode = "605",
                                MoeinCod = "6050004",
                                Tafsil4Name = sale.BillOfLadingGroup,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(artPakagingCost);
                            rownumber++;
                        }

                        // هزینه توزیع و خدمات مقصد
                        if (tozi > 0)
                        {
                            // برای مقاصدی که کیهان پست نماینده توزیع است بخش توزیع بعنوان درآمد ثبت خواهد شد
                            if (sale.DestinationRepresentative == "کیهان پست تهران" || sale.DestinationRepresentative == "شعبه آزادگان اهواز" || sale.DestinationRepresentative == "شعبه فرودگاه اهواز" || sale.DestinationRepresentative == "شعبه آزادگان" || sale.DestinationRepresentative == "پیام کیهان پارس")
                            {
                                ImportSaleDocDto artPakagingCost = new ImportSaleDocDto
                                {
                                    RowNumber = rownumber,
                                    DocDate = sale.MiladiDate.Value,
                                    Description = $"درآمد توزیع بار و خدمات مقصد  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                    Bed = 0,
                                    Bes = tozi,
                                    KolCode = "605",
                                    MoeinCod = "6050003",
                                    Tafsil4Name = sale.BillOfLadingGroup,
                                    Tafsil5Name = sale.AgencyName,
                                    BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                    BillId = sale.Id,
                                };
                                lst.Add(artPakagingCost);
                                rownumber++;
                            }
                            else     // هزینه توزیع و خدمات مقصد به حساب نماینده توزیغ منظور می شود
                            {
                                ImportSaleDocDto artToziNamayande = new ImportSaleDocDto
                                {
                                    RowNumber = rownumber,
                                    DocDate = sale.MiladiDate.Value,
                                    Description = $"بابت  ثبت بستانکاری سهم نماینده توزیع  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                    Bed = 0,
                                    Bes = tozi,
                                    KolCode = "501",
                                    MoeinCod = "5010001",
                                    Tafsil4Name = sale.DestinationRepresentative,
                                    Tafsil5Name = sale.CourierName,
                                    BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                    BillId = sale.Id,
                                };
                                lst.Add(artToziNamayande);
                                rownumber++;
                                //
                            }

                        }
                        // روند فاکتور
                        if (roundingAmount != 0)
                        {
                            ImportSaleDocDto incomMisi = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت روند فاکتور    {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = roundingAmount < 0 ? Math.Abs(roundingAmount) : 0,
                                Bes = roundingAmount > 0 ? Math.Abs(roundingAmount) : 0,
                                KolCode = "701",
                                MoeinCod = "7010006",
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomMisi);
                            rownumber++;
                        }
                        //ارزش افزوده
                        if (vat > 0)
                        {
                            ImportSaleDocDto artVat = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت ارزش افزوده فروش  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = vat,
                                KolCode = "503",
                                MoeinCod = "5030011",
                                Tafsil5Name = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : null,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            //if (sale.PaymentMethod == "اعتباری")
                            //    artVat.Tafsil4Name = sale.CreditCompany;

                            lst.Add(artVat);
                            rownumber++;
                        }

                        // تسویه حساب بارنامه
                        if (sale.PaymentMethod == "نقدی")
                        {
                            ImportSaleDocDto payment = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت تسویه حساب  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = totalInReport,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = sale.AgencyName,
                                Tafsil5Name = sale.DataEntryUserName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(payment);
                            rownumber++;
                        }
                        else
                        {
                            string tafsil4 = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : sale.DestinationRepresentative;

                            ImportSaleDocDto payment = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت بدهی اعتباری/پسکرایه {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = totalInReport,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = tafsil4,
                                Tafsil5Name = sale.CourierName,
                                Tafsil6Name = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : sale.SenderName,

                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(payment);
                            rownumber++;
                        }
                    }
                    else if (sale.BillOfLadingGroupCode == 100 && tozi > 0) // هزینه های مقصد نباید 0 باشد
                    {
                        ImportSaleDocDto income100 = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت  {sale.BillOfLadingGroup} به شماره {sale.NonSystemicBillOfLadingNumber}",
                            Bed = 0,
                            Bes = tozi,
                            KolCode = "605",
                            MoeinCod = "6050003",
                            Tafsil4Name = sale.BillOfLadingGroup,
                            Tafsil5Name = sale.AgencyName,
                            Tafsil6Name = sale.DestinationRepresentative,
                            BillOfLadingNumber = sale.NonSystemicBillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(income100);
                        rownumber++;

                        if (sale.PaymentMethod == "نقدی")
                        {
                            ImportSaleDocDto incomeBed100 = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت  {sale.BillOfLadingGroup} نقدی به شماره  {sale.NonSystemicBillOfLadingNumber}",
                                Bed = tozi,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = sale.DestinationRepresentative,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.NonSystemicBillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomeBed100);
                            rownumber++;
                        }
                        else
                        {
                            // حساب های دریافتنی 
                            // بابت دریافت مبلغ پسکرایه بارنامه های وارده غیر سیستمی
                            string tafsil4 = sale.AgencyName;
                            if (sale.DestinationRepresentative == "نامعلوم" || sale.DestinationRepresentative == "نا معلوم")
                                tafsil4 = "شعبه اهواز";

                            ImportSaleDocDto incomePasKarBed100 = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت خدمات مقصد {sale.BillOfLadingGroup} اعتباری / پسکرایه به شماره {sale.NonSystemicBillOfLadingNumber}",
                                Bed = totalInReport,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : tafsil4,
                                Tafsil5Name = sale.CourierName,
                                BillOfLadingNumber = sale.NonSystemicBillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomePasKarBed100);
                            rownumber++;

                            if (totalInReport - tozi > 0)
                            {
                                // حساب های پرداختنی
                                ImportSaleDocDto incomePasKar100 = new ImportSaleDocDto
                                {
                                    RowNumber = rownumber,
                                    DocDate = sale.MiladiDate.Value,
                                    Description = $"بابت  {sale.BillOfLadingGroup} پسکرایه/اعتباری به شماره {sale.BillOfLadingNumber}",
                                    Bed = 0,
                                    Bes = totalInReport - tozi,
                                    KolCode = "501",
                                    MoeinCod = "5010001",
                                    Tafsil4Name = sale.DestinationRepresentative,
                                    BillOfLadingNumber = sale.NonSystemicBillOfLadingNumber.ToString(),
                                    BillId = sale.Id,
                                };
                                lst.Add(incomePasKar100);
                                rownumber++;
                            }
                        }
                    }
                    else if (sale.BillOfLadingGroupCode == 2) // بارنامه داخلی - آسمان
                    {
                        //درآمد شرکت
                        long income = 0;
                        if (sale.CargoFare.HasValue)
                            income += (long)(sale.CargoFare.Value * 0.15);
                        //درآمد متفرقه
                        long incomemisi = 0;
                        if (sale.InsuranceFee.HasValue)
                            incomemisi += sale.InsuranceFee.Value;
                        if (sale.Discount.HasValue)
                            incomemisi -= sale.Discount.Value;
                        if (sale.OtherOriginFees.HasValue)
                            incomemisi += sale.OtherOriginFees.Value;
                        if (sale.TotalServiceFee.HasValue)
                            incomemisi += sale.TotalServiceFee.Value;
                        //روند فاکتور
                        long roundingAmountAirline = 0;
                        if (sale.RoundingAmount.HasValue)
                            roundingAmountAirline = sale.RoundingAmount.Value;

                        if (income > 0)
                        {
                            // درآمد حاصل از حمل بار داخلی
                            ImportSaleDocDto incom = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت کمیسیون   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = income,
                                KolCode = "605",
                                MoeinCod = "6050001",
                                Tafsil4Name = sale.BillOfLadingGroup,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incom);
                            rownumber++;

                            // درآمد حاصل از حمل بار داخلی
                            ImportSaleDocDto incomMisi = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت درآمد متفرقه   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = incomemisi,
                                KolCode = "605",
                                MoeinCod = "6050001",
                                Tafsil4Name = sale.BillOfLadingGroup,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomMisi);
                            rownumber++;
                        }
                        //روند فاکتور 
                        if (roundingAmountAirline != 0)
                        {
                            ImportSaleDocDto incomMisi = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت روند فاکتور    {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = roundingAmountAirline < 0 ? Math.Abs(roundingAmountAirline) : 0,
                                Bes = roundingAmountAirline > 0 ? Math.Abs(roundingAmountAirline) : 0,
                                KolCode = "701",
                                MoeinCod = "7010006",
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomMisi);
                            rownumber++;
                        }

                        //بابت 10% ارزش افزوده سهم شرکت از فروش بار داخلی آسمان
                        long LocalVat = 0;
                        if (vat > 0)
                        {
                            LocalVat = (long)(vat * 0.15);

                            //بابت 10% ارزش افزوده سهم شرکت از فروش بار داخلی آسمان
                            ImportSaleDocDto artVat = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت ارزش افزوده فروش  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = LocalVat,
                                KolCode = "503",
                                MoeinCod = "5030011",
                                Tafsil5Name = sale.BillOfLadingGroup,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(artVat);
                            rownumber++;
                        }

                        //مطالبات ایرلاین
                        long AsemanBes = 0;
                        if (sale.CargoFare.HasValue)
                            AsemanBes += (long)(sale.CargoFare.Value * 0.85);
                        if (sale.StampFee.HasValue)
                            AsemanBes += sale.StampFee.Value;
                        if (vat > 0)
                            AsemanBes += (long)(vat * 0.85);

                        ImportSaleDocDto artToziNamayande = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                            Bed = 0,
                            Bes = AsemanBes,
                            KolCode = "501",
                            MoeinCod = "5010001",
                            Tafsil4Name = "هواپیمایی آسمان",
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(artToziNamayande);
                        rownumber++;

                        // تسویه حساب بارنامه
                        if (sale.PaymentMethod == "نقدی")
                        {
                            ImportSaleDocDto payment = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت تسویه حساب  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = totalInReport,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = sale.AgencyName,
                                Tafsil5Name = sale.DataEntryUserName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(payment);
                            rownumber++;
                        }
                        else
                        {
                            string tafsil4 = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : sale.AgencyName;

                            ImportSaleDocDto payment = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت بدهی اعتباری/پسکرایه {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = totalInReport,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = tafsil4,
                                Tafsil5Name = sale.DataEntryUserName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(payment);
                            rownumber++;
                        }

                    }
                    else if (sale.BillOfLadingGroupCode == 3) // بارنامه داخلی - ایران ایر
                    {
                        //درآمد شرکت
                        long income = 0;
                        if (sale.CargoFare.HasValue)
                            income += (long)(sale.CargoFare.Value * 0.1);

                        long incomemisi = 0;
                        long roundingAmountAirline = 0;
                        if (sale.RoundingAmount.HasValue)
                            roundingAmountAirline = sale.RoundingAmount.Value;

                        if (sale.InsuranceFee.HasValue)
                            incomemisi += sale.InsuranceFee.Value;
                        if (sale.Discount.HasValue)
                            incomemisi -= sale.Discount.Value;
                        if (sale.OtherOriginFees.HasValue)
                            incomemisi += sale.OtherOriginFees.Value;
                        if (sale.TotalServiceFee.HasValue)
                            incomemisi += sale.TotalServiceFee.Value;

                        if (income > 0)
                        {
                            // درآمد حاصل از حمل بار داخلی
                            ImportSaleDocDto incom = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت کمیسیون   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = income,
                                KolCode = "605",
                                MoeinCod = "6050001",
                                Tafsil4Name = sale.BillOfLadingGroup,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incom);
                            rownumber++;

                            // درآمد حاصل از حمل بار داخلی
                            ImportSaleDocDto incomMisi = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت درآمد متفرقه   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = incomemisi,
                                KolCode = "605",
                                MoeinCod = "6050001",
                                Tafsil4Name = sale.BillOfLadingGroup,
                                Tafsil5Name = sale.AgencyName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomMisi);
                            rownumber++;
                        }

                        //روند فاکتور 
                        if (roundingAmountAirline != 0)
                        {
                            ImportSaleDocDto incomMisi = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت روند فاکتور    {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = roundingAmountAirline < 0 ? Math.Abs(roundingAmountAirline) : 0,
                                Bes = roundingAmountAirline > 0 ? Math.Abs(roundingAmountAirline) : 0,
                                KolCode = "701",
                                MoeinCod = "7010006",
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(incomMisi);
                            rownumber++;
                        }

                        long LocalVat = 0;
                        if (vat > 0)
                        {
                            LocalVat = (long)((income + incomemisi) * 0.1);

                            //بابت 10% ارزش افزوده سهم شرکت از فروش بار داخلی ایران ایر
                            ImportSaleDocDto artVat = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت ارزش افزوده فروش  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = 0,
                                Bes = LocalVat,
                                KolCode = "503",
                                MoeinCod = "5030011",
                                Tafsil5Name = sale.BillOfLadingGroup,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(artVat);
                            rownumber++;
                        }

                        //مطالبات ایرلاین
                        long AsemanBes = 0;
                        if (sale.CargoFare.HasValue)
                            AsemanBes += (long)(sale.CargoFare.Value * 0.9);
                        if (sale.StampFee.HasValue)
                            AsemanBes += sale.StampFee.Value;

                        long airlineVat = (long)(AsemanBes * 0.1);
                        AsemanBes += airlineVat;

                        ImportSaleDocDto artToziNamayande = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                            Bed = 0,
                            Bes = AsemanBes,
                            KolCode = "501",
                            MoeinCod = "5010001",
                            Tafsil4Name = "هواپیمایی ایران ایر",
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(artToziNamayande);
                        rownumber++;

                        // تسویه حساب بارنامه
                        if (sale.PaymentMethod == "نقدی")
                        {
                            ImportSaleDocDto payment = new ImportSaleDocDto
                            {
                                RowNumber = rownumber,
                                DocDate = sale.MiladiDate.Value,
                                Description = $"بابت تسویه حساب  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                Bed = totalInReport,
                                Bes = 0,
                                KolCode = "206",
                                MoeinCod = "2060001",
                                Tafsil4Name = sale.AgencyName,
                                Tafsil5Name = sale.DataEntryUserName,
                                BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                BillId = sale.Id,
                            };
                            lst.Add(payment);
                            rownumber++;
                        }
                        else
                        {
                            string tafsil4 = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : sale.AgencyName;
                            if (sale.CreditCompany == "بار مجموع")
                            {

                                ImportSaleDocDto payment = new ImportSaleDocDto
                                {
                                    RowNumber = rownumber,
                                    DocDate = sale.MiladiDate.Value,
                                    Description = $"بابت هزینه رهسپاری  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                    Bed = totalInReport,
                                    Bes = 0,
                                    KolCode = "702",
                                    MoeinCod = "7020004",
                                    BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                    BillId = sale.Id,
                                };
                                lst.Add(payment);
                                rownumber++;
                            }
                            else
                            {
                                ImportSaleDocDto payment = new ImportSaleDocDto
                                {
                                    RowNumber = rownumber,
                                    DocDate = sale.MiladiDate.Value,
                                    Description = $"بابت بدهی اعتباری/پسکرایه {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                                    Bed = totalInReport,
                                    Bes = 0,
                                    KolCode = "206",
                                    MoeinCod = "2060001",
                                    Tafsil4Name = tafsil4,
                                    Tafsil5Name = sale.DataEntryUserName,
                                    BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                                    BillId = sale.Id,
                                };
                                lst.Add(payment);
                                rownumber++;
                            }

                        }
                    }
                }
                else
                {
                    float weight = 0;
                    if (sale.ActualCargoWeight.HasValue)
                        weight = sale.ActualCargoWeight.Value;
                    long DestinationPrice = (long)(weight * 5000);
                    long AgancyPrice = (long)(weight * 5000);

                    long keyhanRah = (hamleBar + tozi + pickup) - (DestinationPrice + AgancyPrice);

                    // درآمد حاصل از حمل بار داخلی
                    ImportSaleDocDto incom = new ImportSaleDocDto
                    {
                        RowNumber = rownumber,
                        DocDate = sale.MiladiDate.Value,
                        Description = $"بابت  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                        Bed = 0,
                        Bes = keyhanRah,
                        KolCode = "605",
                        MoeinCod = "6050001",
                        Tafsil4Name = sale.AgencyName,
                        Tafsil5Name = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : sale.SenderName,
                        BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                        BillId = sale.Id,
                    };
                    lst.Add(incom);
                    rownumber++;

                    //ارزش افزوده
                    if (vat > 0)
                    {
                        ImportSaleDocDto artVat = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت ارزش افزوده فروش  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                            Bed = 0,
                            Bes = vat,
                            KolCode = "503",
                            MoeinCod = "5030011",
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        //if (sale.PaymentMethod == "اعتباری")
                        //    artVat.Tafsil4Name = sale.CreditCompany;

                        lst.Add(artVat);
                        rownumber++;
                    }
                    // روند فاکتور
                    if (roundingAmount != 0)
                    {
                        ImportSaleDocDto incomMisi = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت روند فاکتور    {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                            Bed = roundingAmount < 0 ? Math.Abs(roundingAmount) : 0,
                            Bes = roundingAmount > 0 ? Math.Abs(roundingAmount) : 0,
                            KolCode = "701",
                            MoeinCod = "7010006",
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(incomMisi);
                        rownumber++;
                    }

                    // سهم نماینده بر مبنای کیلویی 500 تومان
                    //  از ابتدای شهریور سهم نماینده بر مبنای کیلویی 500 تومان
                    ImportSaleDocDto DestinationCost = new ImportSaleDocDto
                    {
                        RowNumber = rownumber,
                        DocDate = sale.MiladiDate.Value,
                        Description = $"بابت هزینه های توزیع   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                        Bed = 0,
                        Bes = DestinationPrice,
                        KolCode = "501",
                        MoeinCod = "5010001",
                        Tafsil4Name = sale.DestinationRepresentative,
                        Tafsil5Name = "خدمات مقصد (بار ورودی)",
                        Tafsil6Name = sale.AgencyName,
                        BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                        BillId = sale.Id,
                    };
                    lst.Add(DestinationCost);

                    rownumber++;
                    // سهم شعبه صادر کننده مبنای کیلویی 500 تومان
                    ImportSaleDocDto soratResan = new ImportSaleDocDto
                    {
                        RowNumber = rownumber,
                        DocDate = sale.MiladiDate.Value,
                        Description = $"سهم صادرکننده بابت   {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber}",
                        Bed = 0,
                        Bes = AgancyPrice,
                        KolCode = "501",
                        MoeinCod = "5010001",
                        Tafsil4Name = sale.AgencyName == "شعبه تجزیه مبادلات اهواز" ? "پیام کیهان پارس" : "سرعت رسان سماء",
                        Tafsil5Name = "خدمات مبدأ (بار خروجی)",
                        Tafsil6Name = sale.AgencyName,
                        BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                        BillId = sale.Id,
                    };
                    lst.Add(soratResan);
                    rownumber++;


                    //...................
                    // تسویه حساب بارنامه
                    if (sale.PaymentMethod == "نقدی")
                    {
                        ImportSaleDocDto payment = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت تسویه حساب  {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber} - {sale.PaymentMethod}",
                            Bed = totalInReport,
                            Bes = 0,
                            KolCode = "206",
                            MoeinCod = "2060001",
                            Tafsil4Name = sale.AgencyName == "شعبه تجزیه مبادلات اهواز" ? "پیام کیهان پارس" : "سرعت رسان سماء",
                            Tafsil5Name = sale.DataEntryUserName,
                            Tafsil6Name = sale.SenderName,
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(payment);
                        rownumber++;
                    }
                    else
                    {
                        string tafsil4 = sale.PaymentMethod == "اعتباری" ? sale.CreditCompany : sale.DestinationRepresentative;

                        ImportSaleDocDto payment = new ImportSaleDocDto
                        {
                            RowNumber = rownumber,
                            DocDate = sale.MiladiDate.Value,
                            Description = $"بابت بدهی اعتباری/پسکرایه {sale.BillOfLadingGroup} به شماره {sale.BillOfLadingNumber} - {sale.PaymentMethod}",
                            Bed = totalInReport,
                            Bes = 0,
                            KolCode = "206",
                            MoeinCod = "2060001",
                            Tafsil4Name = tafsil4,
                            Tafsil5Name = sale.DataEntryUserName,
                            BillOfLadingNumber = sale.BillOfLadingNumber.ToString(),
                            BillId = sale.Id,
                        };
                        lst.Add(payment);
                        rownumber++;
                    }
                }
            }
            return lst;
        }
        public async Task<clsResult> DeleteBillOfLandingsAsync(long[] ids)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;
            List<KPDataTransferService> lst = new List<KPDataTransferService>();

            var items = await _db.KPOldSystemSales.Where(n => ids.Contains(n.Id)).ToListAsync();
            foreach (var item in items)
            {
                if (item.DocId != null)
                {
                    result.Message = "برخی از بارنامه ها دارای سند حسابداری هستند. ابتدا سند موردنظر را حذف کنید";
                    return result;
                }
            }
            //_db.KPOldSystemSales.RemoveRange(items);
            try
            {
                await _db.BulkDeleteAsync(items);
                result.Success = true;
                result.Message = "حذف اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Message = "خطایی در حذف اطلاعات پیش آمده. \n" + x.Message;
            }
            return result;
        }
        public async Task<VmBillOfLandingMonitor> BillsMonitorAsync(long sellerId)
        {
            VmBillOfLandingMonitor monitor = new VmBillOfLandingMonitor();

            monitor.LastUpload = await _db.KPOldSystemSales.Where(n => n.SellerId == sellerId)
               .MaxAsync(n => n.MiladiDate);

            //monitor.sumOfNoDoc = data.Where(n => n.DocId == null).Count();
            //monitor.amountOfNoDoc = (long)data.Where(n => n.DocId == null)?.Sum(n => n.TotalBillOfLadingAmount);
            //monitor.sumOfBills = data.Count;
            //monitor.amountOfBills = (long)data.Sum(n => n.TotalBillOfLadingAmount);
            //monitor.sumOfNoFinal = data.Where(n => n.RecordLock == false && n.BillOfLadingGroupCode != 100).Count();
            //monitor.amountOfNoFinal = (long)data.Where(n => n.RecordLock == false && n.BillOfLadingGroupCode != 100)?.Sum(n => n.TotalBillOfLadingAmount);

            return monitor;

        }
        public async Task<clsResult> SetErrorAsync(long id, string error)
        {
            var result = new clsResult();
            var sale = await _db.KPOldSystemSales.FindAsync(id);
            if (sale == null || string.IsNullOrEmpty(error)) return result;

            sale.ErrorMessage = error;
            _db.KPOldSystemSales.Update(sale);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = ",عملیات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Message = "خطایی در حف اطلاعات پیش آمده. \n" + x.Message;
            }
            return result;
        }
        public async Task<List<KPOldSystemSaleReport>> GetSalesErrorsAsync(SaleFilterDto filter)
        {
            var query = _db.KPOldSystemSales
                .Where(n => n.SellerId == filter.SellerId && !string.IsNullOrEmpty(n.ErrorMessage))
                .AsQueryable();

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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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
        //---
        public async Task<List<KPOldSystemSaleReport>> GetIncomingBillsOfLadingAsync(SaleFilterDto filter)
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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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

            if (!string.IsNullOrEmpty(filter.OldDistRepName))
                query = query.Where(n => n.DestinationRepresentative == filter.OldDistRepName);

            //if (!string.IsNullOrEmpty(filter.OldBranchName))
            //    query = query.Where(n => n.AgencyName == filter.OldBranchName);


            return await query.ToListAsync();
        }
        public IQueryable<KPOldSystemSaleReport> GetIncomingBillsOfLadings(SaleFilterDto filter)
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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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

            if (!string.IsNullOrEmpty(filter.OldDistRepName))
                query = query.Where(n => n.DestinationRepresentative == filter.OldDistRepName);

            return query.AsQueryable();
        }
        public async Task<List<KPOldSystemSaleReport>> GetOutgoingBillsOfLadingAsync(SaleFilterDto filter)
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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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

            if (!string.IsNullOrEmpty(filter.OldBranchName))
                query = query.Where(n => n.AgencyName == filter.OldBranchName);

            return await query.ToListAsync();
        }
        public IQueryable<KPOldSystemSaleReport> GetOutgoingBillsOfLadings(SaleFilterDto filter)
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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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

            if (!string.IsNullOrEmpty(filter.OldBranchName))
                query = query.Where(n => n.AgencyName == filter.OldBranchName);

            return query.AsQueryable();
        }

        public async Task<List<ExportMoadianDto>> GetBillofladingByListIdAsync(List<long> Ids)
        {
            var BillOfLadings = await _db.KPOldSystemSales.Where(n => Ids.Contains(n.Id)).ToListAsync();
            List<ExportMoadianDto> lst = new List<ExportMoadianDto>();
            foreach (var x in BillOfLadings)
            {
                string buyer;
                if (x.PaymentMethod == "اعتباری" && !string.IsNullOrEmpty(x.CreditCompany))
                    buyer = x.CreditCompany;
                else
                    continue;

                string goodName;
                string goodCode;

                if (x.VAT > 0)
                {
                    goodName = "خدمات حمل و نقل هوایی کالا ها / سازمان امور مالیاتی";
                    goodCode = "2330001421435";
                }
                else
                {
                    goodName = "خدمات حمل و نقل جاده ای کالاها/ سازمان امور مالیاتی";
                    goodCode = "2330001421466";
                }

                ExportMoadianDto n = new ExportMoadianDto();
                n.AccountingSystemInvoiceCode = x.BillOfLadingNumber?.ToString();
                n.InvoiceNumber = n.AccountingSystemInvoiceCode.Substring(3);
                n.InvoiceDate = $"{x.ShamsiDate.Substring(0, 4)}/{x.ShamsiDate.Substring(4, 2)}/{x.ShamsiDate.Substring(6, 2)}";
                n.InvoiceType = 1;
                n.PersonType = "";
                n.BuyerFullName = buyer;
                n.NationalID = x.SenderNationalCode;
                n.NewEconomicCode = "";
                n.PostalCode = "";
                n.Address = x.SenderAddress;
                n.GoodsOrServices = "1";
                n.GoodsOrServices13DigitID = goodCode;
                n.GoodsOrServicesDescription = goodName;
                n.GoodsOrServicesUnitCode = "1627";
                n.UnitPrice = x.TotalBillOfLadingAmount.Value - (x.VAT ?? 0);
                n.Quantity = 1;
                n.Discount = 0;
                n.VATRate = x.VAT > 0 ? 10 : 0;
                n.VATAmount = x.VAT > 0 ? (n.UnitPrice * n.VATRate / 100) : 0;
                n.SettlementType = 1;

                lst.Add(n);
            }
            return lst;
        }

        //
        public async Task<Vm_BranchDashboard> BranchDashboardAsync(SaleFilterDto filter)
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

            if (!string.IsNullOrEmpty(filter.BillOfLadingType))
            {
                query = query.Where(r => r.BillOfLadingGroup == filter.BillOfLadingType);
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

            if (!string.IsNullOrEmpty(filter.OldBranchName))
                query = query.Where(n => n.AgencyName == filter.OldBranchName);

            var model = new Vm_BranchDashboard();
            //جدید
            model.NewBillsCount = await query.Where(n => n.Status == 1).CountAsync();
            //در انتظار تأیید پیک
            model.PendingCourierApprovalCount = await query.Where(n => n.Status == 2).CountAsync();
            //درحال توزیع
            model.InDistributionCount = await query.Where(n => n.Status == 3).CountAsync();
            // توزیع شده
            model.DistributedTodayCount = await query.Where(n => n.Status == 4).CountAsync();
            //تعداد بارنامه های توزیع نشده از روزهای قبل
            Int16[] statusRange = new Int16[] { 1, 2, 3, 8 };
            model.UndistributedPreviousBillsCount = await query.Where(n => statusRange.Contains(n.Status) && n.MiladiDate < filter.StartDate.Value).CountAsync();


            return model;
        }
    }
}
