using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class ConsigmentDto
    {
        public Guid Id { get; set; }

        public long SellerId { get; set; }

        [Display(Name = "کد")]
        public string Code { get; set; }

        [Display(Name = "وزن")]
        [Required(ErrorMessage = "فیلد وزن الزامی است")]
        public float Weight { get; set; }

        [Display(Name = "ارتفاع")]
        public float Height { get; set; }

        [Display(Name = "عرض")]
        public float Width { get; set; }

        [Display(Name = "طول")]
        public float Length { get; set; }

        [Display(Name = "حجم")]
        public float Volume { get; set; }

        [Display(Name = "محتوا")]
        public string? ContentDescription { get; set; }

        [Display(Name = "ارزش مرسوله")]
        [Required(ErrorMessage = "فیلد ارزش مرسوله الزامی است")]
        public long Value { get; set; } = 0;

        [Display(Name = "اطلاعات خدماتی")]
        public string? ServiceInformation { get; set; }

        [Display(Name = "تحویل گرفته شده است؟")]
        public bool IsDelivered { get; set; } = false;

        [Display(Name = "نام تحویل گیرنده")]
        public string? RecipientName { get; set; }

        [Display(Name = "تاریخ تحویل به گیرنده")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "امضای الکترونیکی تحویل‌گیرنده")]
        public byte[]? ReceiverSignature { get; set; }


        [Display(Name = "مبلغ رهسپاری")]
        public long CargoFare { get; set; }

        [Display(Name = "جمع هزینه های ")]
        public long TotalCostPrice { get; set; }

        [Display(Name = "تخفیف")]
        public long Discount { get; set; }

        [Display(Name = "نرخ ارزش افزوده")]
        public float VatRate { get; set; }

        [Display(Name = "مبلغ ارزش افزوده")]
        public long VatPrice { get; set; }

        [Display(Name = "مبغ نهایی محموله")]
        public long TotalPrice { get; set; }


        [Display(Name = "شناسه بارنامه")]
        [Required]
        public Guid BillOfLadingId { get; set; }

        [Display(Name = " ماهیت مرسوله")]
        [Required(ErrorMessage = "تعیین ماهیت مرسوله الزامی است")]
        public short NatureTypeId { get; set; }

        [Display(Name = "نوع بسته بندی")]
        [Required(ErrorMessage = "تعیین نوع بسته بندی الزامی است")]
        public int PackageTypeId { get; set; }
    }
}
