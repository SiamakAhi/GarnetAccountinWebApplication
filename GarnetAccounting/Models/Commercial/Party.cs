using GarnetAccounting.Areas.Commercial.Models.Entities;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Areas.Projects.Models.Entities;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.Models.Identity;
using System.ComponentModel.DataAnnotations;

public class Party
{
    [Display(Name = "شناسه")]
    public Int64 Id { get; set; }
    public Int64? uyer_SellerId { get; set; }
    public string Title { get; set; }

    public int CustomerId { get; set; }

    [Display(Name = "شناسه سیستم حسابداری")]
    public string? AccountingSystemId { get; set; }

    [Display(Name = "حقیقی / حقوقی")]
    public Int16 LegalStatus { get; set; }

    public Int16? TaxPayerType { get; set; }

    [Display(Name = "نام")]
    public string Name { get; set; }

    public string? fullNameEn { get; set; }


    [Display(Name = "شناسه ملی")]
    public string? NationalId { get; set; }

    [Display(Name = "کد اقتصادی جدید")]
    public string? EconomicCode { get; set; }

    [Display(Name = "کد حافظه مالیاتی")]
    public string? TaxMemoryId { get; set; }

    [Display(Name = "شماره ثبت")]
    public string? RegistrationNumber { get; set; }

    [Display(Name = "استان")]
    public string? Province { get; set; }

    [Display(Name = "شهر")]
    public string? City { get; set; }

    [Display(Name = "نشانی")]
    public string? Address { get; set; }

    [Display(Name = "کدپستی")]
    public string? PostalCode { get; set; }

    [Display(Name = " موبایل")]
    public string? MobilePhone { get; set; }

    [Display(Name = "نمابر")]
    public string? Fax { get; set; }

    [Display(Name = "Public Key")]
    public string? SellerPublicKey { get; set; }
    public string? SellerPublicKeyAddress { get; set; }

    [Display(Name = "Private Key")]
    public string? SellerPrivateKey { get; set; }
    public string? SellerPrivateAddress { get; set; }

    [Display(Name = "CSR Key")]
    public string? SellerCSRKey { get; set; }
    public string? SellerCSRKeyAddress { get; set; }

    [Display(Name = "لوگو")]
    public string? Logo { get; set; }

    [Display(Name = "آیا لوگو در فاکتور نمایش داده شود؟")]
    public bool IsLogoDisplayedOnInvoice { get; set; } = true;

    [Display(Name = "توضیحات فاکتور")]
    public string? InvoiceDescription { get; set; }

    public bool IsActive { get; set; } = true;

    public long? TafsilId { get; set; }
    public string? TafsilCode { get; set; }

    [Display(Name = "نام مدیرعامل")]
    public string? CEOName { get; set; }

    [Display(Name = "شماره تماس مدیرعامل")]
    public string? CEOContactNumber { get; set; }

    [Display(Name = "شماره پرونده مالیاتی")]
    public string? TaxFileNumber { get; set; }
    [Display(Name = "شماره پیگیری ثبت نام")]
    public string? TaxTrackingNumber { get; set; }

    [Display(Name = "کد واحد مالیاتی")]
    public string? TaxUnitCode { get; set; }

    [Display(Name = "آدرس واحد مالیاتی")]
    public string? TaxUnitAddress { get; set; }

    [Display(Name = "ممیز مالیاتی")]
    public string? TaxAuditor { get; set; }

    [Display(Name = "رمز عبور پنل مالیاتی")]
    public string? TaxPanelPassword { get; set; }

    public virtual Cu_Representative? Representative { get; set; }

    // فیلدهای اضافی برای تعیین نقش
    [Display(Name = "نقش")]
    public Int16 Role { get; set; }

    public bool? IsVendor { get; set; }
    public bool? IsCustomer { get; set; }
    //Navigations
    public virtual Customer PartyCustomer { get; set; }
    public virtual TaxPayerType? PayerType { get; set; }


    //نمایندگان
    public virtual ICollection<PartyRepresentative>? PartyRepresentatives { get; set; }
    public virtual ICollection<Cu_BillOfLading> Senders { get; set; } = new List<Cu_BillOfLading>();
    public virtual ICollection<Cu_BillOfLading> Recivers { get; set; } = new List<Cu_BillOfLading>();


    //پروژه های کارفرما
    public virtual ICollection<Con_Project> ClientProjects { get; set; } = new List<Con_Project>();

    //فاکتورها
    public virtual ICollection<com_Invoice>? Invoices { get; set; }
    public virtual AppIdentityUser? User { get; set; }
}

public enum PartyRole : Int16
{
    Seller = 1,
    Buyer = 2,

}


