using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class RateWeightRangeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وزن شروع الزامی است.")]
        [Range(0, double.MaxValue, ErrorMessage = "وزن شروع باید یک عدد مثبت باشد.")]
        [Display(Name = "وزن شروع")]
        public double StartWeight { get; set; }

        [Required(ErrorMessage = "وزن پایان الزامی است.")]
        [Range(0, double.MaxValue, ErrorMessage = "وزن پایان باید یک عدد مثبت باشد.")]
        [Display(Name = "وزن پایان")]
        public double EndWeight { get; set; }

        [Required(ErrorMessage = "درصد ضریب وزن الزامی است.")]
        [Range(0, 100, ErrorMessage = "درصد ضریب وزن باید بین ۰ تا ۱۰۰ باشد.")]
        [Display(Name = "درصد ضریب وزن")]
        public decimal WeightFactorPercent { get; set; }
    }
}