using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class CuRepresentativeService : ICuRepresentativeService
    {
        private readonly AppDbContext _db;
        public CuRepresentativeService(AppDbContext dbcontext)
        {
            _db = dbcontext;
        }

        //On Old System

        public async Task<SelectList> SelectList_OldSys_RepresentativeAsync(long sellerId)
        {
            var representatives = await _db.KPOldSystemSales.Where(n => n.SellerId == sellerId)
                .Select(m => new { id = m.DestinationRepresentative, name = m.DestinationRepresentative }).Distinct().OrderBy(n => n.name).ToListAsync();

            return new SelectList(representatives, "id", "name");
        }

        public IQueryable<RepresentativeRate> OldSys_RepresentativeRates(RepFilterDto filter)
        {
            var query = _db.KPOldSystemSales.Where(n => n.SellerId == filter.sellerId).AsQueryable();

            if (!string.IsNullOrEmpty(filter.name))
                query = query.Where(n => n.DestinationRepresentative == filter.name);

            if (!string.IsNullOrEmpty(filter.Destination))
                query = query.Where(n => n.ToDestination == filter.Destination);

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime startDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.MiladiDate >= startDate);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime endDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.MiladiDate <= endDate);
            }
            if (!string.IsNullOrEmpty(filter.PaymentMethod))
                query = query.Where(n => n.PaymentMethod == filter.PaymentMethod);

            var data = query.GroupBy(n => n.DestinationRepresentative)
                .Select(n => new RepresentativeRate
                {
                    Name = n.Key,
                    City = n.Max(a => a.ToDestination),
                    LastDayOfWork = n.Max(s => s.MiladiDate),
                    Qty = n.Count(),
                    TotalBill = n.Sum(s => s.TotalBillOfLadingAmount.Value),
                    RepresentativeShare = n.Sum(s => s.DistributionOrSeparationFee.Value + s.DestinationMiscellaneousFee.Value),
                    CashOnDelivery = n.Where(d => d.PaymentMethod == "پسکرایه").Sum(s => s.TotalBillOfLadingAmount),
                    PaidAmounts = 0,

                }).AsQueryable();

            return data;
        }



        //=========================================================================================

        public async Task<SelectList> SelectList_RepresentativeAsync(long sellerId)
        {
            var representatives = await _db.Representatives.Where(n => n.SellerId == sellerId)
                .Select(m => new { id = m.Id, name = m.Title }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(representatives, "id", "name");
        }
        public async Task<SelectList> SelectList_RepresentativeNameAsync(long sellerId)
        {
            var representatives = await _db.Representatives.Where(n => n.SellerId == sellerId)
                .Select(m => new { id = m.Id, name = m.Title }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(representatives, "name", "name");
        }
        public async Task<List<RepresentativeDto>> GetRepresentativesAsync(long sellerId)
        {
            var data = await _db.Representatives.Where(n => n.SellerId == sellerId)
                .Select(n => new RepresentativeDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    Title = n.Title,
                    PartyId = n.PartyId,
                    Description = n.Description,
                    AdditionalInfo = n.AdditionalInfo,
                    TafsilId = n.TafsilId,
                }).OrderBy(n => n.Title).ToListAsync();

            return data;
        }
        public async Task<clsResult> AddRepresentativeAsync(RepresentativeDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            if (dto == null || string.IsNullOrEmpty(dto.Id.ToString()))
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }
            if (await _db.Representatives.AnyAsync(n => n.Title == dto.Title))
            {
                result.Message = "نام نماینده تکراری است.";
                return result;
            }

            Cu_Representative representative = new Cu_Representative();
            representative.Id = dto.Id;
            representative.Title = dto.Title;
            representative.SellerId = dto.SellerId;
            representative.PartyId = dto.PartyId;
            representative.AdditionalInfo = dto.AdditionalInfo;
            representative.Description = dto.Description;
            representative.TafsilId = dto.TafsilId;

            _db.Representatives.Add(representative);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "نماینده جدید با موفقیت ثبت شد.";
                return result;
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ثبت اطلاعات رخ داده است.";
                result.Message += "\n " + x.Message;
                return result;
            }
        }
        public async Task<clsResult> UpdateRepresentativeAsync(RepresentativeDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            if (dto == null || string.IsNullOrEmpty(dto.Id.ToString()))
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }
            if (await _db.Representatives.AnyAsync(n => n.Id != dto.Id && n.Title == dto.Title))
            {
                result.Message = "نام نماینده تکراری است.";
                return result;
            }

            Cu_Representative representative = await _db.Representatives.FindAsync(dto.Id);
            if (representative == null)
            {
                if (await _db.Representatives.AnyAsync(n => n.Id != dto.Id && n.Title == dto.Title))
                {
                    result.Message = "اطلاعات نماینده یافت نشد";
                    return result;
                }
            }
            representative.Title = dto.Title;
            representative.SellerId = dto.SellerId;
            representative.PartyId = dto.PartyId;
            representative.AdditionalInfo = dto.AdditionalInfo;
            representative.Description = dto.Description;
            representative.TafsilId = dto.TafsilId;

            _db.Representatives.Update(representative);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات نماینده با موفقیت بروزسانی شد";
                return result;
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ثبت اطلاعات رخ داده است.";
                result.Message += "\n " + x.Message;
                return result;
            }

        }
        public async Task<clsResult> DeleteRepresentativeAsync(Guid id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            Cu_Representative representative = await _db.Representatives.FindAsync(id);
            if (representative == null)
            {
                result.Message = "اطلاعات نماینده یافت نشد";
                return result;
            }

            _db.Representatives.Remove(representative);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "عمیات حذف نماینده با موفقیت انجام شد";
                return result;
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان حذف اطلاعات رخ داده است. احتمالا نماینده موردنظر در سیستم دارای سوابقی می باشد..";
                result.Message += "\n " + x.Message;
                return result;
            }
        }

    }
}
