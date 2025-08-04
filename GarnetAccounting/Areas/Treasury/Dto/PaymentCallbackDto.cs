namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class PaymentCallbackDto
    {
        public string OrderId { get; set; }  // شناسه سفارش
        public string Status { get; set; }  // وضعیت تراکنش
        public string PaymentId { get; set; }  // شناسه پرداخت
    }
}
