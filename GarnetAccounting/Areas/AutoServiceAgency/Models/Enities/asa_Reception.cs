using GarnetAccounting.Areas.AutoServiceAgency.Models.Enities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AutoServiceAgency.Models.Entities
{

    [Display(Name = "اطلاعات پذیرش", Description = "اطلاعات مربوط به پذیرش خدمات خودرو")]
    public class asa_Reception
    {
        [Key]
        [DisplayName("شناسه")]
        public long Id { get; set; }

        [DisplayName("شماره پذیرش")]
        public string ReceptionNumber { get; set; }

        [DisplayName("نام و نام خانوادگی مشتری")]
        public string? CustomerFullName { get; set; }

        [DisplayName("کد نمایندگی")]
        public string? AgentCode { get; set; }

        [DisplayName("کیلومتر پذیرش")]
        public int? KilometersAtReception { get; set; }

        [DisplayName("تاریخ پذیرش")]
        public DateTime ReceptionDate { get; set; }

        [DisplayName("کد")]
        public string? Code { get; set; }

        [DisplayName("کد لجستیک")]
        public string? LogisticsCode { get; set; }

        [DisplayName("نام")]
        public string? Name { get; set; }

        [DisplayName("قیمت")]
        public long? Price { get; set; } = 0;

        [DisplayName("از محل (کد)")]
        public short LocationCode { get; set; }

        [DisplayName("از محل (نام)")]
        public string? LocationName { get; set; }

        [DisplayName("تعداد")]
        public int? Quantity { get; set; }

        [DisplayName("نوع (کد)")]
        public short? TypeCode { get; set; }

        [DisplayName("نوع (نام)")]
        public string? TypeName { get; set; }

        [DisplayName("کد فاکتور")]
        public string? InvoiceCode { get; set; }

        [DisplayName("تاریخ فاکتور")]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("کمپین")]
        public bool Campaign { get; set; } = false;

        [DisplayName("برند")]
        public string? Brand { get; set; }

        [DisplayName("شماره سند")]
        public long? DocNum { get; set; }

        [DisplayName("آیدی دوره")]
        public int? PeriodId { get; set; }

        [DisplayName("شماره آرشیو")]
        public string? ArchiveNum { get; set; }

        [DisplayName("نرخ شخصی")]
        public byte? PersonRate { get; set; }

        [DisplayName("دارای سند؟")]
        public bool? HasDocument { get; set; }

        [DisplayName("شناسه فروشنده")]
        public long SellerId { get; set; }

        [DisplayName("جمع قطعه")]
        public long TotalGhate { get; set; } = 0;

        [DisplayName("جمع اجرت")]
        public long TotalOjrat { get; set; } = 0;

        [DisplayName("هزینه اضافی")]
        public long? ExtraPrice { get; set; } = 0;

        [DisplayName("تاریخ ایجاد")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [DisplayName("کاربر ایجاد کننده")]
        public string CtreateBy { get; set; }

        public string? BachRefrense { get; set; }

        [DisplayName("پلاک خودرو")]
        public string? LicensePlate { get; set; }

        [DisplayName("پیمانکار")]
        public int? ContractorId { get; set; }

        [DisplayName("درصد پورسانت پیمانکار")]
        public float ContractorPersentage { get; set; } = 0;

        [DisplayName("سهم پیمانکار از فروش خدمات")]
        public long ContractorShareMoney { get; set; } = 0;
        public long? BankTafsilId { get; set; }

        public virtual asa_Contractor? Contractor { get; set; }

        public int? ServiceId { get; set; }
        public virtual asa_Services? Service { get; set; }

        public short? ServiceType { get; set; }
        public int? LamariServiceId { get; set; }
        public virtual asa_LamariService? LamariService { get; set; }

        public long lamariMechanicShareAmount { get; set; } = 0;
        public int? lamariMechanicContractorId { get; set; }
        public long lamariElectricShareAmount { get; set; } = 0;
        public int? lamariElectricContractorId { get; set; }
        public virtual asa_Contractor? ElectricContractor { get; set; }
        public virtual asa_Contractor? MechanicContractor { get; set; }

    }
}