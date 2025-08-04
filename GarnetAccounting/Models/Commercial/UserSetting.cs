using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Models.Commercial
{
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public int? CustomerId { get; set; }
        public Int64? ActiveSellerId { get; set; }
        public int? ActiveFinancePeriodId { get; set; }
        public bool AllowSellerManagement { get; set; }
        public bool AllowStuffManagement { get; set; }
        public bool AllowBuyerManagement { get; set; }
        public bool AllowSaleManagement { get; set; }
        public bool AllowUserManagement { get; set; }
        public short? DepartmentCode { get; set; }
        public Guid? BranchId { get; set; }
        public string? CurrentTheme { get; set; }
    }
}
