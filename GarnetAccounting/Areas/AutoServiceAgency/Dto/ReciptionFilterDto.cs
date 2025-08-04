namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class ReciptionFilterDto
    {
        public long SellerId { get; set; }
        public string? ReceptionNumber { get; set; }
        public long? TechnicianId { get; set; }

        //آزاد و گارانتی
        public short? LocationCode { get; set; }

        // فروش قطعه / اجرت
        public short? TypeCode { get; set; }
        public string? CustomerFullName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string strFromDate { get; set; }
        public string strToDate { get; set; }

        public bool JustNonDoc { get; set; } = true;
    }
}
