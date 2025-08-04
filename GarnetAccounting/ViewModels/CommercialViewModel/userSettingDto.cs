using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class userSettingDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public int? CustomerId { get; set; }
        public Int64? ActiveSellerId { get; set; }
        public string? ActiveSellerName { get; set; }
        public int? ActiveSellerPeriod { get; set; }
        public string? ActiveSellerPeriodName { get; set; }
        public short? DepartmentCode { get; set; }
        public Guid? BeranchId { get; set; }

        [Display(Name = "مدیریت فروشنده")]
        public bool AllowSellerManagement { get; set; }
        [Display(Name = "مدیریت کالا/خدمت")]
        public bool AllowStuffManagement { get; set; }
        [Display(Name = "مدیریت خریداران")]
        public bool AllowBuyerManagement { get; set; }
        [Display(Name = "فروش")]
        public bool AllowSaleManagement { get; set; }
        [Display(Name = "مدیریت کاربران")]
        public bool AllowUserManagement { get; set; }


    }
}
