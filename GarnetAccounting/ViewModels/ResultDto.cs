namespace GarnetAccounting.ViewModels
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? ReturnUrl { get; set; }
        public string? ExceptionError { get; set; }
    }
}
