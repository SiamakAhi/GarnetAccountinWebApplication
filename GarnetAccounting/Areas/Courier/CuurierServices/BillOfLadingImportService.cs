using ClosedXML.Excel;
using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class BillOfLadingImportService : IBillOfLadingImportService
    {
        private readonly AppDbContext _db;

        public BillOfLadingImportService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<KPOldSystemSaleReport>> ImportFromExcelAsync(IFormFile ExcelFile, long sellerId)
        {
            var reports = new List<KPOldSystemSaleReport>();
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

                                    SenderPersonCode = worksheet.Cell(row, 37).GetValue<string?>(),
                                    SenderName = worksheet.Cell(row, 37).GetValue<string>(),
                                    SenderAddress = worksheet.Cell(row, 38).GetValue<string>(),
                                    SenderPhone = worksheet.Cell(row, 39).GetValue<string>(),
                                    SenderNationalCode = worksheet.Cell(row, 40).GetValue<string>(),
                                    SenderZoneCode = worksheet.Cell(row, 48).GetValue<string>(),
                                    SenderZoneAddress = worksheet.Cell(row, 49).GetValue<string>(),
                                    SenderZoneName = worksheet.Cell(row, 50).GetValue<string>(),

                                    RecipientPersonCode = worksheet.Cell(row, 41).GetValue<string?>(),
                                    RecipientName = worksheet.Cell(row, 42).GetValue<string>(),
                                    RecipientCustomerCode = GetNullableLong(worksheet.Cell(row, 41)),
                                    RecipientAddress = worksheet.Cell(row, 43).GetValue<string>(),
                                    RecipientPhone = worksheet.Cell(row, 44).GetValue<string>(),
                                    RecipientCode = worksheet.Cell(row, 45).GetValue<string>(),
                                    RecipientZoneAddress = worksheet.Cell(row, 46).GetValue<string>(),
                                    RecipientZoneName = worksheet.Cell(row, 47).GetValue<string>(),

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
                                return null;
                            }
                        }
                        return reports;
                    }
                }
            }
            catch
            {
                return null;
            }

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

        //...............................
        public async Task<Guid?> GetBranchIdAsync(string OldBranchName, long sellerId)
        {
            var branch = await _db.Cu_Branch
                .Where(n => n.SellerId == sellerId && (n.OldBranchName == OldBranchName || n.OldDistRepName == OldBranchName))
                .FirstOrDefaultAsync();
            if (branch == null) return null;

            return branch.Id;
        }
        public async Task<int?> GetServiceIdAsync(string ServiceCode, long sellerId)
        {

            var service = await _db.Cu_Services
                .Where(n => n.SellerId == sellerId && n.ServiceCode == ServiceCode)
                .FirstOrDefaultAsync();
            if (service == null) return null;

            return service.Id;
        }
        public async Task<long?> GetPersonIdAsync(KPOldSystemSaleReport dto, long sellerId, bool isSender, int customerId)
        {
            if (isSender && !string.IsNullOrEmpty(dto.SenderPersonCode))
            {
                var person = await _db.parties
                  .FirstOrDefaultAsync(n => n.uyer_SellerId == sellerId && n.AccountingSystemId == dto.SenderPersonCode);
                if (person != null)
                    return person.Id;
            }
            else if (!isSender && !string.IsNullOrEmpty(dto.RecipientPersonCode))
            {
                var person = await _db.parties
                  .FirstOrDefaultAsync(n => n.uyer_SellerId == sellerId && n.AccountingSystemId == dto.RecipientPersonCode);
                if (person != null)
                    return person.Id;
            }
            var newperson = await AddPersonAsync(dto, sellerId, isSender, customerId);
            if (newperson != null) return newperson.Id;
            else return null;
        }
        private async Task<Party?> AddPersonAsync(KPOldSystemSaleReport dto, long sellerId, bool isSender, int customerId)
        {
            Party p = new Party();
            p.Name = isSender ? dto.SenderName : dto.RecipientName;
            p.Title = isSender ? dto.SenderName : dto.RecipientName;
            p.NationalId = isSender ? dto.SenderNationalCode : null;
            p.EconomicCode = isSender ? dto.SenderNationalCode : null;
            p.MobilePhone = isSender ? dto.SenderPhone : dto.RecipientPhone;

            p.LegalStatus = 1;
            p.Role = 2;
            p.AccountingSystemId = isSender ? dto.SenderAddress : dto.RecipientAddress;
            p.uyer_SellerId = sellerId;
            p.CustomerId = customerId;
            p.IsActive = true;
            p.IsCustomer = true;
            try
            {
                await _db.parties.AddAsync(p);
                await _db.SaveChangesAsync();
                return p;
            }
            catch { return null; }
        }

    }
}
