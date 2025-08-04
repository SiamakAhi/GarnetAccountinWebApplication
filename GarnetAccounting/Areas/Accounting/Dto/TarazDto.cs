namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TarazDto
    {
        public DocFilterDto? filter { get; set; }
        public ReportHeaderDto? reportHeader { get; set; }
        public List<TarazAzmayeshiDto>? report { get; set; }
        public Pagination<TarazAzmayeshiDto>? Taraz { get; set; }

    }
}
