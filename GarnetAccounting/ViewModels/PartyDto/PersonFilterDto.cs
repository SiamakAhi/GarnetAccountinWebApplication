using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.PartyDto
{
    public class PersonFilterDto
    {
        public long SellerId { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public short? legal { get; set; }
        public string? nationalId { get; set; }
        public string? tafsilCode { get; set; }
        public bool? isVendor { get; set; }
        public bool? isCustomer { get; set; }

        [Display(Name = "صفحه")]
        public int CurrentPage { get; set; } = 1;

        [Display(Name = "تعداد ردیف در هر صفحه")]
        public int PageSize { get; set; } = 25;
    }
}

