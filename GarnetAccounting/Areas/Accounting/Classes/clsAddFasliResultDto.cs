namespace GarnetAccounting.Areas.Accounting.Classes
{
    public class clsAddFasliResultDto
    {
        public long TafsilId { get; set; }
        public string TafsilCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
