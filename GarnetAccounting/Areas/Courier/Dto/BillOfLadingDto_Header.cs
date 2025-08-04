using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class BillOfLadingDto_Header
    {
        public Guid Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "شماره بارنامه")]
        public string WaybillNumber { get; set; }

        [Display(Name = "تاریخ صدور")]
        [Required(ErrorMessage = "تاریخ بارنامه نامشخص است")]
        public DateTime IssuanceDate { get; set; } = DateTime.Now;

        [Display(Name = " مسیر")]
        [Required(ErrorMessage = "نعیین مسیر الزامی است")]
        public int RouteId { get; set; }


        [Display(Name = " سرویس")]
        [Required(ErrorMessage = "نوع سرویس را مشخص کنید")]
        public int ServiceId { get; set; }

        [Display(Name = "شعبه صادرکننده")]
        public Guid OriginBranchId { get; set; }
        [Display(Name = "شعبه صادرکننده")]
        public string? OriginBranchName { get; set; }

        [Display(Name = " فرستنده")]
        [Required(ErrorMessage = "تعیین فرستنده الزامی است")]
        public long SenderId { get; set; }
        [Display(Name = " آدرس فرستنده")]
        [Required(ErrorMessage = "نوشتن آدرس فرستنده الزامی است")]
        public string SenderAddress { get; set; }

        [Display(Name = " گیرنده")]
        [Required(ErrorMessage = "تعیین گیرنده الزامی است")]
        public long ReceiverId { get; set; }
        [Display(Name = " آدرس گیرنده")]
        [Required(ErrorMessage = "نوشتن آدرس گیرنده الزامی است")]
        public string ReceiverAddress { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }


        [Display(Name = "تعداد تحویل شده")]
        public int DeliveredCount { get; set; } = 0;

        [Display(Name = "مبدأ")]
        public string? OriginCity { get; set; }

        [Display(Name = "مقصد")]
        public string? DestinationCity { get; set; }


        // 1- نقدی
        // 2- اعتباری
        // 3- پسکرایه
        [Display(Name = " نوع پرداخت")]
        public short? SettelmentType { get; set; }
        public string? LastStatusDescription { get; set; }

        [Display(Name = " وضعیت بارنامه")]
        [Required(ErrorMessage = "فیلد شناسه وضعیت بارنامه الزامی است")]
        public short BillOfLadingStatusId { get; set; } = 1;

        [Display(Name = "هاب مبدأ")]
        [Required(ErrorMessage = "هاب مبدأ شناسایی نشد")]
        public Guid OriginHubId { get; set; }


        [Display(Name = "هاب مقصد")]
        public Guid? DestinationHubId { get; set; }

        [Display(Name = "کاربر ثبت کننده")]
        [Required(ErrorMessage = " کاربر ثبت کننده شناسس نشد")]
        public string CreatedBy { get; set; }

        [Display(Name = "تاریخ به روزرسانی")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "کاربر ادیت کننده")]
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? SenderName { get; set; }
        public string? SenderPhone { get; set; }
        public string? ReciverName { get; set; }
        public string? ReciverPhone { get; set; }
        public string? ServiceName { get; set; }

    }
}
