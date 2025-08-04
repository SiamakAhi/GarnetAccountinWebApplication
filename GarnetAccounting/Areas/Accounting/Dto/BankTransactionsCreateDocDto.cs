using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BankTransactionsCreateDocDto
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public string? UserName { get; set; }

        [Required(ErrorMessage = "حساب معین را انتخاب کنید")]
        public int MoeinId { get; set; }
        public long? Tafsil4Id { get; set; }
        public long? Tafsil5Id { get; set; }

        public List<long> TransactionsId { get; set; }
        public int TransactionsType { get; set; }
        public int BankAccountId { get; set; }
        public bool CreateNewDoc { get; set; } = false;
        public string? Descriptions { get; set; }
        public bool Grouped { get; set; } = false;
        public bool AppendBankDescription { get; set; } = false;
        public bool InsertTrackingNumber { get; set; } = true;

        public string? DocSelector { get; set; }
    }
}
