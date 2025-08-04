namespace GarnetAccounting.ViewModels
{
    public class XmlSearchResultDto
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string RunDate { get; set; }
        public string ExpirationDate { get; set; }
        public string SpecialOrGeneral { get; set; }
        public string TaxableOrFree { get; set; }
        public string Vat { get; set; }
        public string VatCustomPurposes { get; set; }
        public string DescriptionOfID { get; set; }

        // سازنده بدون پارامتر
        public XmlSearchResultDto()
        {
        }

        // سازنده با پارامتر، در صورت نیاز
        public XmlSearchResultDto(string id, string type, string date, string runDate, string expirationDate, string specialOrGeneral, string taxableOrFree, string vat, string vatCustomPurposes, string descriptionOfID)
        {
            ID = id;
            Type = type;
            Date = date;
            RunDate = runDate;
            ExpirationDate = expirationDate;
            SpecialOrGeneral = specialOrGeneral;
            TaxableOrFree = taxableOrFree;
            Vat = vat;
            VatCustomPurposes = vatCustomPurposes;
            DescriptionOfID = descriptionOfID;
        }
    }

}
