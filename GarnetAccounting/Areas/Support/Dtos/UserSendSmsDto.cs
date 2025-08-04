namespace GarnetAccounting.Areas.Support.Dtos
{
    public class UserSendSmsDto
    {
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string? SenderUserId { get; set; }
        public string? SenderCustomerId { get; set; }
        public string? SenderName { get; set; }
        public string? SenderCustomerName { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
