namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class CoppyInvoiceSettingDto
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public List<Guid> InvoicesId { get; set; } = new List<Guid>();

        // 1- مشمول و غیرمشمول
        // 2- مشمول
        // 3- غیرمشمول
        public short TaxableType { get; set; } = 1;

        // 1- کپی به تاریخ اصلی فاکتور
        // 2- کپی برای ماه بعد
        // 3- کپی برای ماه قبل
        // 4- محاسبه تاریغ بر اساس روز
        // 5- صدور فاکتور در تاریخ مشخص
        public short GenerateDataType { get; set; } = 1;
        public string? strInvoiceDate { get; set; }
        // محاسبه تاریع بر اساس روز
        public int VarDay { get; set; } = 0;

    }
}
