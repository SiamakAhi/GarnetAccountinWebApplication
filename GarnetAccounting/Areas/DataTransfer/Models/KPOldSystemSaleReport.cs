
using GarnetAccounting.Areas.Courier.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.DataTransfer.Models
{

    public class KPOldSystemSaleReport
    {
        [Key]
        public long Id { get; set; }
        public long SellerId { get; set; }
        public Guid? DocId { get; set; }
        public int? DocNumber { get; set; }

        [Display(Name = "کد ماشین")]
        public long? MachineCode { get; set; }

        [Display(Name = "گروه بارنامه")]
        public string BillOfLadingGroup { get; set; }

        [Display(Name = "نام حوزه")]
        public string RegionName { get; set; }

        [Display(Name = "شماره بارنامه")]
        public long? BillOfLadingNumber { get; set; }

        [Display(Name = "شماره بارنامه وارده- غیر سیستمی")]
        public long? NonSystemicBillOfLadingNumber { get; set; }

        [Display(Name = "تاریخ شمسی")]
        public string ShamsiDate { get; set; }

        [Display(Name = "تاریخ میلادی")]
        public DateTime? MiladiDate { get; set; }

        [Display(Name = "زمان")]
        public string Time { get; set; }

        [Display(Name = "نام آژانس")]
        public string AgencyName { get; set; }

        [Display(Name = "مبلغ حمل بار")]
        public long? CargoFare { get; set; }

        [Display(Name = "مبلغ تمبر")]
        public long? StampFee { get; set; }

        [Display(Name = "هزینه جمع آوری یا تفکیک")]
        public long? CollectionOrSeparationFee { get; set; }

        [Display(Name = "مبلغ اظهاری ارزش کالا")]
        public long? DeclaredGoodsValue { get; set; }

        [Display(Name = "مبلغ بسته بندی")]
        public long? PackagingFee { get; set; }

        [Display(Name = "مبلغ بیمه")]
        public long? InsuranceFee { get; set; }

        [Display(Name = "مبلغ تخفیف")]
        public long? Discount { get; set; }

        [Display(Name = "مبلغ ارزش افزوده")]
        public long? VAT { get; set; }

        [Display(Name = "سایر هزینه های مبدا")]
        public long? OtherOriginFees { get; set; }

        [Display(Name = "مبلغ روند")]
        public long? RoundingAmount { get; set; }

        [Display(Name = "هزینه متفرقه")]
        public long? MiscellaneousFee { get; set; }

        [Display(Name = "مبلغ حمل ترانزیت")]
        public long? TransitCargoFare { get; set; }

        [Display(Name = "مبلغ تفکیک ترانزیت")]
        public long? TransitSeparationFee { get; set; }

        [Display(Name = "مبلغ متفرقه ترانزیت")]
        public long? TransitMiscellaneousFee { get; set; }

        [Display(Name = "هزینه توزیع یا تفکیک")]
        public long? DistributionOrSeparationFee { get; set; }

        [Display(Name = "هزینه تفکیک قدیم")]
        public long? OldSeparationFee { get; set; }

        [Display(Name = "مبلغ متفرقه مقصد")]
        public long? DestinationMiscellaneousFee { get; set; }

        [Display(Name = "مبلغ پایه")]
        public long? BaseFare { get; set; }

        [Display(Name = "ارزش افزوده")]
        public long? AddedValue { get; set; }

        [Display(Name = "مبلغ کل خدمات")]
        public long? TotalServiceFee { get; set; }

        [Display(Name = "مبلغ کل بارنامه")]
        public long? TotalBillOfLadingAmount { get; set; }

        [Display(Name = "از مبدا")]
        public string FromOrigin { get; set; }

        [Display(Name = "به مقصد ترانزیت")]
        public string ToTransitDestination { get; set; }

        [Display(Name = "نماینده ترانزیت")]
        public string TransitRepresentative { get; set; }

        [Display(Name = "به مقصد")]
        public string ToDestination { get; set; }

        [Display(Name = "نماینده مقصد")]
        public string DestinationRepresentative { get; set; }

        [Display(Name = "کد مشتری")]
        public long? CustomerCode { get; set; }

        [Display(Name = "نام فرستنده")]
        public string SenderName { get; set; }

        [Display(Name = "آدرس فرستنده")]
        public string SenderAddress { get; set; }

        [Display(Name = "تلفن فرستنده")]
        public string SenderPhone { get; set; }

        [Display(Name = "کد ملی فرستنده")]
        public string SenderNationalCode { get; set; }

        [Display(Name = "کد مشتری")]
        public long? RecipientCustomerCode { get; set; }

        [Display(Name = "نام گیرنده")]
        public string RecipientName { get; set; }

        [Display(Name = "آدرس گیرنده")]
        public string RecipientAddress { get; set; }

        [Display(Name = "تلفن گیرنده")]
        public string RecipientPhone { get; set; }

        [Display(Name = "کد")]
        public string RecipientCode { get; set; }
        public string? RecipientPersonCode { get; set; }
        public string? SenderPersonCode { get; set; }

        [Display(Name = "آدرس")]
        public string RecipientZoneAddress { get; set; }

        [Display(Name = "نام زون")]
        public string RecipientZoneName { get; set; }

        [Display(Name = "کد")]
        public string SenderZoneCode { get; set; }

        [Display(Name = "آدرس")]
        public string SenderZoneAddress { get; set; }

        [Display(Name = "نام زون")]
        public string SenderZoneName { get; set; }

        [Display(Name = "نوع نرخ")]
        public string RateType { get; set; }

        [Display(Name = "تعداد کالا")]
        public long? GoodsCount { get; set; }

        [Display(Name = "وزن حجمی")]
        public float? VolumetricWeight { get; set; }

        [Display(Name = "وزن احتسابی")]
        public float? ChargeableWeight { get; set; }

        [Display(Name = "وزن واقعی بار")]
        public float? ActualCargoWeight { get; set; }

        [Display(Name = "محتویات")]
        public string Contents { get; set; }

        [Display(Name = "نحوه پرداخت")]
        public string PaymentMethod { get; set; }

        [Display(Name = "شرکت اعتباری")]
        public string? CreditCompany { get; set; }

        [Display(Name = "اطلاعات خدمات")]
        public string? ServiceInformation { get; set; }

        [Display(Name = "اطلاعات مالی")]
        public string FinancialInformation { get; set; }

        [Display(Name = "نام تحویل دهنده بار - پیک موتوری")]
        public string CourierName { get; set; }

        [Display(Name = "شماره پیگیری POS")]
        public string POSReceiptNumber { get; set; }

        [Display(Name = "تایید تحویل بار")]
        public string DeliveryConfirmation { get; set; }

        [Display(Name = "ابطال")]
        public bool? Cancellation { get; set; }

        [Display(Name = "تاریخ ابطال")]
        public DateTime? CancellationDate { get; set; }

        [Display(Name = "کاربر ابطال کننده")]
        public string CancellationUser { get; set; }

        [Display(Name = "جریمه ابطال")]
        public long? CancellationPenalty { get; set; }

        [Display(Name = "کد سرویس خدمات")]
        public long? ServiceCode { get; set; }

        [Display(Name = "تاریخ ورود اطلاعات")]
        public string DataEntryDate { get; set; }

        [Display(Name = "قفل رکورد")]
        public bool? RecordLock { get; set; }

        [Display(Name = "کد گروه بارنامه")]
        public long? BillOfLadingGroupCode { get; set; }

        [Display(Name = "کد کاربر وارد کننده")]
        public long? DataEntryUserCode { get; set; }

        [Display(Name = "نام کاربر وارد کننده")]
        public string DataEntryUserName { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? EditDate { get; set; }

        [Display(Name = "کاربر ویرایش کننده")]
        public string EditUserName { get; set; }

        [Display(Name = "توضیحات")]
        public string Remarks { get; set; }
        public bool? DistributorApprove { get; set; }
        public bool? BranchManagerApprove { get; set; }
        public string? Comments { get; set; }

        // ------------------------------------------
        [Display(Name = "هزینه انبارداری")]
        public long WarehousingCost { get; set; } = 0;
        [Display(Name = "سایر هزینه های تعریف نشده")]
        public long OtherDistributionCosts { get; set; } = 0;
        [Display(Name = "شرح هزینه های تعریف نشده")]
        public string? DistributionCostDescription { get; set; }
        [Display(Name = "تایید شده توسط نماینده")]
        public bool? ConfirmedByAgent { get; set; }
        [Display(Name = "تاریخ تایید توسط نماینده")]
        public DateTime? AgentConfirmationDate { get; set; }
        [Display(Name = "تایید شده توسط شعبه صادر کننده")]
        public bool? ConfirmedByIssuingBranch { get; set; }
        [Display(Name = "تاریخ تایید توسط شعبه صادرکننده")]
        public DateTime? IssuingBranchConfirmationDate { get; set; }
        [Display(Name = "کد تفصیلی نماینده مقصد")]
        public long? AgentDetailedCode { get; set; }
        [Display(Name = "کد تفصیلی شعبه صادرکننده")]
        public long? IssuingBranchDetailedCode { get; set; }
        public string? ErrorMessage { get; set; }
        public Int16 Status { get; set; } = 0;
        public Guid? DistributerId { get; set; }
        public Cu_BranchUser? DistributerBranch { get; set; }


    }


}
