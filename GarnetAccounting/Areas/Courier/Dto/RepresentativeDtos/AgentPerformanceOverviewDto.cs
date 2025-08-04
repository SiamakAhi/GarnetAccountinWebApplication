namespace GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos
{
    public class AgentPerformanceOverviewDto
    {
        public RepFilterDto filter { get; set; } = new RepFilterDto() { CurrentPage = 1, PageSize = 50 };
        public Pagination<RepresentativeRate> Report { get; set; }
    }
}
