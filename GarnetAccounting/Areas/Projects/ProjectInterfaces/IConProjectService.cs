using GarnetAccounting.Areas.Accounting.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Projects.ProjectInterfaces
{
    public interface IConProjectService
    {
        Task<SelectList?> SelectList_ProjectsAsync(long sellerId);
        Task<List<ConProjectDto>> GetProjectsAsync(long sellerId);
        Task<ConProjectDto> GetProjectByIdAsync(int id);
        Task<clsResult> CreateProjectAsync(ConProjectDto dto);
        Task<clsResult> UpdateProjectAsync(ConProjectDto dto);
        Task<clsResult> DeleteProjectAsync(int id);
        Task<clsResult> ProjectActiveTogglerAsync(int id);
    }
}