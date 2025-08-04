using GarnetAccounting.Areas.Accounting.Classes;
using GarnetAccounting.Areas.Accounting.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccCodingService
    {
        SelectList SelectList_AccountType();
        SelectList SelectList_GroupType();
        SelectList SelectList_Nature();
        SelectList SelectList_DocTypes();
        //
        Task<SelectList> SelectList_GroupAccountsAsync(long sellerId, Int16 typeid);
        Task<SelectList> SelectList_TemporaryAccounts_KolAsync(long sellerId);
        Task<SelectList> SelectList_TemporaryAccounts_MoeinAsync(long sellerId);
        Task<SelectList> SelectList_PermanentAccounts_KolAsync(long sellerId);
        Task<SelectList> SelectList_PermanentAccounts_MoeinAsync(long sellerId);


        // Default Coding
        Task<clsResult> ResetSellerAccountingData(long sellerId);
        Task<clsResult> LoadDefaultCoding_AddGroupsAsync(long sellerId);
        Task<clsResult> LoadDefaultCoding_CommercialAsync(long sellerId);
        Task<clsResult> LoadDefaultCoding_SetGroupIdAsync(long sellerId);

        //Group
        Task<List<GroupDto>> GetGroupsAsync(long? SellerId = null);
        Task<GroupDto> GetGroupDtoAsync(int Id);
        Task<clsResult> AddGroupAsync(GroupDto dto);
        Task<clsResult> EditGroupAsync(GroupDto dto);
        Task<clsResult> DeleteGroupAsync(short id);

        //Kol
        Task<SelectList> SelectList_KolsAsync(long sellerId);
        Task<List<KolDto>> GetKolsAsync(short? groupId = null, long? SellerId = null);
        Task<KolDto?> GetKolDtoAsync(int id);
        Task<int?> GetKolIdByCodeAsync(string code, long sellerId);
        Task<clsResult> AddKolAsync(KolDto dto);
        Task<clsResult> EditKolAsync(KolDto dto);
        Task<clsResult> DeleteThePeriodAsync(int id);
        Task<string> GenerateKolCodeAsync(int groupId);

        //Moein
        Task<string> GenerateMoeinCodeAsync(int KolId);
        Task<List<MoeinDto>> GetMoeinsAsync(int? kolId = null, long? SellerId = null);
        Task<MoeinDto> GetMoeinDtoByIdAsync(int Id);
        Task<int?> GetMoeinIdByCodeAsync(string code, long sellerId);
        Task<clsResult> AddMoeinAsync(MoeinDto dto);
        Task<clsResult> EditMoeinAsync(MoeinDto dto);
        Task<SelectList> SelectList_MoeinsAsync(long? sellerId = null, string name = "", int? kolId = null);
        Task<SelectList> SelectList_Moeins2Async(long sellerId, List<int>? kols = null);
        Task<SelectList> SelectList_UsageMoeinsAsync(long? sellerId = null);
        Task<int?> GetKolIdByMoeinIdAsync(int? MoeinId);
        Task<clsResult> DeleteMoeinAsync(int Id);


        //Tafsil Group
        Task<List<TafsilGroupDto>> TafsilsGroupAsync(Int64? SellerId = null);
        Task<TafsilGroupDto> GetTafsilGroupById(int Id);
        Task<SelectList> SelectList_TafsilGroupsAsync(Int64? SellerId = null);
        Task<clsResult> AddTafsilGroupAsync(TafsilGroupDto dto);
        Task<clsResult> UpdateTafsilGroupAsync(TafsilGroupDto dto);
        Task<AccountTafsilDto> GetAccountTafsilAsync(int MoeinId);
        Task<clsResult> SetTafsilsAsync(AccountTafsilDto dto);
        Task<clsResult> DeleteTafsilGroupAsync(int id);

        //Tafsil
        string TafsilCodeGenerator(long? sellerId);
        Task<TafsilDto> FindTafsilAsync(long id);
        Task<List<TafsilDto>> TafsilsAsync(Int64? SellerId = null, string name = "");
        Task<SelectList> SelectList_UsageTafsilsAsync(Int64? SellerId = null);
        Task<SelectList> SelectList_UsageTafsils5Async(Int64? SellerId = null);
        Task<SelectList> SelectList_UsageTafsils6Async(Int64? SellerId = null);
        Task<clsResult> AddTafsilAsync(TafsilDto dto);
        Task<long> AddTafsilReturnIdAsync(TafsilDto dto);
        TafsilDto? AddTafsil(TafsilDto dto);
        Task<clsAddFasliResultDto> AutoAddTafsilAsync(AutoAddTafsilDto dto);
        Task<clsResult> EditTafsilAsync(TafsilDto dto);
        Task<SelectList> SelectList_TafsilsAsync(Int64? SellerId = null);
        Task<string?> GetTafsilCodByIdAsync(long tafsilId);
        Task<string?> GetTafsilNameByIdAsync(long? tafsilId);
        Task<clsResult> CreateAccountBankTafsilAsync(BankAccountTafsilDto dto);
        Task<List<TafsilListDto>> GetTafsilsByGroupAsync(int[] groupIds, long sellerId);
        Task<SelectList> SelectItems_TafsilsByGroupAsync(int[] groupIds, long sellerId);
        Task<MoeinSelectListTafsilDto> GetMoeinTafsilsAsync(int MoeinId, long sellerId);
        Task<clsResult> CreatePersonTafsilAsync(SetTafsilDto dto);
        Task<clsResult> CreatBulkPersonTafsilAsync(List<long> persenId, long sellerId);
        Task<long?> CheckAddTafsilAsync(string tafsilName, long sellerId);
        Task<clsResult> DeleteTafsilAccountAsync(long id);

        //Finance Periods
        Task<List<FinancePeriodsDto>> FinancePeriodsAsync(Int64 SellerId, string name = "");
        Task<FinancePeriodsDto> GetFinanceDtoAsync(int id);
        Task<clsResult> AddPeriodAsync(FinancePeriodsDto dto);
        Task<clsResult> EditPeriodAsync(FinancePeriodsDto dto);
        Task<clsResult> DeleteTheKolAsync(int id);
        Task<SelectList> SelectList_FinancePeriodAsync(Int64 SellerId);
        Task<bool> SetSellerPeriodAsync(string username, int periodId);
        //
        Task<bool> UpdateTafsilCodeAsync(long sellerId);

    }
}
