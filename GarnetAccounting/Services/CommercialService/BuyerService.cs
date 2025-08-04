using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Models;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Services.CommercialService
{


    public class BuyerService : IBuyerService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BuyerService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<SelectList> SelectList_PartyType()
        {
            var lst = await _db.PartyTypes.Select(n => new { id = n.Id, name = n.Name }).ToListAsync();
            return new SelectList(lst, "id", "name");
        }
        public async Task<ResultDto> AddBuyer(BuyerAddDto dto)
        {
            ResultDto result = new ResultDto();
            result.Success = false;
            result.Message = "خطایی در ثبت اطلاعات فروشنده رخ داده است";
            //..
            Party buyer = new Party();

            buyer.CustomerId = dto.CustomerId;
            buyer.uyer_SellerId = dto.SellerId;
            buyer.Role = (Int16)PartyRole.Buyer;

            buyer.LegalStatus = dto.LegalStatus;
            buyer.Name = dto.Name;
            buyer.NationalId = dto.NationalId;
            buyer.EconomicCode = dto.EconomicCode;
            buyer.PostalCode = dto.PostalCode;
            buyer.RegistrationNumber = dto.RegistrationNumber;

            buyer.Province = dto.Province;
            buyer.City = dto.City;
            buyer.Address = dto.Address;
            buyer.Fax = dto.Fax;
            buyer.MobilePhone = dto.MobilePhone;

            buyer.AccountingSystemId = dto.AccountingSystemId;
            buyer.InvoiceDescription = dto.InvoiceDescription;
            buyer.IsActive = true;

            _db.parties.Add(buyer);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "مشتـری جدید با موفقیت ثبت شد";
                }
            }
            catch
            {
            }

            return result;

        }

        public async Task<long> AddBuyerAsync(BuyerAddDto dto)
        {

            Party buyer = new Party();

            buyer.CustomerId = dto.CustomerId;
            buyer.uyer_SellerId = dto.SellerId;
            buyer.Role = (Int16)PartyRole.Buyer;

            buyer.LegalStatus = dto.LegalStatus;
            buyer.Name = dto.Name;
            buyer.NationalId = dto.NationalId;
            buyer.EconomicCode = dto.EconomicCode;
            buyer.PostalCode = dto.PostalCode;
            buyer.RegistrationNumber = dto.RegistrationNumber;

            buyer.Province = dto.Province;
            buyer.City = dto.City;
            buyer.Address = dto.Address;
            buyer.Fax = dto.Fax;
            buyer.MobilePhone = dto.MobilePhone;

            buyer.AccountingSystemId = dto.AccountingSystemId;
            buyer.InvoiceDescription = dto.InvoiceDescription;
            buyer.IsActive = true;

            _db.parties.Add(buyer);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    return buyer.Id;
                }
            }
            catch
            {
                return 0;
            }

            return 0;

        }
        public async Task<ResultDto> AddBulkBuyer(List<BuyerAddDto> listDto)
        {
            ResultDto result = new ResultDto();
            result.Success = false;
            result.Message = "خطایی در ثبت اطلاعات فروشنده رخ داده است";
            //..
            int customerId = (await _db.parties.FindAsync(listDto.FirstOrDefault().SellerId)).CustomerId;

            List<Party> newBuyers = new List<Party>();
            foreach (var dto in listDto)
            {
                Party buyer = new Party();
                buyer.CustomerId = customerId;
                buyer.uyer_SellerId = dto.SellerId;
                buyer.Role = (Int16)PartyRole.Buyer;
                buyer.LegalStatus = dto.LegalStatus;
                buyer.Name = dto.Name;
                buyer.Title = dto.Name;
                buyer.NationalId = dto.NationalId;
                buyer.EconomicCode = dto.EconomicCode;
                buyer.PostalCode = dto.PostalCode;
                buyer.RegistrationNumber = dto.RegistrationNumber;
                buyer.Province = dto.Province;
                buyer.City = dto.City;
                buyer.Address = dto.Address;
                buyer.Fax = dto.Fax;
                buyer.MobilePhone = dto.MobilePhone;
                buyer.AccountingSystemId = dto.AccountingSystemId;
                buyer.InvoiceDescription = dto.InvoiceDescription;
                buyer.IsActive = true;
                buyer.LegalStatus = dto.LegalStatus;

                newBuyers.Add(buyer);
            }


            _db.parties.AddRange(newBuyers);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "لیست مشتریان (خریداران) با موفقیت ثبت شد";
                }
            }
            catch
            {
            }

            return result;

        }
        public async Task<ResultDto> UpdateBuyerAsync(BuyerUpdateDto dto)
        {
            ResultDto result = new ResultDto();
            result.Success = false;
            result.Message = "خطایی در ثبت اطلاعات مشتری رخ داده است";
            //..
            Party buyer = _db.parties.Find(dto.Id);
            if (buyer == null) { return result; }


            buyer.CustomerId = dto.CustomerId;
            buyer.Role = (Int16)PartyRole.Buyer;

            buyer.LegalStatus = dto.LegalStatus;
            buyer.Name = dto.Name;
            buyer.NationalId = dto.NationalId;
            buyer.EconomicCode = dto.EconomicCode;
            buyer.PostalCode = dto.PostalCode;
            buyer.RegistrationNumber = dto.RegistrationNumber;

            buyer.Province = dto.Province;
            buyer.City = dto.City;
            buyer.Address = dto.Address;
            buyer.Fax = dto.Fax;
            buyer.MobilePhone = dto.MobilePhone;

            buyer.AccountingSystemId = dto.AccountingSystemId;
            buyer.InvoiceDescription = dto.InvoiceDescription;
            buyer.IsActive = true;

            _db.parties.Update(buyer);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "اطلاعت مشتری با موفقیت بروز شد";
                }
            }
            catch
            {
            }

            return result;

        }

        public async Task<BuyerUpdateDto> GetBuyerByIdAsync(Int64 id)
        {
            BuyerUpdateDto buyer = new BuyerUpdateDto();

            var dto = await _db.parties.SingleOrDefaultAsync(n => n.Id == id);

            buyer.Id = dto.Id;
            buyer.CustomerId = dto.CustomerId;
            buyer.LegalStatus = dto.LegalStatus;
            buyer.Name = dto.Name;
            buyer.NationalId = dto.NationalId;
            buyer.EconomicCode = dto.EconomicCode;
            buyer.PostalCode = dto.PostalCode;
            buyer.RegistrationNumber = dto.RegistrationNumber;

            buyer.Province = dto.Province;
            buyer.City = dto.City;
            buyer.Address = dto.Address;
            buyer.Fax = dto.Fax;
            buyer.MobilePhone = dto.MobilePhone;

            buyer.AccountingSystemId = dto.AccountingSystemId;
            buyer.InvoiceDescription = dto.InvoiceDescription;

            return buyer;
        }

        public IQueryable<VmBuyer> GetBuyers(Int64 SellerId, int customerId, string name = "", string NationalCode = "")
        {
            var buyers = _db.parties.Where(n =>
                                          (n.CustomerId == customerId && n.Role == 2)
                                          && (n.uyer_SellerId == SellerId)
                                          && (n.Name.Contains(name))
                                          && (n.NationalId.StartsWith(NationalCode))
                                         ).Select(n => new VmBuyer
                                         {
                                             Id = n.Id,
                                             customerId = n.CustomerId,
                                             Name = n.Name,
                                             NotionalIdentity = n.NationalId,
                                             EconimicCode = n.EconomicCode,
                                             legalStatus = n.LegalStatus,
                                             Province = n.Province,
                                             City = n.City,
                                             phone = n.MobilePhone,

                                         }).AsQueryable();
            return buyers;


        }

        public async Task<SelectList> SelectList_Buyers(Int64 sellerId)
        {
            var data = await _db.parties
                .Where(n => n.Role == 2 && n.uyer_SellerId == sellerId)
                .Select(n => new { id = n.Id, name = n.Name })
                .ToListAsync();

            return new SelectList(data, "id", "name");

        }

        public async Task<bool> IsDupplicateNationalId(Int64 SellerId, string code)
        {
            return await _db.parties.Where(n => n.uyer_SellerId == SellerId).AnyAsync(n => n.NationalId == code);
        }

        public async Task<ResultDto> DeleteBuyerAsync(Int64 id)
        {
            var party = await _db.parties.FindAsync(id);
            if (party == null)
            {
                return new ResultDto() { Success = false, Message = "موردی با این شناسه یافت نشد" };
            }

            _db.parties.Remove(party);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new ResultDto() { Success = true, Message = "ردیف موردنظر با موفقیت از بانک اطلاعاتی حذف شد" };
            }
            return new ResultDto() { Success = false, Message = "خطایی حین عملیات رخ داده است." };
        }
    }
}
