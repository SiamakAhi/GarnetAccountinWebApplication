using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class BillofladingService : IBillofladingService
    {
        private readonly AppDbContext _db;
        public BillofladingService(AppDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<string> GenerateBillNumberAsync(long sellerId, string branchCode)
        {
            string code = "";
            int billCount = await _db.Cu_BillOfLadings.Where(n => n.SellerId == sellerId).CountAsync();
            string sequencNumber = (billCount + 1).ToString("D6");
            code = branchCode + sequencNumber;
            return code;
        }

        public async Task<clsResult> CreateNewBillOfLadingAsync(BillOfLadingDto_Header dto)
        {

            if (dto == null)
            {
                return new clsResult { Message = "اطلاعاتی جهت صدور دریافت نشد", ShowMessage = true, Success = false };
            }
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            Cu_BillOfLading bill = new Cu_BillOfLading();
            bill.Id = dto.Id;
            bill.SellerId = dto.SellerId;
            bill.WaybillNumber = dto.WaybillNumber;
            bill.IssuanceDate = DateTime.Now;

            bill.RouteId = dto.RouteId;
            bill.ServiceId = dto.ServiceId;
            bill.OriginBranchId = dto.OriginBranchId;
            bill.OriginHubId = dto.OriginHubId;

            bill.SenderId = dto.SenderId;
            bill.SenderAddress = dto.SenderAddress;
            bill.ReceiverId = dto.ReceiverId;
            bill.ReceiverAddress = dto.ReceiverAddress;
            bill.Description = dto.Description;

            bill.BillOfLadingStatusId = dto.BillOfLadingStatusId;
            bill.LastStatusDescription = dto.LastStatusDescription;
            bill.CreatedBy = dto.CreatedBy;

            try
            {
                _db.Cu_BillOfLadings.Add(bill);
                await _db.SaveChangesAsync();

                result.Success = true;
                result.Message = "بارنامه با موفقیت ایجاد شد";
            }
            catch (Exception x)
            {
                result.Message = "در عملیات صدور بارنامه خطایی رخ داده است";

            }
            return result;
        }

        public async Task<BillOfLadingDto> GetBillOfLadingDtoAsync(Guid id)
        {
            // 1. بارنامه اصلی را از دیتابیس بخوانید
            var bill = await _db.Cu_BillOfLadings
                .Include(b => b.Consignments)
                .Include(b => b.IssuingBranch)
                .Include(b => b.Route).ThenInclude(n => n.DestinationCity)
                .Include(b => b.Route).ThenInclude(n => n.OriginCity)
                .Include(b => b.Sender)
                .Include(b => b.Receiver)
                .Include(b => b.Service)
                .SingleOrDefaultAsync(n => n.Id == id);

            if (bill == null)
            {
                throw new InvalidOperationException("بارنامه با شناسه مورد نظر یافت نشد.");
            }

            // 2. تبدیل اطلاعات بارنامه به BillOfLadingDto_Header
            var billHeader = new BillOfLadingDto_Header
            {
                Id = bill.Id,
                SellerId = bill.SellerId,
                WaybillNumber = bill.WaybillNumber,
                IssuanceDate = bill.IssuanceDate,
                RouteId = bill.RouteId,
                OriginCity = bill.Route.OriginCity.PersianName,
                DestinationCity = bill.Route.DestinationCity.PersianName,
                ServiceId = bill.ServiceId,
                ServiceName = bill.Service.ServiceName,
                OriginBranchId = bill.OriginBranchId,
                OriginBranchName = bill.IssuingBranch.BranchName,
                SenderId = bill.SenderId,
                SenderAddress = bill.SenderAddress,
                SenderName = bill.Sender.Name,
                SenderPhone = bill.Sender.MobilePhone,
                ReceiverId = bill.ReceiverId,
                ReceiverAddress = bill.ReceiverAddress,
                ReciverName = bill.Receiver.Name,
                ReciverPhone = bill.Receiver.MobilePhone,
                Description = bill.Description,
                DeliveredCount = bill.DeliveredCount,
                SettelmentType = bill.SettelmentType,
                LastStatusDescription = bill.LastStatusDescription,
                BillOfLadingStatusId = bill.BillOfLadingStatusId,
                OriginHubId = bill.OriginHubId,
                DestinationHubId = bill.DestinationHubId,
                CreatedBy = bill.CreatedBy,
                UpdatedDate = bill.UpdatedDate,
                UpdatedBy = bill.UpdatedBy,
                IsDeleted = bill.IsDeleted
            };

            // 3. تبدیل محموله‌ها به لیست ConsigmentDto
            var consignments = bill.Consignments?.Select(c => new ConsigmentDto
            {
                Id = c.Id,
                SellerId = c.SellerId,
                Code = c.Code,
                Weight = c.Weight,
                Height = c.Height,
                Width = c.Width,
                Length = c.Length,
                Volume = c.Volume,
                ContentDescription = c.ContentDescription,
                Value = c.Value,
                ServiceInformation = c.ServiceInformation,
                IsDelivered = c.IsDelivered,
                RecipientName = c.RecipientName,
                DeliveryDate = c.DeliveryDate,
                ReceiverSignature = c.ReceiverSignature,
                CargoFare = c.CargoFare,
                TotalCostPrice = c.TotalCostPrice,
                Discount = c.Discount,
                VatRate = c.VatRate,
                VatPrice = c.VatPrice,
                TotalPrice = c.TotalPrice,
                BillOfLadingId = c.BillOfLadingId,
                NatureTypeId = c.NatureTypeId
            }).ToList();

            // 4. ایجاد و بازگشت BillOfLadingDto
            return new BillOfLadingDto
            {
                bill = billHeader,
                Consigments = consignments
            };
        }

        public IQueryable<ViewBillOfLadings> GetBillsAsQuery(BillOfLadingFilterDto filter)
        {

            var query = _db.Cu_BillOfLadings
                .Include(n => n.Consignments)
                .Include(n => n.IssuingBranch)
                .Include(n => n.OriginHub)
                .Include(n => n.DestinationHub)
                .Include(n => n.Route).ThenInclude(n => n.OriginCity)
                .Include(n => n.Route).ThenInclude(n => n.DestinationCity)
                .Include(n => n.Service)
                .Include(n => n.Sender)
                .Include(n => n.Receiver)
                .Include(n => n.BillOfLadingStatus)
                .Where(n => n.SellerId == filter.SellerId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.BiilOdLadingNumber)) // فیلتر بر اساس بحشی از شماره بارنامه
                query = query.Where(n => n.WaybillNumber.Contains(filter.BiilOdLadingNumber));
            if (filter.OriginBranchId.HasValue)  // بر اساس شعبه صادرکننده
                query = query.Where(n => n.OriginBranchId == filter.OriginBranchId);
            if (filter.RoutId.HasValue) // بر اساس مسیر
                query = query.Where(n => n.RouteId == filter.RoutId);
            if (filter.OriginCityId.HasValue)  // بر ساس مبدأ
                query = query.Where(n => n.Route.OriginCityId == filter.OriginCityId.Value);
            if (filter.DestinationCityId.HasValue)   // مقصد
                query = query.Where(n => n.Route.DestinationCityId == filter.DestinationCityId.Value);
            if (!string.IsNullOrEmpty(filter.IssuerUserName))  // کاربر صادرکننده
                query = query.Where(n => n.CreatedBy == filter.IssuerUserName);

            if (!string.IsNullOrEmpty(filter.strFromDate))
            {
                DateTime date = filter.strFromDate.PersianToLatin();
                query = query.Where(n => n.IssuanceDate >= date);
            }
            if (!string.IsNullOrEmpty(filter.strUntilDate))
            {
                DateTime date = filter.strUntilDate.PersianToLatin();
                query = query.Where(n => n.IssuanceDate <= date);
            }

            if (filter.BillStatus?.Length > 0)
                query = query.Where(n => filter.BillStatus.Contains(n.BillOfLadingStatusId));

            var result = query.Select(n => new ViewBillOfLadings
            {
                Id = n.Id,
                SellerId = n.SellerId,

                WaybillNumber = n.WaybillNumber,
                IssuanceDate = n.IssuanceDate,
                IssuanceTime = n.IssuanceTime,
                OriginBranchName = n.IssuingBranch.BranchName,
                OriginCity = n.Route.OriginCity.PersianName,
                DestinationCity = n.Route.DestinationCity.PersianName,
                ServiceName = n.Service.ServiceName,
                SenderId = n.SenderId,
                SenderName = n.Sender.Name,
                SenderAddress = n.SenderAddress,
                ReceiverId = n.ReceiverId,
                ReceiverName = n.Receiver.Name,
                ReceiverAddress = n.ReceiverAddress,
                OriginHubId = n.OriginHubId,
                OriginHubName = n.OriginHub.HubName,
                Description = n.Description,
                ConsigmentCount = n.Consignments.Count,

                BillOfLadingStatusId = n.BillOfLadingStatusId,
                LastStatusDescription = n.BillOfLadingStatus.Name,

                CreatedBy = n.CreatedBy,
                UpdatedBy = n.UpdatedBy,
                UpdatedDate = n.UpdatedDate,
                IsDeleted = n.IsDeleted

            }).OrderBy(n => n.IssuanceDate).AsQueryable();

            return result;
        }
    }
}
