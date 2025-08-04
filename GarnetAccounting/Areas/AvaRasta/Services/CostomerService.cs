using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Areas.CustomerArea.Dto;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.AvaRasta.Services
{
    //Https://tp.tax.gov.ir/req/api/self-tsp/sync/GET_SERVER_INFORMATION/
    public class CostomerService : ICostomerService
    {
        private readonly AppDbContext _db;
        private readonly IAppIdentityUserManager _userManager;

        public CostomerService(AppDbContext db, IAppIdentityUserManager userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IQueryable<VmCustomer> GetCustomers(string name = "")
        {
            return _db.Customers
                .Include(n => n.CustomerUsers)
                 .Where(n => (n.FName.Contains(name) || n.LName.Contains(name) || n.Title.Contains(name)))
                 .Select(n => new VmCustomer
                 {
                     FName = n.FName,
                     LName = n.LName,
                     Title = n.Title,
                     Email = n.Email,
                     ActivationDate = n.ActivationDate,
                     Address = n.Address,
                     City = n.City,
                     EconomicNumber = n.EconomicNumber,
                     Fax = n.Fax,
                     Id = n.Id,
                     IsActive = n.IsActive,
                     Ishoghooghi = n.Ishoghooghi,
                     LastUpdate = n.LastUpdate,
                     LicenseCount = n.LicenseCount,
                     LicenseExpireDate = n.licenseExpierDate,
                     Mobile = n.Mobile,
                     NationalId = n.NationalId,
                     Phone = n.Phone,
                     PostalCode = n.PostalCode,
                     RegisterDate = n.RegisterDate,
                     RegistrationNumber = n.RegistrationNumber,
                     State = n.State,
                     UserCreator = n.UserCreator,
                     Username = n.CustomerUsers.Where(u => u.CustomerId == n.Id).FirstOrDefault().UserName,
                     UserCount = n.CustomerUsers.Count(),

                 }).AsQueryable();

        }
        public async Task<VmCustomer> GetVmCustomerByIdAsync(int Id)
        {
            var n = await _db.Customers.Include(x => x.CustomerUsers).SingleOrDefaultAsync(x => x.Id == Id);
            VmCustomer c = new VmCustomer();
            c.Id = n.Id;
            c.FName = n.FName;
            c.LName = n.LName;
            c.Title = n.Title;
            c.Email = n.Email;
            c.Phone = n.Phone;
            c.Mobile = n.Mobile;
            c.Address = n.Address;
            c.Ishoghooghi = n.Ishoghooghi;
            c.NationalId = n.NationalId;
            c.EconomicNumber = n.EconomicNumber;
            c.RegistrationNumber = n.RegistrationNumber;

            c.LicenseCount = n.LicenseCount;
            c.InvoiceCountLimit = n.InvoiceCountLimit;
            c.ActivationDate = n.ActivationDate;
            c.LicenseExpireDate = n.licenseExpierDate;

            c.RegisterDate = n.RegisterDate;
            c.UserCreator = n.UserCreator;
            c.IsActive = n.IsActive;
            c.Username = n.CustomerUsers?.Where(n => n.CustomerId == Id).FirstOrDefault()?.UserName;
            return c;
        }
        public async Task<ResultDto> AddCustomerAsync(VmCustomer n)
        {
            ResultDto result = new ResultDto();
            result.Success = true;
            result.Message = "عملیات ثبت مشتری با موفقیت انجام شد";

            Customer c = new Customer();
            c.FName = n.FName;
            c.LName = n.LName;
            c.Title = n.Title;
            c.Email = n.Email;
            c.Phone = n.Phone;
            c.Mobile = n.Mobile;
            c.Address = n.Address;
            c.Ishoghooghi = n.Ishoghooghi;
            c.NationalId = n.NationalId;
            c.EconomicNumber = n.EconomicNumber;
            c.RegistrationNumber = n.RegistrationNumber;

            c.LicenseCount = n.LicenseCount;
            c.InvoiceCountLimit = n.InvoiceCountLimit;
            c.ActivationDate = n.ActivationDate;
            c.licenseExpierDate = n.LicenseExpireDate;

            c.RegisterDate = n.RegisterDate;
            c.UserCreator = n.UserCreator;
            c.IsActive = n.IsActive;

            _db.Customers.Add(c);
            try
            {
                if (!Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = false;
                    result.Message = "در عملیات ثبت مشتری مشکلی رخ داده است. اطلاعات .ارد شده را بررسی کنید";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "در عملیات ثبت مشتری مشکلی رخ داده است. برای حل آن با پشتیبان سیستم تماس بگیرید";

            }
            return result;
        }
        public async Task<ResultDto> UpdateCustomerAsync(VmCustomer n)
        {
            ResultDto result = new ResultDto();
            if (n == null)
            {
                result.Success = false;
                result.Message = "اطلاعات یافت نشد";
                return result;
            }

            Customer c = await _db.Customers.FindAsync(n.Id);
            if (c == null)
            {
                result.Success = false;
                result.Message = "اطلاعات یافت نشد";
                return result;
            }
            c.FName = n.FName;
            c.LName = n.LName;
            c.Title = n.Title;
            c.Email = n.Email;
            c.Phone = n.Phone;
            c.Mobile = n.Mobile;
            c.Address = n.Address;
            c.Ishoghooghi = n.Ishoghooghi;
            c.NationalId = n.NationalId;
            c.EconomicNumber = n.EconomicNumber;
            c.RegistrationNumber = n.RegistrationNumber;

            c.LicenseCount = n.LicenseCount;
            c.InvoiceCountLimit = n.InvoiceCountLimit;

            c.LastUpdate = DateTime.Now;
            c.IsActive = n.IsActive;

            _db.Customers.Update(c);
            try
            {
                Convert.ToBoolean(await _db.SaveChangesAsync());

                result.Success = true;
                result.Message = "اطلاعات مشتری با موفقیت بروز شد";

            }
            catch
            {
                result.Success = false;
                result.Message = "در عملیات انجام مشکلی رخ داده است. برای حل آن با پشتیبان سیستم تماس بگیرید";

            }
            return result;
        }
        public async Task<ResultDto> DeleteCustomerAsync(int id)
        {

            var customer = await _db.Customers.FindAsync(id);
            if (customer == null)
            {
                return new ResultDto() { Success = false, Message = "موردی با این شناسه یافت نشد" };
            }
            bool hasRecord = false;
            if (await _db.parties.AnyAsync(n => n.CustomerId == id))
                hasRecord = true;
            if (hasRecord)
            {
                return new ResultDto() { Success = false, Message = "مشتری موردنظر در بانک اطلاعاتی دارای سابقه است و امکان حذف آن وجود ندارد" };
            }
            int userCount = _db.Users.Where(n => n.CustomerId == id).Count();
            if (userCount > 0)
            {
                return new ResultDto() { Success = false, Message = $"برای مشتری موردنظر {userCount} کاربر در سیستم معرفی شده است. ابتدا باید کاربران را حذف نمایید." };
            }
            _db.Customers.Remove(customer);
            try
            {
                Convert.ToBoolean(await _db.SaveChangesAsync());
                return new ResultDto() { Success = true, Message = "ردیف موردنظر با موفقیت از بانک اطلاعاتی حذف شد" };
            }
            catch (Exception x)
            {
                string error = x.Message;
            }
            return new ResultDto() { Success = false, Message = "خطایی حین عملیات رخ داده است." };
        }

        //
        public async Task<List<VmCustomerUsers>> GetCustomerUsersAsync(int? CustomerId)
        {
            List<VmCustomerUsers> lst = new List<VmCustomerUsers>();
            if (CustomerId != null)
            {

                var users = await _userManager.Users.Where(n => n.CustomerId == CustomerId).ToListAsync();

                foreach (var usr in users)
                {
                    var sett = _db.UserSettings.FirstOrDefault(n => n.UserName == usr.UserName);
                    var setting = new VmCustomerUsers();
                    setting.UserId = usr.Id;
                    setting.UserName = usr.UserName;
                    setting.FullName = usr.FName + " " + usr.Family;
                    setting.CreationDate = usr.RegistrDate.LatinToPersian();
                    setting.phoneNumber = usr.Mobile;
                    setting.email = usr.Email;
                    setting.Image = usr.Avatar;
                    setting.UserRolesArry = await _userManager.userArrayRolesDescAsync(usr.UserName);
                    setting.IsActive = usr.IsActive;
                    setting.Gender = usr.Gender;

                    if (sett != null)
                    {
                        setting.AllowBuyerManagement = sett.AllowBuyerManagement;
                        setting.AllowSaleManagement = sett.AllowSaleManagement;
                        setting.AllowSellerManagement = sett.AllowSellerManagement;
                        setting.AllowStuffManagement = sett.AllowStuffManagement;
                        setting.AllowUserManagement = sett.AllowUserManagement;
                    }
                    lst.Add(setting);
                }
            }

            return lst;
        }

    }
}
