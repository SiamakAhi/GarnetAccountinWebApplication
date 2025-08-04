using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreCheckDto
    {
        [Key]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [DisplayName("شناسه تراکنش")]
        public Guid TransactionId { get; set; }

        [DisplayName("نوع تراکنش")]
        public int OperationTypeId { get; set; }

        [DisplayName("تاریخ چک")]
        public DateTime CheckDate { get; set; }

        [DisplayName("تاریخ سررسید چک")]
        public DateTime DueDate { get; set; }

        [DisplayName("نام بانک")]
        public int BankId { get; set; }


        [DisplayName("صاحب چک")]
        public int CheckOwnerId { get; set; }


        [DisplayName("مبلغ چک")]
        public decimal Amount { get; set; }


        [DisplayName("شناسه عملیات چک")]
        public int CheckOperationId { get; set; }

    }
}
