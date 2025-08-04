using System.ComponentModel.DataAnnotations;

public enum WarehouseTypeEnum
{
    [Display(Name = "حواله خروج")]
    Issue = 1,

    [Display(Name = "رسید ورود")]
    Receipt = 2,

    [Display(Name = "انتقال")]
    Transfer = 3,

    [Display(Name = "برگشت از خرید")]
    PurchaseReturn = 4,

    [Display(Name = "برگشت از فروش")]
    SalesReturn = 5,

    [Display(Name = "دریافت امانی")]
    ConsignmentIn = 6,

    [Display(Name = "ارسال امانی")]
    ConsignmentOut = 7
}

public enum WarehouseDocumentStatusEnum
{
    [Display(Name = "پیش‌نویس")]
    Draft = 1,

    [Display(Name = "تأیید‌شده")]
    Confirmed = 2,

    [Display(Name = "لغوشده")]
    Canceled = 3
}

public enum UnitTypeEnum
{
    [Display(Name = "واحد خرید")]
    Purchase = 1,

    [Display(Name = "واحد فروش")]
    Sales = 2,

    [Display(Name = "واحد موجودی")]
    Inventory = 3
}

public enum TransactionTypeEnum
{
    [Display(Name = "ورود کالا")]
    Receipt = 1,

    [Display(Name = "خروج کالا")]
    Issue = 2,

    [Display(Name = "انتقال داخلی")]
    Transfer = 3,

    [Display(Name = "تولید")]
    Production = 4,

    [Display(Name = "مصرف")]
    Consumption = 5
}

public enum ProductTypeEnum
{
    [Display(Name = "کالا")]
    Goods = 1,

    [Display(Name = "خدمات")]
    Service = 2,

    [Display(Name = "مواد اولیه")]
    RawMaterial = 3,

    [Display(Name = "محصول نیمه‌ساخته")]
    SemiFinished = 4,

    [Display(Name = "محصول نهایی")]
    FinishedProduct = 5
}