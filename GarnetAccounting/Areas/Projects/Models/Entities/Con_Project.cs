using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Areas.Commercial.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Projects.Models.Entities
{
    public class Con_Project
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "نام پروژه")]
        [Required(ErrorMessage = "نام پروژه الزامی است")]
        public string ProjectName { get; set; }

        [Display(Name = "شماره قرارداد")]
        public string? ProjectNumber { get; set; }

        [Display(Name = "کارفرما")]
        public long? ClientId { get; set; }
        [Display(Name = "کارفرما")]
        public virtual Party? Client { get; set; }

        [Display(Name = "حساب تفصیلی")]
        public long? TafsilId { get; set; }


        [Display(Name = "تاریخ شروع پروژه")]
        public DateTime? ProjectStartDate { get; set; }

        [Display(Name = "مبلغ پروژه")]
        public long? ProjectAmount { get; set; }

        [Display(Name = "مدت پیمان (روز)")]
        public int? ContractDurationDays { get; set; }

        [Display(Name = "کاربر ایجاد کننده")]
        [Required(ErrorMessage = "کاربر ایجاد کننده الزامی است")]
        public string CreatedBy { get; set; }

        public bool IsActive { get; set; } = true;
        public virtual ICollection<Acc_Article>? AccArticles { get; set; }
        public virtual ICollection<com_Invoice>? ProjectInvoices { get; set; }

    }
}
