using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AutoServiceAgency.Models.Enities
{
    public class asa_Setting
    {
        public short Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "معین فروش قطعه")]
        public int? SaleMoeinId { get; set; }

        [Display(Name = "تفصیل فروش قطعه آزاد")]
        public long? SaleFreeTafsilId { get; set; }

        [Display(Name = "تفصیل فروش قطعه گارانتی")]
        public long? SaleWarrantyTfsilId { get; set; }

        [Display(Name = "معین بستانکاران غیرتجاری")]
        public int? NonCommercialCreditorMoeinId { get; set; }

        [Display(Name = "معین بدهکاران تجاری")]
        public int? CommercialDebtorMoeinId { get; set; }

        [Display(Name = "معین بانک")]
        public int? BankMoeinId { get; set; }

        [Display(Name = "تفصیل حساب یانکی پیش فرض")]
        public long? BankTafsilId { get; set; }


        [Display(Name = "تفصیل بدهکاران تجاری برای پذیرش های آزاد")]
        public long? DebtorTafsil4Free { get; set; }

        [Display(Name = "تفصیل بدهکاران تجاری برای پذیرش های گارانتی")]
        public long? DebtorTafsil4Warranty { get; set; }


        [Display(Name = "معین مالیات بر ارزش افزوده فروش")]
        public int? SaleVatMoeinId { get; set; }

        public long? FreeTafsiltId { get; set; }  // حساب تفصیل آزاد
        public long? WarrantyTafsilId { get; set; }  // حساب تفصیل گارانتی

    }
}
