namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class SaveDetails_ItemDto
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public string Itemtype { get; set; }
        public string Income { get; set; }
        public short IncomeCode { get; set; }
        public int? ServiceId { get; set; }
        public int? LamariServiceId { get; set; }
        public int? ContractorId { get; set; }
        public float Percentage { get; set; }
        public long Price { get; set; }

        public long ContractorShareAmount { get; set; }
        public string strContractorShareAmount { get; set; } = "0";

        //Lamari
        public short? LamaryServiceType { get; set; }
        public long lamariMechanicShareAmount { get; set; } = 0;
        public string lamaristrMechanicShareAmount { get; set; } = "0";
        public int? lamariMechanicContractor { get; set; }
        public long lamariElectricShareAmount { get; set; } = 0;
        public string lamaristrElectricShareAmount { get; set; } = "0";
        public int? lamariElectricContractor { get; set; }
    }
}
