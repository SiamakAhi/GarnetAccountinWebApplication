using GarnetAccounting.ViewModels.PartyDto;

namespace GarnetAccounting.ViewModels
{
    public class PersonListDto
    {
        public Pagination<PersonDto> persen { get; set; }
        public PersonFilterDto filter { get; set; }
    }
}
