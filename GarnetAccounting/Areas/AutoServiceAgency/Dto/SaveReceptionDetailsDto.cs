using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class SaveReceptionDetailsDto
    {
        [Required(ErrorMessage = "شماره پذیرش الزامی است")]
        public string ReceptionNumber { get; set; }

        [Required(ErrorMessage = "شماره پلاک الزامی است")]
        public string LicensePlate { get; set; }

        public List<ReceptionItemDto> Items { get; set; } = new List<ReceptionItemDto>();
    }

    public class ReceptionItemDto
    {
        public long Id { get; set; }
        public int ContractorId { get; set; }
        public int ServiceId { get; set; }
    }



}
