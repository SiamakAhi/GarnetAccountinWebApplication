
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class CuOld_SaleReportGrouped
    {
        public long SellerId { get; set; }
        public int? DocNumber { get; set; }

        [Display(Name = "گروه بارنامه")]
        public string BillOfLadingGroup { get; set; }

        [Display(Name = "تاریخ میلادی")]
        public DateTime? MiladiDate { get; set; }

        [Display(Name = "نام آژانس")]
        public string AgencyName { get; set; }

        [Display(Name = "مبلغ حمل بار")]
        public long? TotalCargoFare { get; set; }

        [Display(Name = "مبلغ تمبر")]
        public long? TotalStampFee { get; set; }

        [Display(Name = "هزینه جمع آوری یا تفکیک")]
        public long? TotalCollectionOrSeparationFee { get; set; }

        [Display(Name = "مبلغ بسته بندی")]
        public long? TotalPackagingFee { get; set; }

        [Display(Name = "مبلغ بیمه")]
        public long? TotalInsuranceFee { get; set; }

        [Display(Name = "مبلغ تخفیف")]
        public long? TotalDiscount { get; set; }

        [Display(Name = "مبلغ ارزش افزوده")]
        public long? TotalVAT { get; set; }

        [Display(Name = "سایر هزینه های مبدا")]
        public long? TotalOtherOriginFees { get; set; }

        [Display(Name = "مبلغ روند")]
        public long? TotalRoundingAmount { get; set; }

        [Display(Name = "هزینه متفرقه")]
        public long? TotalMiscellaneousFee { get; set; }

        [Display(Name = "مبلغ متفرقه مقصد")]
        public long? TotalDestinationMiscellaneousFee { get; set; }

        [Display(Name = "مبلغ پایه")]
        public long? TotalBaseFare { get; set; }

        [Display(Name = "ارزش افزوده")]
        public long? TotalAddedValue { get; set; }

        [Display(Name = "مبلغ کل خدمات")]
        public long? TotalTotalServiceFee { get; set; }

        [Display(Name = "مبلغ کل بارنامه")]
        public long? TotalBillOfLadingAmount { get; set; }

        [Display(Name = "هزینه توزیع یا تفکیک")]
        public long? DistributionOrSeparationFee { get; set; }

        [Display(Name = "از مبدا")]
        public string? FromOrigin { get; set; }

        [Display(Name = "به مقصد")]
        public string ToDestination { get; set; }

        [Display(Name = "نماینده مقصد")]
        public string DestinationRepresentative { get; set; }

        [Display(Name = "نحوه پرداخت")]
        public string PaymentMethod { get; set; }

        [Display(Name = "شرکت اعتباری")]
        public string? CreditCompany { get; set; }

        [Display(Name = "کد سرویس خدمات")]
        public long? ServiceCode { get; set; }

        [Display(Name = "کد گروه بارنامه")]
        public long? BillOfLadingGroupCode { get; set; }

        // بار ورودی
        public int InQty { get; set; }
        public long? InGoodsCount { get; set; }
        public float? InActualCargoWeight { get; set; }
        public long? InPasKarayeh { get; set; }
        public long? InCash { get; set; }
        public long? InCreditable { get; set; }
        public long? InRepresentativeShare { get; set; }

        // بار خروجی
        public int OutQty { get; set; }
        public long? OutGoodsCount { get; set; }
        public float? OutActualCargoWeight { get; set; }
        public long? OutPasKarayeh { get; set; }
        public long? OutCash { get; set; }
        public long? OutCreditable { get; set; }
        public long? OutRepresentativeShare { get; set; }

        [Display(Name = "مانده حساب")]
        public long? BalanceAmount => (OutRepresentativeShare ?? 0) + (InPasKarayeh ?? 0) - (OutPasKarayeh ?? 0) - (InRepresentativeShare ?? 0);

        [Display(Name = "وضعیت حساب")]
        public string? BalanceStatus
        {
            get
            {
                if (BalanceAmount > 0)
                    return "بستانکار";
                else if (BalanceAmount < 0)
                    return "بدهکار";
                else
                    return "تسویه";
            }
        }

    }

}
