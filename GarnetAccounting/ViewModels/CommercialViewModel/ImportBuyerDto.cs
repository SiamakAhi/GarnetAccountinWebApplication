namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class ImportBuyerDto
    {
        public bool AlowDuplicate { get; set; }
        public bool CheckNationalId { get; set; }
        public IFormFile ExcelFile { get; set; }
    }
}
