using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto.BranchUserDto
{
    public class VmBranchUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public string BranchName { get; set; }
        public Guid BranchId { get; set; }
        public short Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public string CityName { get; set; }

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

        [Display(Name = "شناسه کاربر")]
        public string UserId { get; set; }
        public int BranchCityId { get; set; }
        public string BranchCityName { get; set; }
        public string BranchCode { get; set; }
        public Guid? BranchHubId { get; set; }
        public string? BranchHubName { get; set; }

    }
}
