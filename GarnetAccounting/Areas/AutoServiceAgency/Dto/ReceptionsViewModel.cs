namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class ReceptionsViewModel
    {
        public ReciptionFilterDto filter { get; set; } = new ReciptionFilterDto();
        public List<ReceptionHeaderDto> Receptions { get; set; }
    }
}
