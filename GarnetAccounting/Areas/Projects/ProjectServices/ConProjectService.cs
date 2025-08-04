using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Projects.Models.Entities;
using GarnetAccounting.Areas.Projects.ProjectInterfaces;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Projects.ProjectServices
{
    public class ConProjectService : IConProjectService
    {
        private readonly AppDbContext _db;

        public ConProjectService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<SelectList?> SelectList_ProjectsAsync(long sellerId)
        {
            var lst = await _db.Con_Projects.Where(n => n.SellerId == sellerId && n.IsActive)
                .Select(n => new { id = n.Id, name = n.ProjectName + n.ProjectNumber }).OrderBy(n => n.name).ToListAsync();
            if (lst.Count > 0)
                return new SelectList(lst, "id", "name");
            else return null;
        }
        public async Task<List<ConProjectDto>> GetProjectsAsync(long sellerId)
        {
            return await _db.Con_Projects.Include(n => n.Client)
                .Where(p => p.SellerId == sellerId)
                .Select(p => new ConProjectDto
                {
                    Id = p.Id,
                    SellerId = p.SellerId,
                    ProjectName = p.ProjectName,
                    ProjectNumber = p.ProjectNumber,
                    TafsilId = p.TafsilId,
                    ProjectStartDate = p.ProjectStartDate,
                    strDate = p.ProjectStartDate.HasValue ? p.ProjectStartDate.Value.LatinToPersian() : "",
                    ProjectAmount = p.ProjectAmount,
                    ContractDurationDays = p.ContractDurationDays,
                    CreatedBy = p.CreatedBy,
                    ClientId = p.ClientId,
                    ClientName = p.Client != null ? p.Client.Name : "کارفرمای پروژه را مشخص نکردید",
                    IsActive = p.IsActive,
                })
                .ToListAsync();
        }

        public async Task<ConProjectDto> GetProjectByIdAsync(int id)
        {
            var project = await _db.Con_Projects.Include(n => n.Client)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return null;

            ConProjectDto dto = new ConProjectDto();
            dto.Id = project.Id;
            dto.SellerId = project.SellerId;
            dto.ProjectName = project.ProjectName;
            dto.TafsilId = project.TafsilId;
            dto.ProjectStartDate = project.ProjectStartDate;
            dto.strDate = project.ProjectStartDate.HasValue ? project.ProjectStartDate.Value.LatinToPersian() : null;
            dto.ProjectAmount = project.ProjectAmount;
            dto.ContractDurationDays = project.ContractDurationDays;
            dto.CreatedBy = project.CreatedBy;
            dto.strDate = project.ProjectStartDate.HasValue ? project.ProjectStartDate.Value.LatinToPersian() : null;
            dto.ClientId = project.ClientId;
            dto.ClientName = project.Client != null ? project.Client.Name : "کارفرما مشخص نشده است ";
            dto.IsActive = project.IsActive;
            return dto;
        }

        public async Task<clsResult> CreateProjectAsync(ConProjectDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (await _db.Con_Projects.AnyAsync(p => p.ProjectName == dto.ProjectName))
            {
                result.Message = "نام پروژه تکراری است";
                return result;
            }

            var project = new Con_Project();

            project.SellerId = dto.SellerId;
            project.ProjectName = dto.ProjectName;
            project.ProjectNumber = dto.ProjectNumber;
            project.TafsilId = dto.TafsilId;
            project.ProjectStartDate = dto.ProjectStartDate;
            project.ProjectAmount = dto.ProjectAmount;
            project.ContractDurationDays = dto.ContractDurationDays;
            project.CreatedBy = dto.CreatedBy;
            project.ClientId = dto.ClientId;
            project.IsActive = dto.IsActive;

            _db.Con_Projects.Add(project);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "پروژه با موفقیت ایجاد شد";
            }
            catch (Exception ex)
            {
                result.Message = $"خطا در ایجاد پروژه: {ex.Message}";
            }

            return result;
        }

        public async Task<clsResult> UpdateProjectAsync(ConProjectDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            var project = await _db.Con_Projects.FindAsync(dto.Id);
            if (project == null)
            {
                result.Message = "پروژه مورد نظر یافت نشد";
                return result;
            }

            if (await _db.Con_Projects.AnyAsync(p => p.Id != dto.Id && p.ProjectName == dto.ProjectName))
            {
                result.Message = "نام پروژه تکراری است";
                return result;
            }

            project.ProjectName = dto.ProjectName;
            project.ProjectNumber = dto.ProjectNumber;
            project.TafsilId = dto.TafsilId;
            project.ProjectStartDate = dto.ProjectStartDate;
            project.ProjectAmount = dto.ProjectAmount;
            project.ContractDurationDays = dto.ContractDurationDays;
            project.IsActive = dto.IsActive;
            project.ClientId = dto.ClientId;

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "پروژه با موفقیت بروزرسانی شد";
            }
            catch (Exception ex)
            {
                result.Message = $"خطا در بروزرسانی پروژه: {ex.Message}";
            }

            return result;
        }

        public async Task<clsResult> DeleteProjectAsync(int id)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            var project = await _db.Con_Projects.FindAsync(id);
            if (project == null)
            {
                result.Message = "پروژه مورد نظر یافت نشد";
                return result;
            }

            _db.Con_Projects.Remove(project);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "پروژه با موفقیت حذف شد";
            }
            catch (Exception ex)
            {
                result.Message = $"خطا در حذف پروژه: {ex.Message}";
            }

            return result;
        }

        public async Task<clsResult> ProjectActiveTogglerAsync(int id)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            var project = await _db.Con_Projects.FindAsync(id);
            if (project == null)
            {
                result.Message = "پروژه مورد نظر یافت نشد";
                return result;
            }
            project.IsActive = !project.IsActive;
            _db.Con_Projects.Update(project);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "وضعیت پروژه با موقفیت بروزشد";
            }
            catch (Exception ex)
            {
                result.Message = $"خطا در بروزرسانی وضعیت پروژه: {ex.Message}";
            }

            return result;
        }
    }
}