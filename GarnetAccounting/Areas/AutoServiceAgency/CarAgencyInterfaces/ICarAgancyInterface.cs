using GarnetAccounting.Areas.AutoServiceAgency.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.AutoServiceAgency.CarAgencyInterfaces
{
    public interface ICarAgancyInterface
    {
        // Contractors
        Task<SelectList> SelectList_ContractorsAsync(long SellerId);
        Task<List<ContractorDto>> GetContractorsAsync(long SellerId);
        Task<ContractorDto> GetContractorByIdAsync(int id);
        Task<clsResult> AddContractorAsync(ContractorDto dto);
        Task<clsResult> UpdateContractorAsync(ContractorDto dto);


        // Services
        Task<SelectList> SelectList_ServicesAsync(long SellerId);
        Task<List<ServiceDto>> GetServicesAsync(long SellerId);
        Task<ServiceDto> GetServiceByIdAsync(int id);
        Task<clsResult> AddServiceAsync(ServiceDto dto);
        Task<clsResult> UpdateServiceAsync(ServiceDto dto);


        //Setting
        Task<ReceptionAccountingSettingDto> GetOrCreateSettingsAsync(long sellerId);
        Task<clsResult> UpdateSettingsAsync(ReceptionAccountingSettingDto dto);

        //Reception methods
        Task<clsResult> CarRec_ImportReception_ModiranAsync(ImpodtReceptionDto dto);
        Task<clsResult> CarRec_ImportReception_LamariAsync(ImpodtReceptionDto dto);
        Task<List<ReciptionDto>> CarRec_GetReceptionInfosAsync(ReciptionFilterDto filter);
        Task<List<ReceptionHeaderDto>> CarRec_GetReceptionHeaderAsync(ReciptionFilterDto filter);
        Task<List<ReciptionDto>> GetReceptionItemsByNumbersAsync(long sellerId, string ReceptionNumber);
        Task<clsResult> UpdateReceptionDetailsAsync(List<ReciptionDto> items);
        Task<clsResult> UpdateLamariReceptionAsync(List<ReciptionDto> items);

        Task<SaveDetailsDto> GetSaveDetailsDtoAsync(long sellerId, string ReceptionNumber);

        // Lamari Services
        Task<SelectList> SelectList_LamariServicesAsync(long SellerId);
        Task<List<LamariServiceDto>> GetLamariServicesAsync(long SellerId);
        Task<LamariServiceDto> GetLamariServiceByIdAsync(int id);
        Task<clsResult> AddLamriServiceAsync(LamariServiceDto dto);
        Task<clsResult> UpdateLamariServiceAsync(LamariServiceDto dto);

    }
}
