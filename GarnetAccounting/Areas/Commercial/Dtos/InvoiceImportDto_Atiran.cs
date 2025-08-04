namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class InvoiceImportDto_Atiran
    {
        public List<ImportRawData_Atiran> Items { get; set; }
        public List<ErrorDto> Errors { get; set; }
    }
}
