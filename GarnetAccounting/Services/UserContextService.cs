namespace GarnetAccounting.Services
{
    public class UserContextService
    {
        public long? SellerId { get; set; }
        public int? PeriodId { get; set; }
        public int? CustomerId { get; set; }
        public string? FinancPeriodStartDate { get; set; }
        public string? FinancPeriodEndDate { get; set; }
        public Guid? BranchId { get; set; }
        public short? DepartmentCode { get; set; }

        public void SetSellerId(long sellerId)
        {
            SellerId = sellerId;
        }

        public void SetPeriodId(int periodId)
        {
            PeriodId = periodId;
        }
        public void SetCustomerId(int customerId)
        {
            CustomerId = customerId;
        }
        public void SetPeriodStartDate(string startDate)
        {
            FinancPeriodStartDate = startDate;
        }
        public void SetPeriodEndDate(string EndDate)
        {
            FinancPeriodEndDate = EndDate;
        }
        public void SetDepartmentCode(short departmentCode)
        {
            DepartmentCode = departmentCode;
        }
        public void SetBranchId(Guid bi)
        {
            BranchId = bi;
        }
    }

}
