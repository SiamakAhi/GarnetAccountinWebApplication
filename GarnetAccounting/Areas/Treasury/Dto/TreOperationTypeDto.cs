using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreOperationTypeDto
    {
        public int Id { get; set; }

        [Display(Name = "نام عملیات")]
        public string OperationName { get; set; }

        [Display(Name = "کد عملیات")]
        public string OperationCode { get; set; }

        [Display(Name = "نوع")]
        public string Type { get; set; }
    }
}
