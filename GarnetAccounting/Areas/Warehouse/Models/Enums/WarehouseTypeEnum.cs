using System.ComponentModel.DataAnnotations;

/// <summary>
/// انواع تراکنش انبار (برای Wh_InventoryDocument.DocumentType و Wh_InventoryTransaction.TransactionType)
/// </summary>
public enum InventoryDocumentType
{
    [Display(Name = "رسید خرید / ورود")]
    PurchaseReceipt = 1,

    [Display(Name = "حواله خروج / ارسال")]
    SalesIssue = 2,

    [Display(Name = "انتقال ورودی")]
    TransferIn = 3,

    [Display(Name = "انتقال خروج")]
    TransferOut = 4,

    [Display(Name = "تعدیل موجودی")]
    Adjustment = 5,

    [Display(Name = "بازگشت از فروش")]
    CustomerReturn = 6,

    [Display(Name = "رسید از تولید")]
    ProductionReceipt = 7
}

/// <summary>
/// حالت رزرو
/// </summary>
public enum ReservationStatus
{
    [Display(Name = "فعال")]
    Active = 1,

    [Display(Name = "مصرف‌شده")]
    Consumed = 2,

    [Display(Name = "آزاد شده")]
    Released = 3,

    [Display(Name = "منقضی")]
    Expired = 4
}

/// <summary>
/// روش ارزیابی موجودی
/// </summary>
public enum CostMethod
{
    [Display(Name = "FIFO (اول وارد، اول خارج)")]
    FIFO = 1,

    [Display(Name = "LIFO (آخر وارد، اول خارج)")]
    LIFO = 2,

    [Display(Name = "میانگین وزنی")]
    WeightedAverage = 3
}

