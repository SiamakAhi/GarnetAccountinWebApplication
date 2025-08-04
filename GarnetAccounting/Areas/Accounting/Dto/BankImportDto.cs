namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BankImportDto
    {
        public IFormFile ReportFile { get; set; }
        public int BankAccountId { get; set; }
    }

}
