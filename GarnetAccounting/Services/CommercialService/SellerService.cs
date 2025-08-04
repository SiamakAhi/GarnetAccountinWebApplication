using GarnetAccounting.Classes;
using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Models;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Services.CommercialService
{
    public class SellerService : ISellerService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SellerService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<ResultDto> AddSeller(AddSellerDto dto)
        {
            ResultDto result = new ResultDto();

            var CustomerInfo = await _db.Customers.FindAsync(dto.CustomerId);
            int limitAddSeller = CustomerInfo.LicenseCount;
            int UsedLisence = _db.parties.Where(n => n.CustomerId == dto.CustomerId && n.Role == 1).Count();

            if (limitAddSeller <= UsedLisence)
            {
                result.Success = false;
                result.Message = "محدودیت تعریف فروشنده !"; ;
                return result;
            }

            string SavePath = string.Empty;
            string NewFileName = string.Empty;
            Party seller = new Party();

            if (dto.LogoFile != null)
            {
                string FileExtension = Path.GetExtension(dto.LogoFile.FileName);
                NewFileName = string.Concat("logo-", dto.NationalId, FileExtension);
                SavePath = $"{_env.WebRootPath}/img/Logos/{NewFileName}";

                using (var Stream = new FileStream(SavePath, FileMode.Create))
                {
                    await dto.LogoFile.CopyToAsync(Stream);
                }
            }

            string privateKeySavePath = string.Empty;
            string privateKeyFileName = string.Empty;
            string privateKeyContent = string.Empty;
            if (dto.SellerPrivateKeyFile != null)
            {
                string FileExtension = Path.GetExtension(dto.SellerPrivateKeyFile.FileName);
                string uniqiuName = $"{dto.fullNameEn}_{dto.NationalId}_PrivateKey";
                privateKeyFileName = $"{uniqiuName}{FileExtension}";
                privateKeySavePath = $"{_env.WebRootPath}/sigF/{privateKeyFileName}";

                if (File.Exists(privateKeySavePath))
                {
                    File.Delete(privateKeySavePath);
                }
                using (var stream = new FileStream(privateKeySavePath, FileMode.Create))
                {
                    await dto.SellerPrivateKeyFile.CopyToAsync(stream);
                }

                var keyContent = await GetKeyString.ProcessFileAsync(dto.SellerPrivateKeyFile);
                privateKeyContent = keyContent.Content;

                seller.SellerPrivateAddress = privateKeyFileName;
                seller.SellerPrivateKey = privateKeyContent;
            }
            //....................................................
            string CsrSavePath = string.Empty;
            string CsrKeyFileName = string.Empty;
            string CsrKeyContent = string.Empty;
            if (dto.SellerPrivateKeyFile != null)
            {
                string FileExtension = Path.GetExtension(dto.SellerCSRFile.FileName);
                string uniqiuName = $"{dto.fullNameEn}_{dto.NationalId}_CSR";
                CsrKeyFileName = $"{uniqiuName}{FileExtension}";
                CsrSavePath = $"{_env.WebRootPath}/sigF/{CsrKeyFileName}";

                if (File.Exists(CsrSavePath))
                {
                    File.Delete(CsrSavePath);
                }
                using (var stream = new FileStream(CsrSavePath, FileMode.Create))
                {
                    await dto.SellerCSRFile.CopyToAsync(stream);
                }
                var keyContent = await GetKeyString.ProcessFileAsync(dto.SellerCSRFile);
                CsrKeyContent = keyContent.Content;

                seller.SellerCSRKeyAddress = CsrKeyFileName;
                seller.SellerCSRKey = CsrKeyContent;

            }
            //..................................................
            //...................................................
            string PublicKeyFileName = string.Empty;
            string PublicKeySavePath = string.Empty;
            string PublicKeyContent = string.Empty;
            if (dto.SellerPrivateKeyFile != null)
            {
                string uniqiuName = $"{dto.fullNameEn}_{dto.NationalId}_PublicKey";
                PublicKeyFileName = $"{uniqiuName}.pem";
                PublicKeySavePath = $"{_env.WebRootPath}/sigF/{PublicKeyFileName}";

                if (File.Exists(PublicKeySavePath))
                {
                    File.Delete(PublicKeySavePath);
                }
                using (var stream = new FileStream(PublicKeySavePath, FileMode.Create))
                {
                    await dto.SellerPublicFile.CopyToAsync(stream);
                }
                var keyContent = await GetKeyString.ProcessFileAsync(dto.SellerPublicFile);
                PublicKeyContent = keyContent.Content;

                seller.SellerPublicKeyAddress = PublicKeyFileName;
                seller.SellerPublicKey = PublicKeyContent;

            }

            result.Success = false;
            result.Message = "خطایی در ثبت اطلاعات فروشنده رخ داده است";
            //..

            seller.CustomerId = dto.CustomerId;
            seller.Role = (Int16)PartyRole.Seller;

            seller.LegalStatus = dto.LegalStatus;
            seller.Name = dto.Name;
            seller.fullNameEn = dto.fullNameEn;
            seller.NationalId = dto.NationalId;
            seller.EconomicCode = dto.EconomicCode;
            seller.PostalCode = dto.PostalCode;
            seller.RegistrationNumber = dto.RegistrationNumber;

            seller.Province = dto.Province;
            seller.City = dto.City;
            seller.Address = dto.Address;
            seller.Fax = dto.Fax;
            seller.MobilePhone = dto.MobilePhone;

            seller.TaxMemoryId = dto.TaxMemoryId;
            seller.Title = dto.Name;

            seller.AccountingSystemId = dto.AccountingSystemId;
            seller.InvoiceDescription = dto.InvoiceDescription;

            if (!string.IsNullOrEmpty(NewFileName))
                seller.Logo = NewFileName;

            _db.parties.Add(seller);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "فروشنده جدید با موفقیت ثبت شد";
                }
            }
            catch
            {
            }

            return result;
        }

        public async Task<List<VmSeller>> GetCustomerSellers(int customerId)
        {
            List<VmSeller> sellers = await _db.parties.Where(n => n.Role == 1 && n.CustomerId == customerId)
                .Select(n => new VmSeller
                {
                    Id = n.Id,
                    CustomerId = customerId,
                    Address = n.Address,
                    City = n.City,
                    EconomicCode = n.EconomicCode,
                    LegalStatus = n.LegalStatus,
                    Logo = n.Logo,
                    Name = n.Name,
                    NationalId = n.NationalId,
                    Province = n.Province,
                    RegistrationNumber = n.RegistrationNumber,
                    TaxMemoryId = n.TaxMemoryId,
                    fullNameEn = n.fullNameEn,
                    SellerPrivateKey = n.SellerPrivateKey,
                    SellerCSRKey = n.SellerCSRKey,
                    SellerPublicKey = n.SellerPublicKey,
                    PostalCode = n.PostalCode,
                    TaxAuditor = n.TaxAuditor,
                    TaxFileNumber = n.TaxFileNumber,
                    TaxTrackingNumber = n.TaxTrackingNumber,
                    TaxUnitAddress = n.TaxUnitAddress,
                    TaxUnitCode = n.TaxUnitCode,
                    CEOContactNumber = n.CEOContactNumber,
                    CEOName = n.CEOName,
                    TaxPanelPassword = n.TaxPanelPassword,
                }).ToListAsync();

            return sellers;
        }

        public async Task<UpdateSellerDto> GetSellerByIdAsync(Int64 SellerId)
        {
            UpdateSellerDto SellerDto = new UpdateSellerDto();

            var seller = await _db.parties.SingleOrDefaultAsync(n => n.Id == SellerId);
            SellerDto.Id = seller.Id;
            SellerDto.Name = seller.Name;
            SellerDto.NationalId = seller.NationalId;
            SellerDto.SellerCSRKey = seller.SellerCSRKey;
            SellerDto.Address = seller.Address;
            SellerDto.City = seller.City;
            SellerDto.RegistrationNumber = seller.RegistrationNumber;
            SellerDto.CustomerId = seller.CustomerId;
            SellerDto.TaxMemoryId = seller.TaxMemoryId;
            SellerDto.AccountingSystemId = seller.AccountingSystemId;
            SellerDto.EconomicCode = seller.EconomicCode;
            SellerDto.Province = seller.Province;
            SellerDto.Fax = seller.Fax;
            SellerDto.MobilePhone = seller.MobilePhone;
            SellerDto.InvoiceDescription = seller.InvoiceDescription;
            SellerDto.LegalStatus = seller.LegalStatus;
            SellerDto.SellerPrivateKey = seller.SellerPrivateKey;
            SellerDto.SellerCSRKey = seller.SellerCSRKey;
            SellerDto.SellerPublicKey = seller.SellerPublicKey;
            SellerDto.Logo = seller.Logo;
            SellerDto.fullNameEn = seller.fullNameEn;
            SellerDto.PostalCode = seller.PostalCode;

            SellerDto.CEOName = seller.CEOName;
            SellerDto.CEOContactNumber = seller.CEOContactNumber;
            SellerDto.TaxFileNumber = seller.TaxFileNumber;
            SellerDto.TaxTrackingNumber = seller.TaxTrackingNumber;
            SellerDto.TaxUnitCode = seller.TaxUnitCode;
            SellerDto.TaxUnitAddress = seller.TaxUnitAddress;
            SellerDto.TaxAuditor = seller.TaxAuditor;
            SellerDto.TaxPanelPassword = seller.TaxPanelPassword;
            return SellerDto;

        }

        public async Task<ResultDto> UpdateSeller(UpdateSellerDto dto)
        {
            ResultDto result = new ResultDto()
            {
                Success = false,
                ExceptionError = null,
                Message = "در بروزرسانی اطلاعات خطایی رخ داده است."
            };

            if (dto == null) { return result; }

            var seller = await _db.parties.FindAsync(dto.Id);
            //
            string SavePath = string.Empty;
            string NewFileName = string.Empty;


            if (dto.LogoFile != null)
            {
                string FileExtension = Path.GetExtension(dto.LogoFile.FileName);
                NewFileName = string.Concat("logo-", dto.NationalId, FileExtension);
                SavePath = $"{_env.WebRootPath}/img/Logos/{NewFileName}";

                if (File.Exists(SavePath))
                {
                    File.Delete(SavePath);
                }
                using (var Stream = new FileStream(SavePath, FileMode.Create))
                {
                    await dto.LogoFile.CopyToAsync(Stream);
                }
            }
            //....................................................
            string CsrSavePath = string.Empty;
            string CsrKeyFileName = string.Empty;
            string CsrKeyContent = string.Empty;
            if (dto.SellerPrivateKeyFile != null)
            {
                string FileExtension = Path.GetExtension(dto.SellerCSRFile.FileName);
                string uniqiuName = $"{dto.fullNameEn}_{dto.NationalId}_CSR";
                CsrKeyFileName = $"{uniqiuName}{FileExtension}";
                CsrSavePath = $"{_env.WebRootPath}/sigF/{CsrKeyFileName}";

                if (File.Exists(CsrSavePath))
                {
                    File.Delete(CsrSavePath);
                }
                using (var stream = new FileStream(CsrSavePath, FileMode.Create))
                {
                    await dto.SellerCSRFile.CopyToAsync(stream);
                }
                var keyContent = await GetKeyString.ProcessFileAsync(dto.SellerCSRFile);
                CsrKeyContent = keyContent.Content;

                seller.SellerCSRKeyAddress = CsrKeyFileName;
                seller.SellerCSRKey = CsrKeyContent;
            }
            //..................................................
            string privateKeySavePath = string.Empty;
            string privateKeyFileName = string.Empty;
            string privateKeyContent = string.Empty;
            if (dto.SellerPrivateKeyFile != null)
            {
                string FileExtension = Path.GetExtension(dto.SellerPrivateKeyFile.FileName);
                string uniqiuName = $"{dto.fullNameEn}_{dto.NationalId}_PrivateKey";
                privateKeyFileName = $"{uniqiuName}{FileExtension}";
                privateKeySavePath = $"{_env.WebRootPath}/sigF/{privateKeyFileName}";

                if (File.Exists(privateKeySavePath))
                {
                    File.Delete(privateKeySavePath);
                }
                using (var stream = new FileStream(privateKeySavePath, FileMode.Create))
                {
                    await dto.SellerPrivateKeyFile.CopyToAsync(stream);
                }

                var keyContent = await GetKeyString.ProcessFileAsync(dto.SellerPrivateKeyFile);
                privateKeyContent = keyContent.Content;

                seller.SellerPrivateAddress = privateKeyFileName;
                seller.SellerPrivateKey = privateKeyContent;
            }
            //...................................................
            string PublicKeyFileName = string.Empty;
            string PublicKeySavePath = string.Empty;
            string PublicKeyContent = string.Empty;
            if (dto.SellerPrivateKeyFile != null)
            {
                string uniqiuName = $"{dto.fullNameEn}_{dto.NationalId}_PublicKey";
                PublicKeyFileName = $"{uniqiuName}.pem";
                PublicKeySavePath = $"{_env.WebRootPath}/sigF/{PublicKeyFileName}";

                if (File.Exists(PublicKeySavePath))
                {
                    File.Delete(PublicKeySavePath);
                }
                using (var stream = new FileStream(PublicKeySavePath, FileMode.Create))
                {
                    await dto.SellerPublicFile.CopyToAsync(stream);
                }
                var keyContent = await GetKeyString.ProcessFileAsync(dto.SellerPublicFile);
                PublicKeyContent = keyContent.Content;

                seller.SellerPublicKeyAddress = PublicKeyFileName;
                seller.SellerPublicKey = PublicKeyContent;

            }

            seller.CustomerId = dto.CustomerId;

            seller.LegalStatus = dto.LegalStatus;
            seller.Name = dto.Name;
            seller.fullNameEn = dto.fullNameEn;
            seller.NationalId = dto.NationalId;
            seller.EconomicCode = dto.EconomicCode;
            seller.PostalCode = dto.PostalCode;
            seller.RegistrationNumber = dto.RegistrationNumber;

            seller.Province = dto.Province;
            seller.City = dto.City;
            seller.Address = dto.Address;
            seller.Fax = dto.Fax;
            seller.MobilePhone = dto.MobilePhone;

            seller.TaxMemoryId = dto.TaxMemoryId;

            seller.AccountingSystemId = dto.AccountingSystemId;
            seller.InvoiceDescription = dto.InvoiceDescription;

            if (!string.IsNullOrEmpty(NewFileName))
                seller.Logo = NewFileName;

            _db.parties.Update(seller);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "اطلاعات فروشنده با موفقیت بروز شد";
                }
            }
            catch (Exception ex)
            {
                result.ExceptionError = ex.Message;
            }

            return result;

        }
        public async Task<clsResult> SetOthersSellerInfoAsync(UpdateSellerDto dto)
        {
            clsResult result = new clsResult()
            {
                Success = false,
                ShowMessage = true,
                Message = "در بروزرسانی اطلاعات خطایی رخ داده است."
            };

            if (dto == null) { return result; }

            var seller = await _db.parties.FindAsync(dto.Id);
            //

            seller.Name = dto.Name;
            seller.NationalId = dto.NationalId;
            seller.EconomicCode = dto.EconomicCode;
            seller.PostalCode = dto.PostalCode;
            seller.RegistrationNumber = dto.RegistrationNumber;

            seller.CEOContactNumber = dto.CEOContactNumber;
            seller.CEOName = dto.CEOName;
            seller.TaxFileNumber = dto.TaxFileNumber;
            seller.TaxTrackingNumber = dto.TaxTrackingNumber;
            seller.TaxUnitAddress = dto.TaxUnitAddress;
            seller.TaxUnitCode = dto.TaxUnitCode;
            seller.TaxAuditor = dto.TaxAuditor;
            seller.TaxPanelPassword = dto.TaxPanelPassword;

            _db.parties.Update(seller);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "اطلاعات با موفقیت بروز شد";
                }
            }
            catch (Exception ex)
            {
                result.Message += ex.Message;
            }

            return result;

        }

        public async Task<SelectList> SelectList_CustomerSellers(int? customerId)
        {
            var sellers = await _db.parties
               .Where(n => n.CustomerId == customerId && n.Role == 1)
               .Select(n => new { id = n.Id, name = n.Name }).ToListAsync();

            return new SelectList(sellers, "id", "name");
        }

        public async Task<TaxpayerInfoDto> GetTaxPayerInfoAsync(long sellerId)
        {
            var info = await _db.TaxpayerInfos.Where(n => n.SellerId == sellerId).FirstOrDefaultAsync();
            TaxpayerInfoDto dto = new TaxpayerInfoDto();

            if (info == null)
            {
                var sellerInfo = await _db.parties.Include(n => n.PayerType).Where(n => n.Id == sellerId).FirstOrDefaultAsync();

                if (sellerInfo == null)
                {
                    throw new Exception("اطلاعات فروشنده یافت نشد.");
                }

                TaxpayerInfo taxpayerInfo = new TaxpayerInfo
                {
                    SellerId = sellerId,

                    NationalCode = sellerInfo.NationalId,
                    TaxId = sellerInfo.EconomicCode,
                    RegistrationNumber = sellerInfo.RegistrationNumber,
                    RegisteredAddress = sellerInfo.Address,
                    CompanyType = sellerInfo.PayerType?.Name,

                };

                _db.TaxpayerInfos.Add(taxpayerInfo);

                try
                {
                    await _db.SaveChangesAsync();

                    dto.Id = taxpayerInfo.Id;
                    dto.SellerId = taxpayerInfo.SellerId;
                    dto.NationalCode = taxpayerInfo.NationalCode;
                    dto.TaxId = taxpayerInfo.TaxId;
                    dto.RegistrationNumber = taxpayerInfo.RegistrationNumber;
                    dto.RegisteredAddress = taxpayerInfo.RegisteredAddress;
                    dto.CompanySubject = taxpayerInfo.CompanySubject;
                    dto.CompanyActivity = taxpayerInfo.CompanyActivity;
                    dto.CompanyType = taxpayerInfo.CompanyType;
                    dto.VATStatus = taxpayerInfo.VATStatus;
                    dto.DeclarationType = taxpayerInfo.DeclarationType;
                    dto.CEOName = taxpayerInfo.CEOName;
                    dto.CEONationalCode = taxpayerInfo.CEONationalCode;
                    dto.CEOPhone = taxpayerInfo.CEOPhone;
                    dto.CEOAddress = taxpayerInfo.CEOAddress;
                    dto.BoardMember1Name = taxpayerInfo.BoardMember1Name;
                    dto.BoardMember1NationalCode = taxpayerInfo.BoardMember1NationalCode;
                    dto.BoardMember1Phone = taxpayerInfo.BoardMember1Phone;
                    dto.BoardMember1Address = taxpayerInfo.BoardMember1Address;

                }
                catch (Exception ex)
                {
                    throw new Exception("خطا در ذخیره اطلاعات مودی.", ex);
                }

                return dto;
            }
            else
            {
                dto.Id = info.Id;
                dto.SellerId = info.SellerId;
                dto.NationalCode = info.NationalCode;
                dto.TaxId = info.TaxId;
                dto.RegistrationNumber = info.RegistrationNumber;
                dto.RegisteredAddress = info.RegisteredAddress;
                dto.CompanySubject = info.CompanySubject;
                dto.CompanyActivity = info.CompanyActivity;
                dto.CompanyType = info.CompanyType;
                dto.VATStatus = info.VATStatus;
                dto.DeclarationType = info.DeclarationType;
                dto.CEOName = info.CEOName;
                dto.CEONationalCode = info.CEONationalCode;
                dto.CEOPhone = info.CEOPhone;
                dto.CEOAddress = info.CEOAddress;

                dto.BoardMember1Name = info.BoardMember1Name;
                dto.BoardMember1NationalCode = info.BoardMember1NationalCode;
                dto.BoardMember1Phone = info.BoardMember1Phone;
                dto.BoardMember1Address = info.BoardMember1Address;

                dto.BoardMember2Name = info.BoardMember2Name;
                dto.BoardMember2NationalCode = info.BoardMember2NationalCode;
                dto.BoardMember2Phone = info.BoardMember2Phone;
                dto.BoardMember2Address = info.BoardMember2Address;

                dto.BoardMember3Name = info.BoardMember3Name;
                dto.BoardMember3NationalCode = info.BoardMember3NationalCode;
                dto.BoardMember3Phone = info.BoardMember3Phone;
                dto.BoardMember3Address = info.BoardMember3Address;

                dto.TaxUnitCode = info.TaxUnitCode;
                dto.TaxOfficeAddress = info.TaxOfficeAddress;
                dto.TaxGroupHead = info.TaxGroupHead;
                dto.TaxAuditor = info.TaxAuditor;
                dto.SeniorTaxAuditor = info.SeniorTaxAuditor;
                dto.ChiefTaxAuditor = info.ChiefTaxAuditor;

                dto.PreRegistrationTrackingCode = info.PreRegistrationTrackingCode;
                dto.TaxFileNumber = info.TaxFileNumber;
                dto.UniqueTaxMemoryId = info.UniqueTaxMemoryId;
                dto.AssetPanelPassword = info.AssetPanelPassword;

                dto.AccountingSoftwareName = info.AccountingSoftwareName;
                dto.AccountingSoftwarePassword = info.AccountingSoftwarePassword;
                dto.InterfaceSoftwareName = info.InterfaceSoftwareName;
                dto.InterfaceSoftwarePassword = info.InterfaceSoftwarePassword;

                dto.CFOName = info.CFOName;
                dto.CFOMobile = info.CFOMobile;

                dto.FinancialAdvisorName = info.FinancialAdvisorName;
                dto.FinancialAdvisorMobile = info.FinancialAdvisorMobile;

                dto.AccountantName = info.AccountantName;
                dto.AccountantMobile = info.AccountantMobile;

                dto.AuditFirmName = info.AuditFirmName;

                return dto;
            }
        }

        public async Task<clsResult> UpdateTaxPayerInoAsync(TaxpayerInfoDto dto)
        {
            // ایجاد شیء پاسخ (Result)
            clsResult result = new clsResult()
            {
                Success = false,
                ShowMessage = true,
                Message = "در بروزرسانی اطلاعات خطایی رخ داده است."
            };

            // بررسی صحت ورودی
            if (dto == null || dto.SellerId <= 0)
            {
                result.Message = "اطلاعات ورودی نامعتبر است.";
                return result;
            }

            try
            {
                // جستجوی رکورد مربوط به SellerId
                var taxpayerInfo = await _db.TaxpayerInfos.FindAsync(dto.Id);

                if (taxpayerInfo == null)
                {
                    result.Message = "اطلاعات مودی یافت نشد.";
                    return result;
                }

                // به‌روزرسانی فیلدهای مودی با مقادیر جدید
                taxpayerInfo.NationalCode = dto.NationalCode;
                taxpayerInfo.TaxId = dto.TaxId;
                taxpayerInfo.RegistrationNumber = dto.RegistrationNumber;
                taxpayerInfo.RegisteredAddress = dto.RegisteredAddress;
                taxpayerInfo.CompanySubject = dto.CompanySubject;
                taxpayerInfo.CompanyActivity = dto.CompanyActivity;
                taxpayerInfo.CompanyType = dto.CompanyType;
                taxpayerInfo.VATStatus = dto.VATStatus;
                taxpayerInfo.DeclarationType = dto.DeclarationType;
                taxpayerInfo.CEOName = dto.CEOName;
                taxpayerInfo.CEONationalCode = dto.CEONationalCode;
                taxpayerInfo.CEOPhone = dto.CEOPhone;
                taxpayerInfo.CEOAddress = dto.CEOAddress;

                taxpayerInfo.BoardMember1Name = dto.BoardMember1Name;
                taxpayerInfo.BoardMember1NationalCode = dto.BoardMember1NationalCode;
                taxpayerInfo.BoardMember1Phone = dto.BoardMember1Phone;
                taxpayerInfo.BoardMember1Address = dto.BoardMember1Address;

                taxpayerInfo.BoardMember2Name = dto.BoardMember2Name;
                taxpayerInfo.BoardMember2NationalCode = dto.BoardMember2NationalCode;
                taxpayerInfo.BoardMember2Phone = dto.BoardMember2Phone;
                taxpayerInfo.BoardMember2Address = dto.BoardMember2Address;

                taxpayerInfo.BoardMember3Name = dto.BoardMember3Name;
                taxpayerInfo.BoardMember3NationalCode = dto.BoardMember3NationalCode;
                taxpayerInfo.BoardMember3Phone = dto.BoardMember3Phone;
                taxpayerInfo.BoardMember3Address = dto.BoardMember3Address;

                taxpayerInfo.TaxUnitCode = dto.TaxUnitCode;
                taxpayerInfo.TaxOfficeAddress = dto.TaxOfficeAddress;
                taxpayerInfo.TaxGroupHead = dto.TaxGroupHead;
                taxpayerInfo.TaxAuditor = dto.TaxAuditor;
                taxpayerInfo.SeniorTaxAuditor = dto.SeniorTaxAuditor;
                taxpayerInfo.ChiefTaxAuditor = dto.ChiefTaxAuditor;

                taxpayerInfo.PreRegistrationTrackingCode = dto.PreRegistrationTrackingCode;
                taxpayerInfo.TaxFileNumber = dto.TaxFileNumber;
                taxpayerInfo.UniqueTaxMemoryId = dto.UniqueTaxMemoryId;
                taxpayerInfo.AssetPanelPassword = dto.AssetPanelPassword;

                taxpayerInfo.AccountingSoftwareName = dto.AccountingSoftwareName;
                taxpayerInfo.AccountingSoftwarePassword = dto.AccountingSoftwarePassword;
                taxpayerInfo.InterfaceSoftwareName = dto.InterfaceSoftwareName;
                taxpayerInfo.InterfaceSoftwarePassword = dto.InterfaceSoftwarePassword;


                taxpayerInfo.CFOName = dto.CFOName;
                taxpayerInfo.CFOMobile = dto.CFOMobile;

                taxpayerInfo.FinancialAdvisorName = dto.FinancialAdvisorName;
                taxpayerInfo.FinancialAdvisorMobile = dto.FinancialAdvisorMobile;

                taxpayerInfo.AccountantName = dto.AccountantName;
                taxpayerInfo.AccountantMobile = dto.AccountantMobile;

                taxpayerInfo.AuditFirmName = dto.AuditFirmName;

                // به‌روزرسانی رکورد در دیتابیس
                _db.TaxpayerInfos.Update(taxpayerInfo);

                // ذخیره تغییرات
                int saveResult = await _db.SaveChangesAsync();

                // بررسی موفقیت عملیات
                if (saveResult > 0)
                {
                    result.Success = true;
                    result.Message = "اطلاعات مودی با موفقیت بروزرسانی شد.";
                }
            }
            catch (Exception ex)
            {
                // مدیریت خطا
                result.Message = $"خطا در بروزرسانی اطلاعات: {ex.Message}";
            }

            return result;
        }


    }
}
