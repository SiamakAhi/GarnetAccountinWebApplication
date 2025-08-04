using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_BranchUser
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public long sellerId { get; set; }
        public virtual Cu_Branch Branch { get; set; }

        [Display(Name = "نام کامل")]
        public string FullName { get; set; }

        [Display(Name = "ناظر")]
        public bool IsSupervisor { get; set; }

        [Display(Name = "شناسه موقعیت")]
        public int PositionId { get; set; }


        // New boolean fields added as requested
        [Display(Name = "صادرکننده بارنامه داخلی")]
        public bool IsInternalBLIssuer { get; set; }

        [Display(Name = "صادر کننده بارنامه خارجی")]
        public bool IsExternalBLIssuer { get; set; }

        [Display(Name = "توزیع کننده بارنامه")]
        public bool IsBLIssuerDistributor { get; set; }

        [Display(Name = "مدیر عملیات توزیع و جمع آوری")]
        public bool IsDistCollectManager { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "ماشین رهسپاری")]
        public bool IsDispatchVehicle { get; set; }

        [Display(Name = "نام کاربری")]
        public string userName { get; set; }

        [Display(Name = "شناسه کاربر")]
        public string UserId { get; set; }
        public virtual AppIdentityUser IdentityUser { get; set; }

        public virtual ICollection<KPOldSystemSaleReport> Distributers { get; set; } = new List<KPOldSystemSaleReport>();

    }
}
