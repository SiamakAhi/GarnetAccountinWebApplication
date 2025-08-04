using Microsoft.CodeAnalysis.Elfie.Extensions;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_MoadianReport
    {
        [Key]
        public long Id { get; set; }
        public long SellerId { get; set; }
        public bool IsSaleInvoice { get; set; }
        public short ReportType { get; set; }
        [Display(Name = "نوع صورت‌حساب")] public string InvoiceType { get; set; }
        [Display(Name = "الگو صورت‌حساب")] public string InvoicePattern { get; set; }
        [Display(Name = "موضوع صورت‌حساب")] public string InvoiceSubject { get; set; }
        [Display(Name = "شماره مالیاتی صورت‌حساب")] public string TaxNumber { get; set; }
        [Display(Name = "مجموع صورت‌حساب")] public long TotalInvoiceAmount { get; set; }
        [Display(Name = "مالیات بر ارزش افزوده")] public long VAT { get; set; } = 0;
        [Display(Name = "وضعیت صورت‌حساب")] public string InvoiceStatus { get; set; }
        [Display(Name = "تاریخ صدور صورت‌حساب")] public DateTime IssueDate { get; set; }
        [Display(Name = "تاریخ درج در کارپوشه")] public DateTime FolderInsertDate { get; set; }
        [Display(Name = "شناسه هویتی خریدار/ حق‌العمل‌کار")] public string? BuyerIdentity { get; set; }
        [Display(Name = "شماره اقتصادی خریدار/حق‌العمل‌کار")] public string? BuyerEconomicNumber { get; set; }
        [Display(Name = "شعبه فروشنده")] public string? SellerBranch { get; set; }
        [Display(Name = "نام خریدار/حق‌العمل‌کار")] public string? BuyerName { get; set; }
        [Display(Name = "نام تجاری خریدار/حق‌العمل‌کار")] public string? BuyerTradeName { get; set; }
        [Display(Name = "نوع شخص خریدار/حق‌العمل‌کار")] public string? BuyerPersonType { get; set; }
        [Display(Name = "شماره قرارداد حق‌العمل‌کاری فروشنده")] public string? SellerContractNumber { get; set; }
        [Display(Name = "شماره اشتراک /شناسه قبض")] public string? SubscriptionNumber { get; set; }
        [Display(Name = "نوع پرواز")] public string? FlightType { get; set; }
        [Display(Name = "شماره قرارداد پیمانکاری")] public string ContractorContractNumber { get; set; }
        [Display(Name = "روش تسویه")] public string SettlementMethod { get; set; }
        [Display(Name = "سال و دوره")] public string YearAndPeriod { get; set; }
        [Display(Name = "وضعیت حد مجاز")] public string? LimitStatus { get; set; }
        [Display(Name = "وضعیت احتساب")] public string? AccountingStatus { get; set; }
        [Display(Name = "مبلغ فاکتور بدون ارزش افزوده (ریال)")] public long InvoiceAmountWithoutVAT { get; set; }
        [Display(Name = "تاریخ صدور صورت‌حساب ارجاعی ابطال کننده")] public DateTime? ReferringInvoiceIssueDate { get; set; }
        [Display(Name = "تاریخ تنظیم وضعیت عدم احتساب")] public DateTime? NonAccountingStatusDate { get; set; }
        [Display(Name = "شماره مالیاتی صورت‌حساب مرجع")] public string? ReferenceInvoiceTaxNumber { get; set; }
        [Display(Name = "مانده تسویه صورت‌حساب")] public long InvoiceSettlementBalance { get; set; } = 0;

        [Display(Name = "یادداشت کاربر")]
        public string? UserComment { get; set; }
        public short InvoiceStatusCode { get; set; }
        public short InvoiceSubjectCode { get; set; }
        public bool HasAccountingDoc { get; set; } = false;
        public bool IsChecked { get; set; } = false;
        public long? TafsilId { get; set; }
        public long Batchnuber { get; set; } = DateTime.Now.ToLong();
        public string CreateAt { get; set; } = DateTime.Now.LatinToPersian();
    }
}
