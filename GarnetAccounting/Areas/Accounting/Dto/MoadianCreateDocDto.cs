using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class MoadianCreateDocDto
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public string? UserName { get; set; }

        //-- Debtor Side 
        [Required(ErrorMessage = "حساب معین طرف بدهکار سند را انتخاب کنید")]
        public int BedMoeinId { get; set; }
        public long? BedTafsil4Id { get; set; }
        public long? BedTafsil5Id { get; set; }

        //-- Credator Side
        [Required(ErrorMessage = "حساب معین طرف بستانکار سند را انتخاب کنید")]

        public int BesMoeinId { get; set; }
        public long? BesTafsil4Id { get; set; }
        public long? BesTafsil5Id { get; set; }

        public string? Descriptions { get; set; }

        public List<long> TransactionsId { get; set; }
        public short transactionsType { get; set; }
        public bool CreateNewDoc { get; set; } = false;
        public bool Grouped { get; set; } = true;
        public bool InsertTrackingNumber { get; set; } = true;

        public string? DocSelector { get; set; }


    }
}
