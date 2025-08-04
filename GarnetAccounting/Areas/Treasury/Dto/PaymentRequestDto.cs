namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class PaymentRequestDto
    {
        public long Amount { get; set; }  // مبلغ پرداخت
        public string OrderId { get; set; }  // شناسه سفارش
        public string CallbackUrl { get; set; }  // لینک بازگشت
    }
}
