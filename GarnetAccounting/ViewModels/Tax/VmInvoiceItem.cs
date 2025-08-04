using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.Tax
{

    public class VmInvoiceItem
    {
        [Display(Name = "شناسه آیتم")]
        public long ItemId { get; set; } // شناسه آیتم

        [Display(Name = "شناسه فاکتور")]
        public long InvoiceId { get; set; } // شناسه فاکتور

        [Display(Name = "شماره فاکتور")]
        public string InvoiceNo { get; set; } // تاریخ

        [Display(Name = "تاریخ")]
        public string Date { get; set; } // تاریخ

        [Display(Name = "کد طرف حساب")]
        public string AccountCode { get; set; } // کد طرف حساب

        [Display(Name = "نام طرف حساب")]
        public string AccountName { get; set; } // نام طرف حساب

        [Display(Name = "شناسه کالا")]
        public long ProductId { get; set; } // شناسه کالا
        [Display(Name = "نام کالا")]
        public string ProductName { get; set; } // شناسه کالا

        [Display(Name = "نوع طرف حساب")]
        public string AccountType { get; set; } // نوع طرف حساب

        [Display(Name = "فی")]
        public double UnitPrice { get; set; }

        [Display(Name = "تعداد")]
        public double Qty { get; set; }

        [Display(Name = "جمع قیمت")]
        public double TotalPrice { get; set; }



    }

}
