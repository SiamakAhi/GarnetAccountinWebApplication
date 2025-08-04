using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Models;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GarnetAccounting.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        private readonly IAccCodingService _coding;
        private readonly UserContextService _userContext;

        public PersonService(AppDbContext db, IWebHostEnvironment env, IAccCodingService CodingService, UserContextService userContext)
        {
            _db = db;
            _env = env;
            _coding = CodingService;
            _userContext = userContext;
        }
        public async Task<List<PersonDto>> PersenAsync(long Seller, string? name = null, string? phone = null, short? legal = null, string? nationalId = null, string? tafsilCode = null, bool? vendor = null, bool? customer = null)
        {
            List<PersonDto> persen = new List<PersonDto>();

            var query = _db.parties.Where(x => x.uyer_SellerId == Seller).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(n => n.Name.Contains(name));

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(n => n.MobilePhone.Contains(phone));

            if (!string.IsNullOrEmpty(nationalId))
                query = query.Where(n => n.NationalId == nationalId);

            if (!string.IsNullOrEmpty(tafsilCode))
                query = query.Where(n => n.TafsilCode == tafsilCode);
            if (legal != null)
                query = query.Where(n => n.LegalStatus == legal.Value);

            if (vendor.HasValue && vendor.Value)
                query = query.Where(n => n.IsVendor == true);
            if (customer.HasValue && customer.Value)
                query = query.Where(n => n.IsCustomer == true);

            persen = await query.Select(n => new PersonDto
            {
                Id = n.Id,
                CustomerId = n.CustomerId,
                SellerId = n.uyer_SellerId.HasValue ? n.uyer_SellerId.Value : 0,
                LegalStatus = n.LegalStatus,
                Name = n.Name,
                NationalId = n.NationalId,
                EconomicCode = n.EconomicCode,
                MobilePhone = n.MobilePhone,
                Fax = n.Fax,
                Province = n.Province,
                City = n.City,
                Address = n.Address,
                IsCustomer = n.IsCustomer,
                IsVendor = n.IsVendor,
                TafsilCode = n.TafsilCode,
                TafsilId = n.TafsilId,
            }).OrderBy(n => n.Name).ToListAsync();

            return persen;
        }
        public IQueryable<PersonDto> PersenAsQuery(PersonFilterDto filter)
        {


            var query = _db.parties.Where(x => x.uyer_SellerId == filter.SellerId).AsQueryable();

            if (!string.IsNullOrEmpty(filter.name))
                query = query.Where(n => n.Name.Contains(filter.name));

            if (!string.IsNullOrEmpty(filter.phone))
                query = query.Where(n => n.MobilePhone.Contains(filter.phone));

            if (!string.IsNullOrEmpty(filter.nationalId))
                query = query.Where(n => n.NationalId == filter.nationalId);

            if (!string.IsNullOrEmpty(filter.tafsilCode))
                query = query.Where(n => n.TafsilCode == filter.tafsilCode);
            if (filter.legal.HasValue)
                query = query.Where(n => n.LegalStatus == filter.legal.Value);

            if (filter.isVendor.HasValue && filter.isVendor.Value)
                query = query.Where(n => n.IsVendor == true);
            if (filter.isCustomer.HasValue && filter.isCustomer.Value)
                query = query.Where(n => n.IsCustomer == true);

            var persen = query.Select(n => new PersonDto
            {
                Id = n.Id,
                CustomerId = n.CustomerId,
                SellerId = n.uyer_SellerId.HasValue ? n.uyer_SellerId.Value : 0,
                LegalStatus = n.LegalStatus,
                Name = n.Name,
                NationalId = n.NationalId,
                EconomicCode = n.EconomicCode,
                MobilePhone = n.MobilePhone,
                Fax = n.Fax,
                Province = n.Province,
                City = n.City,
                Address = n.Address,
                IsCustomer = n.IsCustomer,
                IsVendor = n.IsVendor,
                TafsilCode = n.TafsilCode,
                TafsilId = n.TafsilId,
            })
                .OrderBy(n => n.Name)
                .AsQueryable();

            return persen;
        }
        public async Task<SelectList> SelectList_PartyTypeAsync()
        {
            var lst = await _db.PartyTypes.Select(n => new { id = n.Id, name = n.Name }).ToListAsync();
            return new SelectList(lst, "id", "name");
        }
        public async Task<PersonDto> GetPersonDtoAsync(long Id)
        {
            PersonDto dto = new PersonDto();
            var person = await _db.parties.SingleOrDefaultAsync(n => n.Id == Id);
            if (person == null) return null;

            dto.Id = person.Id;
            dto.SellerId = person.uyer_SellerId;
            dto.CustomerId = person.CustomerId;
            dto.LegalStatus = person.LegalStatus;
            dto.Name = person.Name;
            dto.Address = person.Address;
            dto.City = person.City;
            dto.Province = person.Province;
            dto.PostalCode = person.PostalCode;
            dto.MobilePhone = person.MobilePhone;
            dto.NationalId = person.NationalId;
            dto.EconomicCode = person.EconomicCode;
            dto.TafsilCode = person.TafsilCode;
            dto.TafsilId = person.TafsilId;
            dto.IsVendor = person.IsVendor;
            dto.IsCustomer = person.IsCustomer;
            return dto;
        }
        public async Task<clsResult> AddPersonAsync(PersonDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            bool isDuplicate = await _db.parties.Where(n => n.Name == dto.Name && n.uyer_SellerId == dto.SellerId).AnyAsync();
            if (isDuplicate)
            {
                result.Message = "نام شخص تکراری است";
                return result;
            }
            //..
            Party person = new Party();

            person.CustomerId = dto.CustomerId;
            person.uyer_SellerId = dto.SellerId;
            person.Role = (Int16)PartyRole.Buyer;

            person.LegalStatus = dto.LegalStatus;
            person.Name = dto.Name;
            person.NationalId = dto.NationalId;
            person.EconomicCode = dto.EconomicCode;
            person.PostalCode = dto.PostalCode;
            person.RegistrationNumber = dto.RegistrationNumber;
            person.Province = dto.Province;
            person.City = dto.City;
            person.Address = dto.Address;
            person.Fax = dto.Fax;
            person.MobilePhone = dto.MobilePhone;
            person.IsActive = true;
            person.Title = dto.Name;
            person.IsCustomer = dto.IsCustomer;
            person.IsVendor = dto.IsVendor;
            _db.parties.Add(person);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "اطلاعات شخص با موفقیت ثبت شد";

                    TafsilDto tafsil = new TafsilDto();
                    tafsil.SellerId = dto.SellerId;
                    tafsil.intGroupsId = new int[] { 1 };
                    tafsil.strGroupsId = JsonConvert.SerializeObject(tafsil.intGroupsId);
                    tafsil.IsPerson = true;
                    tafsil.Name = dto.Name;

                    tafsil = _coding.AddTafsil(tafsil);
                    if (tafsil != null)
                    {
                        var updatePerson = _db.parties.Find(person.Id);
                        updatePerson.TafsilCode = tafsil.Code;
                        updatePerson.TafsilId = tafsil.Id;
                        _db.parties.Update(updatePerson);
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n " + ex.Message;
            }

            return result;

        }
        public async Task<clsResult> UpdatePersonAsync(PersonDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            //..
            Party person = await _db.parties.FindAsync(dto.Id);
            if (person == null)
            {
                result.Message = "اطلاعات شخص موردنظر یافت نشد";
                result.ShowMessage = true;
                return result;
            }

            person.CustomerId = dto.CustomerId;
            person.uyer_SellerId = dto.SellerId;
            person.Role = (Int16)PartyRole.Buyer;

            person.LegalStatus = dto.LegalStatus;
            person.Name = dto.Name;
            person.NationalId = dto.NationalId;
            person.EconomicCode = dto.EconomicCode;
            person.PostalCode = dto.PostalCode;
            person.RegistrationNumber = dto.RegistrationNumber;
            person.Province = dto.Province;
            person.City = dto.City;
            person.Address = dto.Address;
            person.Fax = dto.Fax;
            person.MobilePhone = dto.MobilePhone;
            person.IsCustomer = dto.IsCustomer;
            person.IsVendor = dto.IsVendor;
            person.IsActive = true;

            _db.parties.Update(person);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "ویرایش اطلاعات شخص با موفقیت انجام شد";

                    if (person.TafsilId != null)
                    {
                        var tafsil = _db.Acc_Coding_Tafsils.Find(person.TafsilId);
                        if (tafsil != null)
                        {
                            if (person.Name != tafsil.Name)
                            {
                                tafsil.Name = person.Name;
                                _db.Acc_Coding_Tafsils.Update(tafsil);
                                _db.SaveChanges();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n " + ex.Message;
            }

            return result;

        }
        public async Task<clsResult> SetPersonTafsilAsync(long personId, long tafsilId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            Party? x = await _db.parties.FindAsync(personId);
            if (x == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            var tafsil = await _coding.FindTafsilAsync(tafsilId);
            x.TafsilCode = tafsil.Code;
            x.TafsilId = tafsilId;


            _db.parties.Update(x);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<clsResult> RemoveTafsilFromPersonAsync(long personId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            Party? x = await _db.parties.FindAsync(personId);
            if (x == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            x.TafsilCode = null;
            x.TafsilId = null;


            _db.parties.Update(x);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<SelectList> SelectList_PersenAsync(Int64 sellerId, short? role = null)
        {
            var data = await _db.parties
                .Where(n =>
                (role == null ? n.Role > 0 : n.Role == role.Value)
                && n.uyer_SellerId == sellerId)
                .Select(n => new { id = n.Id, name = n.Name })
                .ToListAsync();
            return new SelectList(data, "id", "name");

        }
        public async Task<SelectList> SelectList_PersenListAsync(Int64 sellerId, bool? isVendor = null, bool? isCustomer = null)
        {
            var query = _db.parties
                .Where(n => n.uyer_SellerId == sellerId)
                .AsQueryable();

            if (isVendor.HasValue && isVendor.Value)
                query = query.Where(n => n.IsVendor == true);
            if (isCustomer.HasValue && isCustomer.Value)
                query = query.Where(n => n.IsCustomer == true);

            var data = await query.Select(n => new { id = n.Id, name = n.Name }).OrderBy(n => n.name).ToListAsync();
            return new SelectList(data, "id", "name");

        }
        public async Task<SelectList> SelectItems_TafsilsByGroupAsync(int[] groupIds, long sellerId)
        {
            var data = await _db.Acc_Coding_TafsilToGroups
                .Include(n => n.TafsilAccount)
                .Include(n => n.Group)
                .Where(n => groupIds.Contains(n.GroupId) && n.TafsilAccount.SellerId == sellerId)
                .Select(n => new TafsilListDto
                {
                    id = n.TafsilId,
                    name = n.TafsilAccount.Name,
                    GroupId = n.GroupId,
                    GroupName = n.Group.GroupName,
                }).Distinct().OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<string> GetPersonNameByIdAsync(Int64 id)
        {
            var person = await _db.parties.FindAsync(id);
            if (person != null)
                return person.Name;

            return "نامشخص";
        }
        public async Task<clsResult> DeletePersonAsync(Int64 id)
        {
            var party = await _db.parties.FindAsync(id);
            if (party == null)
            {
                return new clsResult() { Success = false, Message = "موردی با این شناسه یافت نشد" };
            }
            bool IsRepresentative = await _db.PartyRepresentatives.AnyAsync(n => n.PartyId == id || n.RepresentativeId == id);
            if (IsRepresentative)
            {
                return new clsResult() { Success = false, Message = "ردیف موردنظر در بانک اطلاعاتی دارای سابقه است و امکان حذف آن وجود ندارد" };
            }

            _db.parties.Remove(party);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new clsResult() { Success = true, Message = "ردیف موردنظر با موفقیت حذف شد" };
            }
            return new clsResult() { Success = false, Message = "خطایی حین عملیات رخ داده است." };
        }
        public async Task<long> GetOrCreatePersonIdAsync(string personName, string? personCode = null, string? nationalId = null, string? economicCode = null)
        {
            var existingPerson = await _db.parties
                .FirstOrDefaultAsync(b => b.Name == personName.Trim());

            if (existingPerson != null)
            {
                return existingPerson.Id;
            }

            if (!_userContext.SellerId.HasValue)
                return 0;

            // If the buyer does not exist, create a new record
            var person = new Party
            {
                Name = personName,
                AccountingSystemId = personCode,
                CustomerId = _userContext.CustomerId ?? 0,
                NationalId = nationalId,
                EconomicCode = economicCode,
                Role = (Int16)PartyRole.Buyer,
                IsActive = true,
                Title = personName,
                uyer_SellerId = _userContext.SellerId.Value,
                LegalStatus = 1,
            };

            _db.parties.Add(person);
            await _db.SaveChangesAsync();
            return person.Id;
        }


        // ==== Party Representative
        public async Task<SelectList> SelectList_PersenRepresentativesAsync(Int64 sellerId)
        {
            var data = await _db.PartyRepresentatives
                .Where(n =>
                 n.PartyId == sellerId).Include(n => n.Representative)
                .Select(n => new { id = n.RepresentativeId, name = n.Representative.Name })
                .ToListAsync();
            return new SelectList(data, "id", "name");
        }
        public async Task<List<PresentativeDto>> GetPersenRepresentativesDtoAsync(Int64 sellerId)
        {
            var Representatives = await _db.PartyRepresentatives
                .Where(n => n.PartyId == sellerId)
                .Include(n => n.Representative).Include(n => n.Party)
                .Select(n => new PresentativeDto
                {
                    RepresentativeId = n.RepresentativeId,
                    IdentityNumber = n.Representative.NationalId,
                    Name = n.Representative.Name,
                    phone = n.Representative.MobilePhone,
                    ParentId = n.PartyId,
                    ParentName = n.Party.Name,
                }).ToListAsync();

            return Representatives;
        }
        public async Task<clsResult> AddPartyRepresentativeAsync(long PartyId, long RepresentativeId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (PartyId == null || PartyId == 0 || RepresentativeId == 0 || RepresentativeId == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            if (PartyId == RepresentativeId)
            {
                result.Message = "رابط و شخص اصلی یکی هستند.";
                return result;
            }

            if (_db.PartyRepresentatives.Any(n => n.RepresentativeId == RepresentativeId && n.PartyId == PartyId))
            {
                result.Message = "شخص موردنظر در لیست رابطین وجود دارد";
                return result;
            }
            PartyRepresentative n = new PartyRepresentative();
            n.PartyId = PartyId;
            n.RepresentativeId = RepresentativeId;
            _db.PartyRepresentatives.Add(n);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"شخص موردنظر به لیست رابطین افزوده شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<clsResult> RemovePartyRepresentativeAsync(long PartyId, long RepresentativeId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            var data = await _db.PartyRepresentatives.SingleOrDefaultAsync(n => n.RepresentativeId == RepresentativeId && n.PartyId == PartyId);
            if (data == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            _db.PartyRepresentatives.Remove(data);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات حذف اطلاعات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<clsResult> CreateBulkTafsilsAsync(long sellerId)
        {
            var persen = await _db.parties.Where(n => n.uyer_SellerId == sellerId && n.TafsilId == null).Select(n => n.Id).ToListAsync<long>();
            var result = await _coding.CreatBulkPersonTafsilAsync(persen, sellerId);
            return result;
        }
    }
}
