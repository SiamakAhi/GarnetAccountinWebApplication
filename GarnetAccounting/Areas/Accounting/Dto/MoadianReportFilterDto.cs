namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class MoadianReportFilterDto
    {
        public long SellerId { get; set; }

        public short Transactiontype { get; set; }
        public string? InvoiceType { get; set; }
        public short[]? InvoiceSubject { get; set; }
        public short[]? InvoiceStatuse { get; set; }
        public string? strFromDate { get; set; }
        public string? strToDate { get; set; }
        public string? Description { get; set; }
        public long? BatchNumber { get; set; }
        public bool? Ischecked { get; set; } = false;
        public bool ShowAll { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 25;



    }
}
