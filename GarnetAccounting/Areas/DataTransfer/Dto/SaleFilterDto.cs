using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.DataTransfer.Dto
{
    public class SaleFilterDto
    {
        public long SellerId { get; set; }
        public DateTime? StartDate { get; set; }
        public string? strStartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? strEndDate { get; set; }
        public long? BillOfLadingNumber { get; set; }
        public string BillOfLadingType { get; set; }
        public int BillOfLadingGroup { get; set; }
        public string CreditCustomer { get; set; }
        public string PaymentMethod { get; set; }
        public bool? HasAccountingDoc { get; set; }
        public string? DestinationRepresentative { get; set; }
        public string? SettelmentType { get; set; }
        public string? Agency { get; set; }
        [Display(Name = "نام شعبه در سیستم قدیم")]
        public string? OldBranchName { get; set; }

        [Display(Name = "نام نمایندگی در سیستم قدیم")]
        public string? OldDistRepName { get; set; }

        public int currentPage { get; set; }
        public int pageSize { get; set; }

        public int currentPage2 { get; set; }
        public int pageSize2 { get; set; }
    }
}
