using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Classes;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccCodingService : IAccCodingService
    {
        private readonly AppDbContext _db;

        public AccCodingService(AppDbContext db)
        {
            _db = db;
        }

        public SelectList SelectList_AccountType()
        {
            List<SelectListDto> lst = new List<SelectListDto>();
            lst.Add(new SelectListDto { Id = 1, Name = "ترازنامه ای - دائم" });
            lst.Add(new SelectListDto { Id = 2, Name = "سود و زیانی - موقت" });
            lst.Add(new SelectListDto { Id = 3, Name = "کنترلی و آماری" });

            return new SelectList(lst, "Id", "Name");
        }
        public SelectList SelectList_GroupType()
        {
            List<SelectListDto> lst = new List<SelectListDto>();
            lst.Add(new SelectListDto { Id = 1, Name = "دارایی" });
            lst.Add(new SelectListDto { Id = 2, Name = "بدهی" });
            lst.Add(new SelectListDto { Id = 3, Name = "سرمایه" });
            lst.Add(new SelectListDto { Id = 4, Name = "درآمد" });
            lst.Add(new SelectListDto { Id = 5, Name = "هزینه" });
            lst.Add(new SelectListDto { Id = 6, Name = "انتظامی" });

            return new SelectList(lst, "Id", "Name");
        }
        public SelectList SelectList_Nature()
        {
            List<SelectListDto> lst = new List<SelectListDto>();
            lst.Add(new SelectListDto { Id = 1, Name = "بدهکار" });
            lst.Add(new SelectListDto { Id = 2, Name = "بستانکار" });
            lst.Add(new SelectListDto { Id = 3, Name = "بدهکار/بستانکار" });

            return new SelectList(lst, "Id", "Name");
        }
        public SelectList SelectList_DocTypes()
        {
            List<SelectListDto> lst = new List<SelectListDto>();
            lst.Add(new SelectListDto { Id = 1, Name = "سند روزانه" });
            lst.Add(new SelectListDto { Id = 2, Name = "سند افتتاحیه" });
            lst.Add(new SelectListDto { Id = 3, Name = "سند اختتامیه" });
            lst.Add(new SelectListDto { Id = 4, Name = "سند بستن حساب های موقت" });
            lst.Add(new SelectListDto { Id = 5, Name = "سند بستن حساب های دائم" });
            lst.Add(new SelectListDto { Id = 6, Name = "سند طبقه بندی حساب ها" });

            return new SelectList(lst, "Id", "Name");
        }
        //
        //لیست گروه های حساب
        public async Task<SelectList> SelectList_GroupAccountsAsync(long sellerId, Int16 typeid)
        {
            var accounts = await _db.Acc_Coding_Groups.Where(n => n.SellerId == sellerId && n.TypeId == typeid)
               .Select(n => new { id = n.Id, name = n.GroupCode + " - " + n.GroupName, code = n.GroupCode }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(accounts, "id", "name");
        }
        // لیست سرفصل های موقت
        public async Task<SelectList> SelectList_TemporaryAccounts_KolAsync(long sellerId)
        {
            var accounts = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId && n.KolGroup.TypeId == 2)
               .Select(n => new { id = n.Id, name = n.KolCode + " - " + n.KolName, code = n.KolCode }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(accounts, "id", "name");
        }
        // لیست معین های موقت
        public async Task<SelectList> SelectList_TemporaryAccounts_MoeinAsync(long sellerId)
        {
            var accounts = await _db.Acc_Coding_Moeins.Where(n => n.SellerId == sellerId && n.MoeinKol.KolGroup.TypeId == 2)
               .Select(n => new { id = n.Id, name = n.MoeinCode + " - " + n.MoeinName, code = n.MoeinCode }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(accounts, "id", "name");
        }
        // لیست سرفصل های دائم
        public async Task<SelectList> SelectList_PermanentAccounts_KolAsync(long sellerId)
        {
            var accounts = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId && n.KolGroup.TypeId == 1)
                .Select(n => new { id = n.Id, name = n.KolCode + " - " + n.KolName, code = n.KolCode }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(accounts, "id", "name");
        }
        // لیست معین های دائم
        public async Task<SelectList> SelectList_PermanentAccounts_MoeinAsync(long sellerId)
        {
            var accounts = await _db.Acc_Coding_Moeins.Where(n => n.SellerId == sellerId && n.MoeinKol.KolGroup.TypeId == 1)
               .Select(n => new { id = n.Id, name = n.MoeinCode + " - " + n.MoeinName, code = n.MoeinCode }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(accounts, "id", "name");
        }

        public async Task<clsResult> ResetSellerAccountingData(long sellerId)
        {
            clsResult result = new clsResult();
            result.ShowMessage = true;
            result.Success = false;


            var articles = await _db.Acc_Articles.Where(a => a.Doc.SellerId == sellerId).ToListAsync();
            if (articles.Count > 0)
            {
                //حذف آرتیکل
                _db.Acc_Articles.RemoveRange(articles);
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    var docs = await _db.Acc_Documents.Where(a => a.SellerId == sellerId).ToListAsync();
                    if (docs.Count > 0)
                    {
                        //حذف اسناد
                        _db.Acc_Documents.RemoveRange(docs);
                        if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                        {
                            var moeins = await _db.Acc_Coding_Moeins.Where(n => n.SellerId == sellerId).ToListAsync();
                            if (moeins.Count > 0)
                            {    //حذف حساب های معین
                                _db.Acc_Coding_Moeins.RemoveRange(moeins);
                                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                                {
                                    var kols = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId).ToListAsync();
                                    if (kols.Count > 0)
                                    {    //حذف حساب های کل
                                        _db.Acc_Coding_Kols.RemoveRange(kols);
                                        if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                                        {
                                            var groups = await _db.Acc_Coding_Groups.Where(n => n.SellerId == sellerId).ToListAsync();
                                            if (groups.Count > 0)
                                            {
                                                _db.Acc_Coding_Groups.RemoveRange(groups);
                                                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                                                {
                                                    result.Success = true;
                                                    result.Message = "حذف اطلاعات با موفقیت انجام شد";
                                                    return result;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
        // Default Coding
        public async Task<clsResult> LoadDefaultCoding_CommercialAsync(long sellerId)
        {
            clsResult result = new clsResult();
            result.ShowMessage = true;
            result.Success = false;

            DefaultCoding accounts = new DefaultCoding();

            var groups = accounts.DefaultCoding_Groups(sellerId);
            var currentGroups = await _db.Acc_Coding_Groups.Where(n => n.SellerId == sellerId).ToListAsync();
            foreach (var group in groups)
            {
                if (currentGroups.Any(n => n.GroupCode == group.GroupCode))
                {
                    result.Message += "\n گروه  " + group.GroupName + " با کد " + group.GroupCode + " پیش از این در سیستم تعریف شده است";
                    groups.Remove(group);
                }
            }
            _db.Acc_Coding_Groups.AddRange(groups);


            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    var kols = accounts.DefaultCoding_Kol_Commercial(sellerId);
                    var CurrentKolAccounts = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId).ToListAsync();
                    foreach (var kol in kols)
                    {
                        var g = _db.Acc_Coding_Groups.FirstOrDefault(n => n.GroupCode == kol.Description);
                        if (CurrentKolAccounts.Any(a => a.KolCode == kol.KolCode))
                        {
                            result.Message += "\n حساب کل  " + kol.KolName + " با کد " + kol.KolCode + " پیش از این در سیستم تعریف شده است";
                            kols.Remove(kol);
                        }
                        else
                        {
                            if (g != null)
                            {
                                kol.SellerId = sellerId;
                                kol.GroupId = g.Id;
                                kol.Description = null;
                            }
                            else
                            {
                                result.Message += "\n حساب کل با کد " + kol.Description + " یافت نشد";
                                kols.Remove(kol);
                            }
                        }
                    }
                    if (kols.Count > 0)
                        _db.Acc_Coding_Kols.AddRange(kols);

                    if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                    {
                        var moeins = accounts.DefaultCoding_Moein_Commercial(sellerId);
                        var sellerKols = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId).ToListAsync();
                        foreach (var moein in moeins)
                        {
                            var k = sellerKols.FirstOrDefault(n => n.KolCode == moein.Description);
                            if (k != null)
                            {
                                moein.KolId = k.Id;
                                moein.SellerId = sellerId;
                                moein.Description = null;
                            }
                            else
                            {
                                result.Message += "\n حساب کل با کد " + moein.Description + " یافت نشد";
                                moeins.Remove(moein);
                            }
                        }

                        if (moeins.Count > 0)
                        {
                            _db.Acc_Coding_Moeins.AddRange(moeins);
                            try
                            {
                                if (Convert.ToBoolean(_db.SaveChanges()))
                                {
                                    result.Success = true;
                                    result.Message = "کدینگ حسابداری با موفقیت بارگزاری شد";
                                    return result;
                                }
                                else
                                {
                                    result.Message += "\n مشکلی در ثبت حساب های معین بوجود آمده است";
                                    return result;
                                }
                            }
                            catch (Exception x)
                            {
                                result.Message += "\n مشکلی در ثبت حساب های معین بوجود آمده است";
                                result.Message += "\n" + x.Message;
                                return result;
                            }
                        }
                        else
                        {
                            result.Success = true;
                            result.Message = "همه سرفصل ها، پیش از این بارگذاری شده اند";
                            return result;
                        }

                    }
                    else
                    {
                        result.Message += "\n مشکلی در ثبت حساب های کل بوجود آمده است";
                        return result;
                    }
                }
                else
                {
                    result.Message += "\n مشکلی در ثبت حساب های کل بوجود آمده است";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message += "\n مشکلی در ثبت حساب های کل بوجود آمده است";
                result.Message += "\n" + x.Message;
                return result;
            }

            //return result;
        }
        public async Task<clsResult> LoadDefaultCoding_AddGroupsAsync(long sellerId)
        {
            clsResult result = new clsResult();
            result.ShowMessage = true;
            result.Success = false;

            DefaultCoding accounts = new DefaultCoding();

            var groups = accounts.DefaultCoding_Groups(sellerId);
            var currentGroups = await _db.Acc_Coding_Groups.Where(n => n.SellerId == sellerId).ToListAsync();
            foreach (var group in groups)
            {
                if (currentGroups.Any(n => n.GroupCode == group.GroupCode))
                {
                    result.Message += "\n گروه  " + group.GroupName + " با کد " + group.GroupCode + " پیش از این در سیستم تعریف شده است";
                    groups.Remove(group);
                }
            }
            _db.Acc_Coding_Groups.AddRange(groups);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                result.Success = true;
                result.Message = "گروه حسابها با موفقیت بارگزاری شد";
                return result;
            }
            else
            {
                result.Success = false;
                result.Message = "خطایی در افزودن گروه حساب ها پیش آمده است.";
            }

            return result;
        }
        public async Task<clsResult> LoadDefaultCoding_SetGroupIdAsync(long sellerId)
        {
            clsResult result = new clsResult();
            result.ShowMessage = true;
            result.Success = false;

            var sellerGroups = await _db.Acc_Coding_Groups.Where(n => n.SellerId == sellerId).ToListAsync();
            if (sellerGroups.Count <= 0)
                return result;

            try
            {
                foreach (var group in sellerGroups)
                {
                    var kols = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId && n.KolGroup.GroupCode == group.GroupCode)
                       .ToListAsync();
                    foreach (var kol in kols)
                    {
                        kol.GroupId = group.Id;
                    }
                    _db.Acc_Coding_Kols.UpdateRange(kols);
                    await _db.SaveChangesAsync();
                }

                result.Success = true;
                result.Message = "گروه حسابها با موفقیت بارگزاری شد";
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "خطایی در اصطلاح شناسه ها پیش آمده است.";
            }

            return result;
        }

        // Groups
        public async Task<List<GroupDto>> GetGroupsAsync(long? SellerId = null)
        {
            var groups = await _db.Acc_Coding_Groups
                .Where(n => n.SellerId == SellerId)
                .Select(n => new GroupDto
                {
                    Id = n.Id,
                    GroupName = n.GroupName,
                    GroupCode = n.GroupCode,
                    Description = n.Description,
                    SellerId = n.SellerId,
                    IsEditable = n.IsEditable,
                    TypeId = n.TypeId,
                    TypeName = n.TypeId.AccToGroupTypeName(),
                    GroupType = n.GroupType,
                    GroupTypeName = n.GroupType.AccToGroupType(),
                    Order = n.Order,
                }).ToListAsync();

            return groups;
        }
        public async Task<GroupDto> GetGroupDtoAsync(int Id)
        {
            var group = await _db.Acc_Coding_Groups.SingleOrDefaultAsync(n => n.Id == Id);
            GroupDto dto = new GroupDto();
            dto.Id = group.Id;
            dto.SellerId = group.SellerId;
            dto.GroupCode = group.GroupCode;
            dto.GroupName = group.GroupName;
            dto.TypeId = group.TypeId;
            dto.Description = group.Description;
            dto.IsEditable = dto.IsEditable;
            dto.GroupType = group.GroupType;
            dto.Order = group.Order;
            return dto;
        }
        public async Task<clsResult> AddGroupAsync(GroupDto dto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Acc_Coding_Groups.Where(n =>
            n.GroupCode == dto.GroupCode
            && (n.SellerId == dto.SellerId)
            ).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این گروهی با کد {checkDuplicate.GroupCode} با نام {checkDuplicate.GroupName} در سیستم تعریف شده است.";
                return result;
            }

            Acc_Coding_Group group = new Acc_Coding_Group();
            group.SellerId = dto.SellerId;
            group.GroupCode = dto.GroupCode;
            group.GroupName = dto.GroupName;
            group.TypeId = dto.TypeId;
            group.GroupType = dto.GroupType;
            group.Description = dto.Description;
            group.Order = dto.Order;
            group.IsEditable = true;

            _db.Acc_Coding_Groups.Add(group);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" گروه {group.GroupName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> EditGroupAsync(GroupDto dto)
        {
            clsResult result = new clsResult();

            var checkDuplicate = await _db.Acc_Coding_Groups.Where(n =>
            n.GroupCode == dto.GroupCode
            && (n.SellerId == dto.SellerId) && (n.Id != dto.Id)
            ).FirstOrDefaultAsync();

            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این گروهی با کد {checkDuplicate.GroupCode} با نام {checkDuplicate.GroupName} در سیستم تعریف شده است.";
                return result;
            }

            Acc_Coding_Group? group = await _db.Acc_Coding_Groups.FindAsync(dto.Id);
            if (group == null)
            {
                result.Success = false;
                result.Message = $"اطلاعات موردنظر یافت نشد";
                return result;
            }
            group.SellerId = dto.SellerId;
            group.GroupCode = dto.GroupCode;
            group.GroupName = dto.GroupName;
            group.TypeId = dto.TypeId;
            group.GroupType = dto.GroupType;
            group.Description = dto.Description;
            group.Order = dto.Order;
            group.IsEditable = true;

            _db.Acc_Coding_Groups.Update(group);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" گروه {group.GroupName} با موفقیت ویرایش شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> DeleteGroupAsync(short id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;

            var group = await _db.Acc_Coding_Groups.FindAsync(id);
            if (group == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }

            if (_db.Acc_Coding_Kols.Any(n => n.GroupId == id))
            {
                result.Message = "گروه موردنظر دارای زیرمجموعه می باشد";
                return result;
            }

            _db.Acc_Coding_Groups.Remove(group);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"گروه مورنظر با موفقیت حذف شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در حذف اطلاعات رخ داده است. " + x.Message;
            }

            return result;

        }

        //Kol
        public async Task<SelectList> SelectList_KolsAsync(long sellerId)
        {
            var accounts = await _db.Acc_Coding_Kols.AsNoTracking().Where(n => n.SellerId == sellerId)
                .Select(n => new { id = n.Id, name = n.KolName, code = n.KolCode })
               .OrderBy(n => n.code).ToListAsync();

            return new SelectList(accounts, "id", "name");
        }
        public async Task<List<KolDto>> GetKolsAsync(short? groupId = null, long? SellerId = null)
        {
            var kols = await _db.Acc_Coding_Kols
                .Where(n =>
                  (n.SellerId == SellerId)
                  && (groupId == null ? n.GroupId > 0 : n.GroupId == groupId))
                .Select(n => new KolDto
                {
                    Id = n.Id,
                    GroupId = n.GroupId,
                    Description = n.Description,
                    SellerId = n.SellerId,
                    IsEditable = n.IsEditable,
                    TypeId = n.TypeId,
                    KolCode = n.KolCode,
                    KolName = n.KolName,
                    Nature = n.Nature,
                    GroupName = n.KolGroup.GroupName,
                    NatureName = n.Nature.AccToNatureName(),
                    TypeName = n.TypeId.AccToGroupTypeName(),

                }).OrderBy(n => n.KolCode).ToListAsync();

            return kols;
        }
        public async Task<KolDto?> GetKolDtoAsync(int id)
        {
            var kol = await _db.Acc_Coding_Kols.Include(n => n.KolGroup).SingleOrDefaultAsync(n => n.Id == id);
            if (kol == null)
                return null;

            KolDto dto = new KolDto();
            dto.Id = id;
            dto.KolCode = kol.KolCode;
            dto.KolName = kol.KolName;
            dto.Description = kol.Description;
            dto.Nature = kol.Nature;
            dto.NatureName = kol.Nature.AccToNatureName();
            dto.TypeId = kol.TypeId;
            dto.TypeName = kol.TypeId.AccToGroupTypeName();

            dto.GroupId = kol.GroupId;
            dto.GroupName = kol.KolGroup.GroupName;
            dto.GroupCode = kol.KolGroup.GroupCode;

            dto.SellerId = kol.SellerId;
            dto.IsEditable = kol.IsEditable;

            return dto;

        }
        public async Task<int?> GetKolIdByCodeAsync(string code, long sellerId)
        {
            var kol = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId && n.KolCode == code).FirstOrDefaultAsync();
            if (kol == null)
                return null;
            return kol.Id;
        }
        public async Task<clsResult> AddKolAsync(KolDto dto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Acc_Coding_Kols.Where(n =>
            n.KolCode == dto.KolCode
            && (n.SellerId == dto.SellerId)
            ).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این سرفصلی با کد {checkDuplicate.KolCode} با نام {checkDuplicate.KolName} در سیستم تعریف شده است.";
                return result;
            }

            Acc_Coding_Kol kol = new Acc_Coding_Kol();
            kol.SellerId = dto.SellerId;
            kol.KolCode = dto.KolCode;
            kol.TypeId = dto.TypeId;
            kol.KolName = dto.KolName;
            kol.Nature = dto.Nature;
            kol.GroupId = dto.GroupId;
            kol.Description = dto.Description;
            kol.IsEditable = true;

            _db.Acc_Coding_Kols.Add(kol);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" سر فصل {kol.KolName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> EditKolAsync(KolDto dto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Acc_Coding_Kols.Where(n =>
            n.KolCode == dto.KolCode
            && (n.SellerId == dto.SellerId)
            && (n.Id != dto.Id)
            ).FirstOrDefaultAsync();

            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این سرفصلی با کد {checkDuplicate.KolCode} با نام {checkDuplicate.KolName} در سیستم تعریف شده است.";
                return result;
            }

            Acc_Coding_Kol kol = await _db.Acc_Coding_Kols.FindAsync(dto.Id);
            if (kol == null)
            {
                result.Success = false;
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            kol.SellerId = dto.SellerId;
            kol.KolCode = dto.KolCode;
            kol.TypeId = dto.TypeId;
            kol.KolName = dto.KolName;
            kol.Nature = dto.Nature;
            kol.GroupId = dto.GroupId;
            kol.Description = dto.Description;
            kol.IsEditable = true;

            _db.Acc_Coding_Kols.Update(kol);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" سر فصل {kol.KolName} با موفقیت ویرایش شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> DeleteTheKolAsync(int id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;

            var kol = await _db.Acc_Coding_Kols.FindAsync(id);
            if (kol == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }

            if (_db.Acc_Articles.Any(n => n.Moein.KolId == id))
            {
                result.Message = "سرفصل موردنظر در بانک اطلاعاتی دارای سابقه می باشد";
                return result;
            }

            _db.Acc_Coding_Kols.Remove(kol);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"سرفصل مورنظر با موفقیت حذف شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در حذف اطلاعات رخ داده است. " + x.Message;
            }

            return result;

        }
        public async Task<string> GenerateKolCodeAsync(int groupId)
        {
            string code = "";
            var kols = await _db.Acc_Coding_Kols.Where(n => n.GroupId == groupId).ToListAsync();
            if (kols.Count > 0)
            {
                string lastCode = kols.Select(n => n.KolCode).Max<string>();
                int intCode = Convert.ToInt32(lastCode) + 1;
                code = intCode.ToString();
            }
            else
            {
                var group = await _db.Acc_Coding_Groups.SingleOrDefaultAsync(n => n.Id == groupId);
                code = group.GroupCode + "01";
            }


            return code;
        }

        //Moeins
        public async Task<string> GenerateMoeinCodeAsync(int KolId)
        {
            string code = "";
            var moeins = await _db.Acc_Coding_Moeins.Where(n => n.KolId == KolId).ToListAsync();
            if (moeins.Count > 0)
            {
                string lastCode = moeins.Select(n => n.MoeinCode).Max<string>();
                int intCode = Convert.ToInt32(lastCode) + 1;
                code = intCode.ToString();
            }
            else
            {
                var kol = await _db.Acc_Coding_Kols.SingleOrDefaultAsync(n => n.Id == KolId);
                code = kol.KolCode + "0001";
            }
            return code;
        }
        public async Task<List<MoeinDto>> GetMoeinsAsync(int? kolId = null, long? SellerId = null)
        {
            var Moeins = await _db.Acc_Coding_Moeins
                .Where(n =>
                  (n.SellerId == SellerId)
                && (kolId == null ? n.KolId > 0 : n.KolId == kolId))
                .Select(n => new MoeinDto
                {
                    Id = n.Id,
                    KolId = n.KolId,
                    MoeinCode = n.MoeinCode,
                    MoeinName = n.MoeinName,
                    Nature = n.Nature,
                    MoeinContraryNatureId = n.MoeinContraryNatureId,
                    Description = n.Description,
                    IsCurrencyAccount = n.IsCurrencyAccount,
                    CurrencyId = n.CurrencyId,
                    SellerId = n.SellerId,
                    IsEditable = n.IsEditable,
                    KolName = n.MoeinKol.KolName,
                    KolCode = n.MoeinKol.KolCode,
                    GroupId = n.MoeinKol.GroupId,
                    GroupName = n.MoeinKol.KolGroup.GroupName

                }).OrderBy(n => n.MoeinCode).ToListAsync();

            return Moeins;
        }
        public async Task<MoeinDto?> GetMoeinDtoByIdAsync(int Id)
        {
            var moein = await _db.Acc_Coding_Moeins
                .Include(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .SingleOrDefaultAsync(n => n.Id == Id);

            if (moein == null) return null;

            MoeinDto dto = new MoeinDto();
            dto.Id = moein.Id;
            dto.GroupId = moein.MoeinKol.GroupId;
            dto.GroupName = moein.MoeinKol.KolGroup.GroupName;
            dto.KolId = moein.KolId;
            dto.KolCode = moein.MoeinKol.KolCode;
            dto.KolName = moein.MoeinKol.KolName;
            dto.MoeinCode = moein.MoeinCode;
            dto.MoeinName = moein.MoeinName;
            dto.Nature = moein.Nature;
            dto.MoeinContraryNatureId = moein.MoeinContraryNatureId;
            dto.IsCurrencyAccount = moein.IsCurrencyAccount;
            dto.CurrencyId = moein.CurrencyId;
            dto.Description = moein.Description;

            return dto;
        }
        public async Task<int?> GetMoeinIdByCodeAsync(string code, long sellerId)
        {
            var moein = await _db.Acc_Coding_Moeins.Where(n => n.SellerId == sellerId && n.MoeinCode == code).FirstOrDefaultAsync();
            if (moein == null)
                return null;
            return moein.Id;
        }
        public async Task<clsResult> AddMoeinAsync(MoeinDto dto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Acc_Coding_Moeins.Where(n =>
            n.MoeinCode == dto.MoeinCode
            && (n.SellerId == dto.SellerId)
            ).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این حسابی با کد {checkDuplicate.MoeinCode} با نام {checkDuplicate.MoeinName} در سیستم تعریف شده است.";
                return result;
            }

            Acc_Coding_Moein Moein = new Acc_Coding_Moein();
            Moein.KolId = dto.KolId;
            Moein.MoeinCode = dto.MoeinCode;
            Moein.SellerId = dto.SellerId;
            Moein.MoeinName = dto.MoeinName;
            Moein.Nature = dto.Nature;
            Moein.IsCurrencyAccount = dto.IsCurrencyAccount;
            Moein.CurrencyId = dto.CurrencyId;
            Moein.MoeinContraryNatureId = dto.MoeinContraryNatureId;
            Moein.Description = dto.Description;
            Moein.IsEditable = true;

            _db.Acc_Coding_Moeins.Add(Moein);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" حساب {Moein.MoeinName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> EditMoeinAsync(MoeinDto dto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Acc_Coding_Moeins.Where(n =>
            n.MoeinCode == dto.MoeinCode
            && (n.SellerId == dto.SellerId)
            && (n.Id != dto.Id)
            ).FirstOrDefaultAsync();

            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این حسابی با کد {checkDuplicate.MoeinName} با نام {checkDuplicate.MoeinCode} در سیستم تعریف شده است.";
                return result;
            }

            Acc_Coding_Moein? moein = await _db.Acc_Coding_Moeins.FindAsync(dto.Id);
            if (moein == null)
            {
                result.Success = false;
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            moein.SellerId = dto.SellerId;
            moein.KolId = dto.KolId;
            moein.MoeinCode = dto.MoeinCode;
            moein.MoeinName = dto.MoeinName;
            moein.Nature = dto.Nature;
            moein.CurrencyId = dto.CurrencyId;
            moein.Description = dto.Description;
            moein.IsCurrencyAccount = dto.IsCurrencyAccount;
            moein.MoeinContraryNatureId = dto.MoeinContraryNatureId;

            _db.Acc_Coding_Moeins.Update(moein);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" حساب {moein.MoeinName} با موفقیت ویرایش شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<SelectList> SelectList_MoeinsAsync(long? sellerId = null, string name = "", int? kolId = null)
        {
            var lst = await _db.Acc_Coding_Moeins.Where(n =>
                   (n.SellerId == sellerId)
                && (kolId == null ? n.KolId > 0 : n.KolId == kolId)
                && (n.MoeinName.Contains(name))).Select(n => new
                {
                    id = n.Id,
                    name = n.MoeinCode + " " + n.MoeinName,
                }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_Moeins2Async(long sellerId, List<int>? kols = null)
        {
            var query = _db.Acc_Coding_Moeins.AsNoTracking().Where(n => n.SellerId == sellerId).AsQueryable();

            if (kols.Count > 0)
                query = query.Where(n => kols.Contains(n.KolId));

            var lst = await query.Select(n => new { id = n.Id, name = n.MoeinName, code = n.MoeinCode })
                .OrderBy(n => n.code)
                .ToListAsync();
            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageMoeinsAsync(long? sellerId = null)
        {
            var lst = await _db.Acc_Articles.Include(n => n.Moein).Where(n =>
                   (n.SellerId == sellerId && n.IsDeleted == false)).Select(n => new
                   {
                       id = n.MoeinId,
                       name = n.Moein.MoeinCode + " " + n.Moein.MoeinName,
                   }).Distinct().OrderBy(n => n.name).ToListAsync();

            return new SelectList(lst, "id", "name");
        }

        public async Task<int?> GetKolIdByMoeinIdAsync(int? MoeinId)
        {
            if (MoeinId == null || MoeinId == 0) return null;
            var moein = await _db.Acc_Coding_Moeins.FindAsync(MoeinId);
            return moein.KolId;
        }
        public async Task<clsResult> DeleteMoeinAsync(int Id)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var moein = await _db.Acc_Coding_Moeins.FindAsync(Id);
            if (moein == null)
            {
                result.Message = "حساب موردنظر یافت نشد.";
                return result;
            }

            if (_db.Acc_Articles.Any(n => n.MoeinId == Id))
            {
                result.Message = "حساب موردنظر در بانک اطلاعاتی دارای سابقه می باشد";
                return result;
            }

            _db.Acc_Coding_Moeins.Remove(moein);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"حساب مورنظر با موفقیت حذف شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در حذف حساب رخ داده است. " + x.Message;
            }

            return result;
        }

        //Tafsil Group
        public async Task<List<TafsilGroupDto>> TafsilsGroupAsync(Int64? SellerId = null)
        {
            var tafsils = await _db.Acc_Coding_TafsilGroups
                .Where(n =>
                 (n.SellerId == null || n.SellerId == SellerId))
                .Select(n => new TafsilGroupDto
                {
                    Id = n.Id,
                    GroupName = n.GroupName,
                    Description = n.Description,
                    SellerId = n.SellerId,
                    IsEditable = n.IsEditable,
                    IsPerson = n.IsPerson,
                }).ToListAsync();
            return tafsils;
        }

        public async Task<TafsilGroupDto> GetTafsilGroupById(int Id)
        {
            var tafsil = await _db.Acc_Coding_TafsilGroups
                .Where(n =>
                 (n.Id == Id))
                .Select(n => new TafsilGroupDto
                {
                    Id = n.Id,
                    GroupName = n.GroupName,
                    Description = n.Description,
                    SellerId = n.SellerId,
                    IsEditable = n.IsEditable,
                    IsPerson = n.IsPerson,
                }).FirstOrDefaultAsync();
            return tafsil;
        }
        public async Task<SelectList> SelectList_TafsilGroupsAsync(Int64? SellerId = null)
        {
            var tafsils = await _db.Acc_Coding_TafsilGroups
                 .Where(n =>
                  (n.SellerId == null || n.SellerId == SellerId))
                 .Select(n => new TafsilGroupDto
                 {
                     Id = n.Id,
                     GroupName = n.GroupName,
                 }).ToListAsync();

            return new SelectList(tafsils, "Id", "GroupName");
        }
        public async Task<clsResult> AddTafsilGroupAsync(TafsilGroupDto dto)
        {
            clsResult result = new clsResult();

            Acc_Coding_TafsilGroup group = new Acc_Coding_TafsilGroup();
            group.GroupName = dto.GroupName;
            group.Description = dto.Description;
            group.SellerId = dto.SellerId;
            group.IsEditable = dto.IsEditable;
            group.IsPerson = dto.IsPerson;

            _db.Acc_Coding_TafsilGroups.Add(group);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" گروه تفصیلی {group.GroupName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> UpdateTafsilGroupAsync(TafsilGroupDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            Acc_Coding_TafsilGroup group = await _db.Acc_Coding_TafsilGroups.FindAsync(dto.Id);
            if (group == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
            }

            group.GroupName = dto.GroupName;
            group.Description = dto.Description;
            group.SellerId = dto.SellerId;
            group.IsEditable = dto.IsEditable;
            group.IsPerson = dto.IsPerson;

            _db.Acc_Coding_TafsilGroups.Update(group);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" گروه تفصیلی {group.GroupName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        private int[] ConvertStringToIntArray(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return Array.Empty<int>();

            input = input.Trim('[', ']');

            return input.Split(',')
                        .Select(id => int.TryParse(id, out var result) ? result : 0)
                        .Where(id => id != 0)
                        .ToArray();
        }
        private string[] GroupIdsToStringArray(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return Array.Empty<string>();

            input = input.Trim('[', ']');

            return input.Split(',').ToArray();
        }
        public async Task<AccountTafsilDto> GetAccountTafsilAsync(int MoeinId)
        {
            var Moein = await _db.Acc_Coding_Moeins
                .Include(n => n.MoeinKol)
                .ThenInclude(n => n.KolGroup)
                .SingleOrDefaultAsync(n => n.Id == MoeinId);

            if (Moein == null)
                return null;

            var dto = new AccountTafsilDto();
            dto.Id = Moein.Id;
            dto.MoeinCode = Moein.MoeinCode;
            dto.MoeinName = Moein.MoeinName;
            dto.KolId = Moein.KolId;
            dto.KolCode = Moein.MoeinKol.KolCode;
            dto.KolName = Moein.MoeinKol.KolName;
            dto.GroupId = Moein.MoeinKol.GroupId;
            dto.Tafsil4_GroupIds = Moein.Tafsil4_GroupIds;
            dto.Tafsil5_GroupIds = Moein.Tafsil5_GroupIds;
            dto.Tafsil6_GroupIds = Moein.Tafsil6_GroupIds;
            dto.Tafsil7_GroupIds = Moein.Tafsil7_GroupIds;
            dto.Tafsil8_GroupIds = Moein.Tafsil8_GroupIds;
            dto.Tafsil4_Array = ConvertStringToIntArray(Moein.Tafsil4_GroupIds);
            dto.Tafsil5_Array = ConvertStringToIntArray(Moein.Tafsil5_GroupIds);
            dto.Tafsil6_Array = ConvertStringToIntArray(Moein.Tafsil6_GroupIds);
            dto.Tafsil7_Array = ConvertStringToIntArray(Moein.Tafsil7_GroupIds);
            dto.Tafsil8_Array = ConvertStringToIntArray(Moein.Tafsil8_GroupIds);

            return dto;

        }
        public async Task<clsResult> SetTafsilsAsync(AccountTafsilDto dto)
        {
            clsResult result = new clsResult();

            var moein = await _db.Acc_Coding_Moeins.FindAsync(dto.Id);
            if (moein == null)
            {
                result.Success = true;
                result.Message = $"اطلاعات حساب معین یافت نشد.";
            }
            moein.Tafsil4_GroupIds = dto.Tafsil4_GroupIds;
            moein.Tafsil5_GroupIds = dto.Tafsil5_GroupIds;
            moein.Tafsil6_GroupIds = dto.Tafsil6_GroupIds;
            moein.Tafsil7_GroupIds = dto.Tafsil7_GroupIds;
            moein.Tafsil8_GroupIds = dto.Tafsil8_GroupIds;

            _db.Update(moein);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" اطلاعات تفصیلی حساب {moein.MoeinName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }

        public async Task<clsResult> DeleteTafsilGroupAsync(int id)
        {
            clsResult result = new clsResult();
            result.Success = false;


            var TafsilGroup = await _db.Acc_Coding_TafsilGroups.FindAsync(id);
            if (TafsilGroup == null)
            {
                result.Message = $"اطلاعات موردنظر یافت نشد.";
                return result;
            }
            bool hasData = await _db.Acc_Coding_TafsilToGroups.AnyAsync(n => n.GroupId == id);
            if (hasData)
            {
                result.Message = $"گروه موردنظر در بانک اطلاعاتی دارای سابقه می باشد.";
                return result;
            }

            _db.Acc_Coding_TafsilGroups.Remove(TafsilGroup);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $" گروه تفصیلی  {TafsilGroup.GroupName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        //====== Tafsil
        public string TafsilCodeGenerator(long? sellerId)
        {
            string code;

            if (sellerId == null)
                code = "1";
            else
                code = sellerId + "1";


            string? lastCode = _db.Acc_Coding_Tafsils.Where(n => n.SellerId == sellerId)
                .Select(n => new { n.Code }).OrderByDescending(n => n.Code).FirstOrDefault()?.Code;

            if (string.IsNullOrEmpty(lastCode))
            {
                return code;
            }
            code = (Convert.ToInt64(lastCode) + 1).ToString();

            return code;
        }
        public async Task<TafsilDto> FindTafsilAsync(long id)
        {
            var x = await _db.Acc_Coding_Tafsils
                .Include(n => n.TafsilToGroups).ThenInclude(n => n.Group)
                .SingleOrDefaultAsync(n => n.Id == id);


            TafsilDto tafsil = new TafsilDto();
            tafsil.Id = x.Id;
            tafsil.Code = x.Code;
            tafsil.Name = x.Name;
            tafsil.Description = x.Description;
            tafsil.strGroupsId = x.GroupsId;
            tafsil.intGroupsId = x.TafsilToGroups.Select(x => x.GroupId).ToArray();
            tafsil.GroupsName = x.TafsilToGroups.Select(g => g.Group.GroupName).ToArray();

            return tafsil;
        }
        public async Task<List<TafsilDto>> TafsilsAsync(Int64? SellerId = null, string name = "")
        {
            var query = _db.Acc_Coding_Tafsils
                .Include(n => n.TafsilToGroups).ThenInclude(n => n.Group)
                .Where(n =>
                 (n.SellerId == SellerId)
                 && (n.Name.Contains(name)))
                .AsQueryable();

            List<TafsilDto> tafsils = new List<TafsilDto>();

            foreach (var x in query)
            {
                TafsilDto tafsil = new TafsilDto();
                tafsil.Id = x.Id;
                tafsil.Code = x.Code;
                tafsil.Name = x.Name;
                tafsil.Description = x.Description;
                tafsil.strGroupsId = x.GroupsId;
                tafsil.intGroupsId = x.TafsilToGroups.Select(x => x.GroupId).ToArray();
                tafsil.GroupsName = x.TafsilToGroups.Select(g => g.Group.GroupName).ToArray();

                tafsils.Add(tafsil);
            }

            return tafsils.OrderBy(n => n.Name).ToList();
        }
        public async Task<clsResult> AddTafsilAsync(TafsilDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var isDuolicate = await _db.Acc_Coding_Tafsils
                .AnyAsync(n => n.SellerId == dto.SellerId && n.Name == dto.Name);
            if (isDuolicate)
            {
                result.Message = $"حساب تفصیلی با نام '  {dto.Name}   ' قبلا تعریف شده است.";
                result.ShowMessage = true;
                return result;
            }

            if (string.IsNullOrEmpty(dto.Code))
                dto.Code = TafsilCodeGenerator(dto.SellerId.Value);
            var isDuolicateCode = await _db.Acc_Coding_Tafsils
              .AnyAsync(n => n.SellerId == dto.SellerId && n.Code == dto.Code);
            if (isDuolicateCode)
            {
                result.Message = $"حساب تفصیلی با کد '  {dto.Code}   ' قبلا تعریف شده است.";
                result.ShowMessage = true;
                return result;
            }

            Acc_Coding_Tafsil tafsil = new Acc_Coding_Tafsil();
            tafsil.Code = dto.Code;
            tafsil.Name = dto.Name;
            tafsil.Description = dto.Description;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.strGroupsId;


            _db.Acc_Coding_Tafsils.Add(tafsil);
            try
            {
                await _db.SaveChangesAsync();
                List<Acc_Coding_TafsilToGroup> groups = new List<Acc_Coding_TafsilToGroup>();

                if (dto.intGroupsId.Length > 0)
                {
                    for (int i = 0; i < dto.intGroupsId.Length; i++)
                    {
                        groups.Add(
                            new Acc_Coding_TafsilToGroup { GroupId = dto.intGroupsId[i], TafsilId = tafsil.Id });
                    }

                    var groupresult = await AddTafsilToGroupsAsync(groups);
                }
                result.Success = true;
                result.Message = $" حساب تفصیلی {tafsil.Name} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<long> AddTafsilReturnIdAsync(TafsilDto dto)
        {

            Acc_Coding_Tafsil tafsil = new Acc_Coding_Tafsil();
            tafsil.Code = TafsilCodeGenerator(dto.SellerId.Value);
            tafsil.Name = dto.Name;
            tafsil.Description = dto.Description;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.strGroupsId;

            _db.Acc_Coding_Tafsils.Add(tafsil);
            try
            {
                await _db.SaveChangesAsync();
                List<Acc_Coding_TafsilToGroup> groups = new List<Acc_Coding_TafsilToGroup>();

                if (dto.intGroupsId.Length > 0)
                {
                    for (int i = 0; i < dto.intGroupsId.Length; i++)
                    {
                        groups.Add(
                            new Acc_Coding_TafsilToGroup { GroupId = dto.intGroupsId[i], TafsilId = tafsil.Id });
                    }

                    var groupresult = await AddTafsilToGroupsAsync(groups);
                }

                return tafsil.Id;
            }
            catch
            {
                return 0;
            }

            return 0;
        }
        public TafsilDto? AddTafsil(TafsilDto dto)
        {
            clsResult result = new clsResult();

            Acc_Coding_Tafsil tafsil = new Acc_Coding_Tafsil();
            tafsil.Code = TafsilCodeGenerator(dto.SellerId.Value);
            tafsil.Name = dto.Name;
            tafsil.Description = dto.Description;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.strGroupsId;


            _db.Acc_Coding_Tafsils.Add(tafsil);
            try
            {
                _db.SaveChanges();
                dto.Id = tafsil.Id;
                dto.Code = tafsil.Code;
                List<Acc_Coding_TafsilToGroup> groups = new List<Acc_Coding_TafsilToGroup>();

                if (dto.intGroupsId.Length > 0)
                {
                    for (int i = 0; i < dto.intGroupsId.Length; i++)
                    {
                        groups.Add(
                            new Acc_Coding_TafsilToGroup { GroupId = dto.intGroupsId[i], TafsilId = tafsil.Id });
                    }

                    AddTafsilToGroups(groups);
                }
                return dto;
            }
            catch
            {

            }

            return null;
        }
        public async Task<clsAddFasliResultDto> AutoAddTafsilAsync(AutoAddTafsilDto dto)
        {
            clsAddFasliResultDto result = new clsAddFasliResultDto();
            result.Success = false;

            Acc_Coding_Tafsil tafsil = new Acc_Coding_Tafsil();
            tafsil.Code = tafsil.Code = TafsilCodeGenerator(dto.SellerId);
            tafsil.Name = dto.Name;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.GroupId.ToString();


            _db.Acc_Coding_Tafsils.Add(tafsil);
            try
            {
                await _db.SaveChangesAsync();
                Acc_Coding_TafsilToGroup group = new Acc_Coding_TafsilToGroup();
                group.TafsilId = tafsil.Id;
                group.GroupId = dto.GroupId;

                _db.Acc_Coding_TafsilToGroups.Add(group);
                _db.SaveChanges();

                result.TafsilCode = tafsil.Code;
                result.TafsilId = tafsil.Id;
                result.Success = true;
                result.Message = $" حساب تفصیلی {tafsil.Name} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ایجاد حساب تفصیلی رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> EditTafsilAsync(TafsilDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            Acc_Coding_Tafsil tafsil = await _db.Acc_Coding_Tafsils.FindAsync(dto.Id);
            if (tafsil == null)
            {
                result.Message = "اطلاعات یافت نشد";
                return result;
            }

            var isDuolicate = await _db.Acc_Coding_Tafsils
                .AnyAsync(n => n.Id != dto.Id && n.SellerId == dto.SellerId && n.Name == dto.Name);
            if (isDuolicate)
            {
                result.Message = $"حساب تفصیلی با نام '  {dto.Name}   ' قبلا تعریف شده است.";
                result.ShowMessage = true;
                return result;
            }

            if (string.IsNullOrEmpty(dto.Code))
                dto.Code = TafsilCodeGenerator(dto.SellerId.Value);
            var isDuolicateCode = await _db.Acc_Coding_Tafsils
              .AnyAsync(n => n.Id != dto.Id && n.SellerId == dto.SellerId && n.Code == dto.Code);
            if (isDuolicateCode)
            {
                result.Message = $"حساب تفصیلی با کد '  {dto.Code}   ' قبلا تعریف شده است.";
                result.ShowMessage = true;
                return result;
            }

            tafsil.Code = dto.Code;
            tafsil.Name = dto.Name;
            tafsil.Description = dto.Description;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.strGroupsId;

            _db.Acc_Coding_Tafsils.Update(tafsil);
            try
            {
                await _db.SaveChangesAsync();

                if (dto.intGroupsId.Length > 0)
                {
                    List<Acc_Coding_TafsilToGroup> groups = new List<Acc_Coding_TafsilToGroup>();
                    for (int i = 0; i < dto.intGroupsId.Length; i++)
                    {
                        groups.Add(
                            new Acc_Coding_TafsilToGroup { GroupId = dto.intGroupsId[i], TafsilId = tafsil.Id });
                    }

                    await AddTafsilToGroupsAsync(groups);
                }
                result.Success = true;
                result.Message = $" ویرایش حساب تفصیلی {tafsil.Name} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<bool> AddTafsilToGroupsAsync(List<Acc_Coding_TafsilToGroup> dto)
        {
            if (dto.Count == 0)
                return false;

            long tid = dto.FirstOrDefault().TafsilId;

            var OldRecords = await _db.Acc_Coding_TafsilToGroups.Where(n => n.TafsilId == tid).ToListAsync();
            try
            {
                if (OldRecords.Count > 0)
                {
                    _db.Acc_Coding_TafsilToGroups.RemoveRange(OldRecords);
                    await _db.SaveChangesAsync();
                }
                _db.Acc_Coding_TafsilToGroups.AddRange(dto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddTafsilToGroups(List<Acc_Coding_TafsilToGroup> dto)
        {
            if (dto.Count == 0)
                return false;

            long tid = dto.FirstOrDefault().TafsilId;

            var OldRecords = _db.Acc_Coding_TafsilToGroups.Where(n => n.TafsilId == tid).ToList();
            try
            {
                if (OldRecords.Count > 0)
                {
                    _db.Acc_Coding_TafsilToGroups.RemoveRange(OldRecords);
                    _db.SaveChanges();
                }
                _db.Acc_Coding_TafsilToGroups.AddRange(dto);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<SelectList> SelectList_TafsilsAsync(Int64? SellerId = null)
        {
            var lst = await _db.Acc_Coding_Tafsils
                .Where(n => n.SellerId == SellerId)
                 .Select(n => new { id = n.Id, name = n.Name }).OrderBy(n => n.name).ToListAsync();
            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageTafsilsAsync(Int64? SellerId = null)
        {
            // دریافت داده‌ها از پایگاه داده
            var lst = await _db.Acc_Articles
                .Where(n => SellerId != null ? n.SellerId == SellerId : n.Tafsil4Id > 0 && n.Tafsil4Id != null && n.IsDeleted == false)
                .Select(n => new { id = n.Tafsil4Id, name = n.Tafsil4Name })
                .Distinct()
                .OrderBy(x => x.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageTafsils5Async(Int64? SellerId = null)
        {
            // دریافت داده‌ها از پایگاه داده
            var lst = await _db.Acc_Articles
                .Where(n => SellerId != null ? n.SellerId == SellerId : n.Tafsil5Id > 0 && n.Tafsil5Id != null && n.IsDeleted == false)
                .Select(n => new { id = n.Tafsil5Id, name = n.Tafsil5Name })
                .Distinct()
                .OrderBy(x => x.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageTafsils6Async(Int64? SellerId = null)
        {
            // دریافت داده‌ها از پایگاه داده
            var lst = await _db.Acc_Articles
                .Where(n => SellerId != null ? n.SellerId == SellerId : n.Tafsil6Id > 0 && n.Tafsil6Id != null && n.IsDeleted == false)
                .Select(n => new { id = n.Tafsil6Id, name = n.Tafsil6Name })
                .Distinct()
                .OrderBy(x => x.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }

        public async Task<string?> GetTafsilCodByIdAsync(long tafsilId)
        {
            var tafsil = await _db.Acc_Coding_Tafsils.SingleOrDefaultAsync(n => n.Id == tafsilId);
            string? code = tafsil?.Code;
            return code;
        }
        public async Task<string?> GetTafsilNameByIdAsync(long? tafsilId)
        {
            if (tafsilId == null || tafsilId == 0)
                return null;

            var tafsil = await _db.Acc_Coding_Tafsils.SingleOrDefaultAsync(n => n.Id == tafsilId);
            string? name = tafsil?.Name;
            return name;
        }
        public async Task<clsResult> CreateAccountBankTafsilAsync(BankAccountTafsilDto dto)
        {
            clsResult result = new clsResult();

            Acc_Coding_Tafsil tafsil = new Acc_Coding_Tafsil();
            tafsil.Code = TafsilCodeGenerator(dto.SellerId.Value);
            tafsil.Name = dto.Name;
            tafsil.Description = dto.Description;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.strGroupsId;


            _db.Acc_Coding_Tafsils.Add(tafsil);
            try
            {
                await _db.SaveChangesAsync();
                List<Acc_Coding_TafsilToGroup> groups = new List<Acc_Coding_TafsilToGroup>();
                var bankAccount = _db.BankAccounts.SingleOrDefault(n => n.Id == dto.BankAccountId);
                if (bankAccount != null)
                {
                    bankAccount.TafsilCode = tafsil.Code;
                    bankAccount.TafsilId = tafsil.Id;
                    _db.BankAccounts.Update(bankAccount);
                    _db.SaveChanges();
                }

                if (dto.intGroupsId.Length > 0)
                {
                    for (int i = 0; i < dto.intGroupsId.Length; i++)
                    {
                        groups.Add(
                            new Acc_Coding_TafsilToGroup { GroupId = dto.intGroupsId[i], TafsilId = tafsil.Id });
                    }

                    AddTafsilToGroupsAsync(groups);
                }
                result.Success = true;
                result.Message = $" حساب تفصیلی {tafsil.Name} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<List<TafsilListDto>> GetTafsilsByGroupAsync(int[] groupIds, long sellerId)
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

            return data;
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
        public async Task<MoeinSelectListTafsilDto> GetMoeinTafsilsAsync(int MoeinId, long sellerId)
        {
            MoeinSelectListTafsilDto tafsils = new MoeinSelectListTafsilDto();
            if (MoeinId == 0)
                return null;

            var moein = await GetAccountTafsilAsync(MoeinId);
            if (moein.Tafsil4_Array?.Length > 0)
                tafsils.Tafsil4 = new SelectList(await GetTafsilsByGroupAsync(moein.Tafsil4_Array, sellerId), "id", "name");

            if (moein.Tafsil5_Array?.Length > 0)
                tafsils.Tafsil5 = new SelectList(await GetTafsilsByGroupAsync(moein.Tafsil5_Array, sellerId), "id", "name");

            if (moein.Tafsil6_Array?.Length > 0)
                tafsils.Tafsil6 = new SelectList(await GetTafsilsByGroupAsync(moein.Tafsil6_Array, sellerId), "id", "name");

            if (moein.Tafsil7_Array?.Length > 0)
                tafsils.Tafsil7 = new SelectList(await GetTafsilsByGroupAsync(moein.Tafsil7_Array, sellerId), "id", "name");

            if (moein.Tafsil8_Array?.Length > 0)
                tafsils.Tafsil8 = new SelectList(await GetTafsilsByGroupAsync(moein.Tafsil8_Array, sellerId), "id", "name");

            return tafsils;
        }
        public async Task<clsResult> CreatePersonTafsilAsync(SetTafsilDto dto)
        {
            clsResult result = new clsResult();

            Acc_Coding_Tafsil tafsil = new Acc_Coding_Tafsil();
            tafsil.Code = TafsilCodeGenerator(dto.SellerId.Value);
            tafsil.Name = dto.Name;
            tafsil.Description = dto.Description;
            tafsil.SellerId = dto.SellerId;
            tafsil.GroupsId = dto.strGroupsId;

            _db.Acc_Coding_Tafsils.Add(tafsil);
            try
            {
                await _db.SaveChangesAsync();
                List<Acc_Coding_TafsilToGroup> groups = new List<Acc_Coding_TafsilToGroup>();
                var person = _db.parties.SingleOrDefault(n => n.Id == dto.longId);
                if (person != null)
                {
                    person.TafsilCode = tafsil.Code;
                    person.TafsilId = tafsil.Id;
                    _db.parties.Update(person);
                    _db.SaveChanges();
                }

                if (dto.intGroupsId.Length > 0)
                {
                    for (int i = 0; i < dto.intGroupsId.Length; i++)
                    {
                        groups.Add(
                            new Acc_Coding_TafsilToGroup { GroupId = dto.intGroupsId[i], TafsilId = tafsil.Id });
                    }

                    var addtogroupsResult = await AddTafsilToGroupsAsync(groups);
                }
                result.Success = true;
                result.Message = $" حساب تفصیلی {tafsil.Name} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> CreatBulkPersonTafsilAsync(List<long> persenId, long sellerId)
        {
            var result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;

            List<Acc_Coding_TafsilToGroup> lstGroups = new List<Acc_Coding_TafsilToGroup>();

            for (int i = 0; i < persenId.Count; i++)
            {
                var person = await _db.parties.FindAsync(persenId[i]);
                if (person != null && person.TafsilId == null)
                {
                    var tafsil = new TafsilDto
                    {
                        Code = TafsilCodeGenerator(sellerId),
                        Name = person.Name,
                        SellerId = sellerId,
                        strGroupsId = "1",
                        IsPerson = true,
                        intGroupsId = new int[] { 1 },
                    };
                    var tafsilDto = AddTafsil(tafsil);
                    person.TafsilId = tafsilDto.Id;
                    person.TafsilCode = tafsilDto.Code;
                    _db.parties.Update(person);
                    _db.SaveChanges();

                    //if (tafsilDto != null)
                    //{
                    //    var group = new Acc_Coding_TafsilToGroup()
                    //    {
                    //        GroupId = 1,
                    //        TafsilId = tafsilDto.Id,
                    //    };
                    //    lstGroups.Add(group);
                    //}
                }
                //if (lstGroups.Count > 0)
                //{
                //    _db.Acc_Coding_TafsilToGroups.AddRange(lstGroups);
                //    await _db.SaveChangesAsync();
                //    result.Success = true;
                //    result.Message = "حساب های تفصیلی اشخاص با موفقیت ایجاد شد";

                //}
            }
            return result;
        }
        public async Task<long?> CheckAddTafsilAsync(string tafsilName, long sellerId)
        {
            var tafsil = await _db.Acc_Coding_Tafsils.Where(n => n.SellerId == sellerId && n.Name == tafsilName).FirstOrDefaultAsync();
            if (tafsil == null)
            {
                TafsilDto td = new TafsilDto();
                td.SellerId = sellerId;
                td.Name = tafsilName;
                td.strGroupsId = "[1,7]";
                td.intGroupsId = new int[] { 1, 7 };
                td.IsPerson = true;
                td.Code = TafsilCodeGenerator(sellerId);

                long? tid = await AddTafsilReturnIdAsync(td);
                return tid;
            }
            return tafsil.Id;
        }
        public async Task<clsResult> DeleteTafsilAccountAsync(long id)
        {
            var result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;

            var tafsil = await _db.Acc_Coding_Tafsils.FindAsync(id);
            if (tafsil == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد.";
                return result;
            }

            bool hasData = await _db.Acc_Articles
                 .Where(n => n.Tafsil4Id == id
                 || n.Tafsil5Id == id
                 || n.Tafsil6Id == id
                 || n.Tafsil7Id == id
                 || n.Tafsil8Id == id).AnyAsync();

            if (hasData)
            {
                result.Message = "حساب موردنظر در بانک اطلاعاتی دارای سابقه است و امکان حذف آن وجود ندارد.";
                return result;
            }
            var tafsilIngroups = await _db.Acc_Coding_TafsilToGroups.Where(n => n.TafsilId == id).ToListAsync();
            if (tafsilIngroups.Count > 0)
                _db.Acc_Coding_TafsilToGroups.RemoveRange(tafsilIngroups);
            _db.Acc_Coding_Tafsils.Remove(tafsil);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "حساب موردنظر با موفقیت حذف شد";
                return result;
            }
            catch (Exception)
            {
                result.Message = "هنگام حذف اطلاعات خطایی رخ داده است";
                return result;
            }


        }

        //===== Finance Periods
        public async Task<List<FinancePeriodsDto>> FinancePeriodsAsync(Int64 SellerId, string name = "")
        {
            var priods = await _db.Acc_FinancialPeriods
                .Where(n =>
                 (n.SellerId == SellerId)
                 && (n.Name.Contains(name)))
                .Select(n => new FinancePeriodsDto
                {
                    Id = n.Id,
                    Name = n.Name,
                    Description = n.Description,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    strEndDate = n.EndDate.LatinToPersian(),
                    strStartDate = n.StartDate.LatinToPersian(),
                    DefualtVatRate = n.DefualtVatRate,
                    SellerId = n.SellerId,

                }).OrderBy(n => n.StartDate).ToListAsync();
            return priods;
        }
        public async Task<FinancePeriodsDto> GetFinanceDtoAsync(int id)
        {
            var period = await _db.Acc_FinancialPeriods.FindAsync(id);
            FinancePeriodsDto dto = new FinancePeriodsDto();
            dto.Id = id;
            dto.DefualtVatRate = period.DefualtVatRate;
            dto.Name = period.Name;
            dto.Description = period.Description;
            dto.strStartDate = period.StartDate.LatinToPersian();
            dto.strEndDate = period.EndDate.LatinToPersian();
            dto.StartDate = period.StartDate;
            dto.EndDate = period.EndDate;
            dto.SellerId = period.SellerId;

            return dto;

        }
        public async Task<clsResult> AddPeriodAsync(FinancePeriodsDto dto)
        {
            clsResult result = new clsResult();

            Acc_FinancialPeriod period = new Acc_FinancialPeriod();
            period.StartDate = dto.StartDate;
            period.EndDate = dto.EndDate;
            period.Name = dto.Name;
            period.Description = dto.Description;
            period.DefualtVatRate = dto.DefualtVatRate;
            period.SellerId = dto.SellerId;

            if (period.StartDate >= period.EndDate)
            {
                result.Success = false;
                result.Message = "خطای تاریخ : تاریخ شروع دوره نباید کوچکتر یا مساوی با تاریخ پایان دوره باشد.";
                return result;
            }

            _db.Acc_FinancialPeriods.Add(period);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دوره مالی جدید با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> EditPeriodAsync(FinancePeriodsDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            Acc_FinancialPeriod period = await _db.Acc_FinancialPeriods.FindAsync(dto.Id);
            if (period == null)
            {
                result.Message = "اطلاعات یافت نشد";
                return result;
            }
            period.StartDate = dto.StartDate;
            period.EndDate = dto.EndDate;
            period.Name = dto.Name;
            period.Description = dto.Description;
            period.DefualtVatRate = dto.DefualtVatRate;
            period.SellerId = dto.SellerId;

            if (dto.EndDate <= dto.StartDate)
            {
                result.Success = false;
                result.Message = "خطای تاریخ : تاریخ شروع دوره نباید کوچکتر یا مساوی با تاریخ پایان دوره باشد.";
                return result;
            }

            _db.Acc_FinancialPeriods.Update(period);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"اطلاعات دوره مالی با موفقیت ویرایش شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ویرایش اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> DeleteThePeriodAsync(int id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;

            var per = await _db.Acc_FinancialPeriods.FindAsync(id);
            if (per == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }

            if (_db.Acc_Documents.Any(n => n.PeriodId == id))
            {
                result.Message = "دوره موردنظر در بانک اطلاعاتی دارای سابقه می باشد";
                return result;
            }

            _db.Acc_FinancialPeriods.Remove(per);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دوره مورنظر با موفقیت حذف شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در حذف اطلاعات رخ داده است. " + x.Message;
            }

            return result;

        }
        public async Task<SelectList> SelectList_FinancePeriodAsync(Int64 SellerId)
        {
            var periods = await _db.Acc_FinancialPeriods
                 .Where(n =>
                  (n.SellerId == SellerId))
                 .Select(n => new FinancePeriodsDto
                 {
                     Id = n.Id,
                     Name = n.Name,
                 }).ToListAsync();

            return new SelectList(periods, "Id", "Name");
        }
        public async Task<bool> SetSellerPeriodAsync(string username, int periodId)
        {
            var sellerSetting = await _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == username);
            if (sellerSetting == null) return false;
            sellerSetting.ActiveFinancePeriodId = periodId;
            _db.UserSettings.Update(sellerSetting);

            return Convert.ToBoolean(await _db.SaveChangesAsync());

        }

        public async Task<bool> UpdateTafsilCodeAsync(long sellerId)
        {
            var tafsils = await _db.Acc_Coding_Tafsils.Where(n => n.SellerId == sellerId).Distinct().ToListAsync();
            foreach (var tafsil in tafsils)
            {
                await _db.Acc_Articles
                      .Where(n => n.Doc.SellerId == sellerId && n.Tafsil4Name == tafsil.Name)
                      .ExecuteUpdateAsync(n => n.SetProperty(x => x.Tafsil4Id, tafsil.Id));
            }
            return true;
        }
    }
}
