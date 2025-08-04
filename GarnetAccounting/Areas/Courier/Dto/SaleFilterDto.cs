using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class SaleFilterDto
    {

        //-- public Property
        public long SellerId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public DateTime? StartDate { get; set; }
        public string? strStartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? strEndDate { get; set; }

        //---------------------------
        public long? BillOfLadingNumber { get; set; }

        [Display(Name = "گروه بارنامه")]
        public int? BillOfLadingType { get; set; }

        [Display(Name = "مشتری اعتباری")]
        public string? CreditCustomer { get; set; }

        [Display(Name = "روش پرداخت")]
        public string? PaymentMethod { get; set; }

        [Display(Name = "دارای سند حسابداری")]
        public bool? HasAccountingDoc { get; set; }

        [Display(Name = "نماینده مقصد")]
        public string? DestinationRepresentative { get; set; }

        [Display(Name = "شعبه صادرکننده")]
        public string? Agency { get; set; }

        [Display(Name = "محاسبه بارنامه های نهایی نشده (زرد)")]
        public bool ShowUnFinal { get; set; } = true;


    }
}
