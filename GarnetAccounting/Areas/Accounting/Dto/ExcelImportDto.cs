namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ExcelImportDto
    {
        public IFormFile File { get; set; }
        public int Reporttype { get; set; }
        public bool AppendDoc { get; set; } = true;
    }
}
