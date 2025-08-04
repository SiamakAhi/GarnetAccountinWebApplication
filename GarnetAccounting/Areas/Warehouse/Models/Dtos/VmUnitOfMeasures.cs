using GarnetAccounting.Areas.Warehouse.Dto;

namespace GarnetAccounting.Areas.Warehouse.Models.Dtos
{
    public class VmUnitOfMeasures
    {
        public List<UnitOfMeasureDto>? Measures { get; set; }
        public UnitOfMeasureDto? Dto { get; set; }
    }
}
