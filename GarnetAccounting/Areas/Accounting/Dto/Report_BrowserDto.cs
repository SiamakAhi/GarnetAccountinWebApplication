using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class Report_BrowserDto
    {
        public int Id { get; set; }

        [Display(Name = "نوع حساب")]
        public short TypeId { get; set; }
        public string? TypeName { get; set; }

        [Display(Name = "کد حساب")]
        public string KolCode { get; set; }
        public int? KolId { get; set; }

        [Display(Name = "نام حساب")]
        public string KolName { get; set; }

        [Display(Name = "شرح حساب")]
        public string? Description { get; set; }

        [Display(Name = "ماهیت")]
        public short Nature { get; set; }
        public short MandehNature { get; set; } = 3;
        public string NatureName => (MandehNature.AccToBalanceNatureName());
        public bool isMatch { get; set; }
        public long? SellerId { get; set; }
        public int? PeriodId { get; set; }

        [Display(Name = "گروه حساب")]
        public short GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupCode { get; set; }
        public short KolNature { get; set; }
        public short MoeinNature { get; set; }
        public int MoeinId { get; set; }
        public string? MoeinName { get; set; }
        public string? MoeinCode { get; set; }

        public long? CurrentTafsilId { get; set; }
        public long? Tafsil4Id { get; set; }
        public string? Tafsil4Name { get; set; }
        public long? Tafsil5Id { get; set; }
        public string? Tafsil5Name { get; set; }
        public long? Tafsil6Id { get; set; }
        public string? Tafsil6Name { get; set; }
        public long? Tafsil7Id { get; set; }
        public string? Tafsil7Name { get; set; }
        public long? Tafsil8Id { get; set; }
        public string? Tafsil8Name { get; set; }

        public long Bed { get; set; }
        public long Bes { get; set; }
        public long Mandeh { get; set; }
        public long MandehBed { get; set; }
        public long MandehBes { get; set; }
        public int GroupPeriority { get; set; }
    }
}
