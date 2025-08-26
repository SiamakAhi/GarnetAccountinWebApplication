using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using GarnetAccounting.Areas.Warehouse.Models.Entities;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Warehouse.WarehouseServices
{
    public class WhProductService : IWhProductService
    {
        private readonly AppDbContext _db;
        public WhProductService(AppDbContext context)
        {
            _db = context;
        }

        // SelectList برای دسته‌بندی‌ها با ساختار درختی
        public async Task<SelectList> SelectList_CategoriesFullnameAsync(long sellerId)
        {
            var categories = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId).ToListAsync();

            var categoryItems = categories.Select(c => new
            {
                id = c.CategoryId,
                name = GetFullCategoryName(c, categories)
            }).ToList();

            return new SelectList(categoryItems, "id", "name");
        }

        private string GetFullCategoryName(Wh_ProductCategory category, List<Wh_ProductCategory> allCategories)
        {
            string name = category.CategoryName;
            var parentId = category.ParentCategoryId;

            while (parentId != null)
            {
                var parentCategory = allCategories.FirstOrDefault(c => c.CategoryId == parentId);
                if (parentCategory != null)
                {
                    name = parentCategory.CategoryName + " > " + name;
                    parentId = parentCategory.ParentCategoryId;
                }
                else
                {
                    break;
                }
            }

            return name;
        }

        // لیست دسته‌بندی‌ها
        public async Task<List<Wh_ProductCategoryDto>> GetAllCategoriesAsync(long sellerId)
        {
            var categories = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId).ToListAsync();
            return categories.Select(c => new Wh_ProductCategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description,
                IsActive = c.IsActive,
                SellerId = c.SellerId,
                ParentCategoryId = c.ParentCategoryId
            }).ToList();
        }
        // ساختار درختی دسته‌بندی‌ها
        public async Task<List<Wh_ProductCategoryDto>> GetCategoryTreeAsync(long sellerId)
        {
            // بارگذاری همه دسته‌ها به همراه تمامی زیرشاخه‌ها
            var categories = await _db.Wh_ProductCategories
         .Where(c => c.SellerId == sellerId)
         .ToListAsync();

            // ساخت درخت دسته‌بندی
            var categoryTree = BuildCategoryTree(categories, null, 0);
            return categoryTree;
        }

        private List<Wh_ProductCategoryDto> BuildCategoryTree(List<Wh_ProductCategory> allCategories, long? parentId, int level)
        {
            return allCategories
                .Where(c => c.ParentCategoryId == parentId) // پیدا کردن دسته‌های ریشه یا زیرشاخه
                .Select(c => new Wh_ProductCategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    IsActive = c.IsActive,
                    SellerId = c.SellerId,
                    ParentCategoryId = c.ParentCategoryId,
                    Level = level,
                    SubCategories = BuildCategoryTree(allCategories, c.CategoryId, level + 1)
                }).ToList();
        }
        // ایجاد گروه جدید
        public async Task<clsResult> CreateCategoryAsync(Wh_ProductCategoryDto categoryDto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Wh_ProductCategories.Where(c => c.CategoryName == categoryDto.CategoryName && c.SellerId == categoryDto.SellerId).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این دسته‌بندی با نام {checkDuplicate.CategoryName} در سیستم تعریف شده است.";
                return result;
            }

            var category = new Wh_ProductCategory
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description,
                IsActive = categoryDto.IsActive,
                SellerId = categoryDto.SellerId,
                ParentCategoryId = categoryDto.ParentCategoryId,

            };

            _db.Wh_ProductCategories.Add(category);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دسته‌بندی {category.CategoryName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        // ایجاد زیرگروه
        public async Task<clsResult> CreateSubCategoryAsync(long sellerId, long parentCategoryId, Wh_ProductCategoryDto subCategoryDto)
        {
            clsResult result = new clsResult();
            var parentCategory = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.CategoryId == parentCategoryId).FirstOrDefaultAsync();
            if (parentCategory == null)
            {
                result.Success = false;
                result.Message = "دسته‌بندی والد یافت نشد";
                return result;
            }

            var subCategory = new Wh_ProductCategory
            {
                CategoryName = subCategoryDto.CategoryName,
                Description = subCategoryDto.Description,
                IsActive = subCategoryDto.IsActive,
                SellerId = sellerId,
                ParentCategoryId = parentCategoryId
            };

            _db.Wh_ProductCategories.Add(subCategory);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"زیرگروه {subCategory.CategoryName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        // ویرایش دسته‌بندی
        public async Task<clsResult> UpdateCategoryAsync(long sellerId, Wh_ProductCategoryDto categoryDto)
        {
            clsResult result = new clsResult();
            var existingCategory = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.CategoryId == categoryDto.CategoryId).FirstOrDefaultAsync();
            if (existingCategory == null)
            {
                result.Success = false;
                result.Message = "دسته‌بندی یافت نشد";
                return result;
            }

            existingCategory.CategoryName = categoryDto.CategoryName;
            existingCategory.Description = categoryDto.Description;
            existingCategory.IsActive = categoryDto.IsActive;

            _db.Wh_ProductCategories.Update(existingCategory);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دسته‌بندی {existingCategory.CategoryName} با موفقیت به‌روزرسانی شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در به‌روزرسانی اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        // حذف دسته‌بندی
        public async Task<clsResult> DeleteCategoryAsync(long sellerId, long categoryId)
        {
            clsResult result = new clsResult();
            var category = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                result.Success = false;
                result.Message = "دسته‌بندی یافت نشد";
                return result;
            }
            int childCount = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.ParentCategoryId == categoryId).CountAsync();
            if (childCount > 0)
            {
                result.Success = false;
                result.Message = $" دسته {category.CategoryName} دارای {childCount} زیرگروه است بنابراین امکان حذف آن وجود ندارد";
                return result;
            }
            int productCount = await _db.Wh_Products.Where(c => c.SellerId == sellerId && c.CategoryId == categoryId).CountAsync();
            if (childCount > 0)
            {
                result.Success = false;
                result.Message = $" دسته {category.CategoryName} دارای {productCount} کالا و محصول می باشد. برای حذف آن ابتدا باید محصولات مرتبط با این دسته را حذف یا تغییر دهید.";
                return result;
            }

            _db.Wh_ProductCategories.Remove(category);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دسته‌بندی {category.CategoryName} با موفقیت حذف شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در حذف اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        // فعال کردن دسته‌بندی
        public async Task<clsResult> ActivateCategoryAsync(long sellerId, long categoryId)
        {
            clsResult result = new clsResult();
            var category = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                result.Success = false;
                result.Message = "دسته‌بندی یافت نشد";
                return result;
            }

            category.IsActive = true;
            _db.Wh_ProductCategories.Update(category);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دسته‌بندی {category.CategoryName} با موفقیت فعال شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در فعال‌سازی اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        // غیرفعال کردن دسته‌بندی
        public async Task<clsResult> DeactivateCategoryAsync(long sellerId, long categoryId)
        {
            clsResult result = new clsResult();
            var category = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                result.Success = false;
                result.Message = "دسته‌بندی یافت نشد";
                return result;
            }

            category.IsActive = false;
            _db.Wh_ProductCategories.Update(category);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"دسته‌بندی {category.CategoryName} با موفقیت غیرفعال شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در غیرفعال‌سازی اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        // بررسی وجود دسته‌بندی
        public async Task<bool> CategoryExistsAsync(long sellerId, long categoryId)
        {
            return await _db.Wh_ProductCategories.AnyAsync(c => c.SellerId == sellerId && c.CategoryId == categoryId);
        }
        // دریافت دسته‌بندی با شناسه
        public async Task<Wh_ProductCategoryDto> GetCategoryByIdAsync(long sellerId, long categoryId)
        {
            var category = await _db.Wh_ProductCategories.Where(c => c.SellerId == sellerId && c.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                throw new Exception("دسته‌بندی یافت نشد");
            }

            return new Wh_ProductCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                IsActive = category.IsActive,
                SellerId = category.SellerId,
                ParentCategoryId = category.ParentCategoryId
            };
        }
        //-----
        //------
        // Unit Of Measure
        public async Task<clsResult> AddUnitCountAsync(UnitOfMeasureDto dto)
        {
            if (dto == null)
            {
                return new clsResult { Success = false, Message = "اطلاعاتی برای درج وجود ندارد" };
            }
            bool hasDupplicate = await _db.Wh_UnitOfMeasures.AnyAsync(n => n.SellerId == dto.SellerId && n.UnitName == dto.UnitName);
            if (hasDupplicate)
            {
                return new clsResult { Success = false, Message = "نام یا کد وارد شده تکراری است" };
            }
            Wh_UnitOfMeasure unit = new Wh_UnitOfMeasure();
            unit.SellerId = dto.SellerId;
            unit.UnitName = dto.UnitName;
            unit.UnitSymbol = dto.UnitSymbol;
            unit.UnitCode = dto.UnitCode;
            unit.Description = dto.Description;
            unit.IsActive = dto.IsActive;
            _db.Wh_UnitOfMeasures.Add(unit);

            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new clsResult { Success = true, Message = "واحد اندازه گیری با موفقیت ثبت شد" };
            }
            return new clsResult { Success = false, Message = "خطایی در انجام کار رخ داده است" };
        }
        public async Task<clsResult> UpdateUnitCountAsync(UnitOfMeasureDto dto)
        {
            if (dto == null)
            {
                return new clsResult { Success = false, Message = "اطلاعاتی برای بروزرسانی وجود ندارد" };
            }

            Wh_UnitOfMeasure unit = await _db.Wh_UnitOfMeasures.FindAsync(dto.Id);
            if (unit == null)
            {
                return new clsResult { Success = false, Message = " رکورد متناظری یافت نشد" };
            }
            unit.SellerId = dto.SellerId;
            unit.UnitName = dto.UnitName;
            unit.UnitSymbol = dto.UnitSymbol;
            unit.UnitCode = dto.UnitCode;
            unit.Description = dto.Description;
            unit.IsActive = dto.IsActive;
            _db.Wh_UnitOfMeasures.Update(unit);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new clsResult { Success = true, Message = "ویرایش اطلاعات با موفقیت انجام شد" };
            }
            return new clsResult { Success = false, Message = "خطایی در انجام کار رخ داده است" };
        }
        public async Task<UnitOfMeasureDto> GetUnitCountByIdAsync(int Id)
        {

            Wh_UnitOfMeasure? unit = await _db.Wh_UnitOfMeasures.FindAsync(Id);
            if (unit == null)
            {
                return new UnitOfMeasureDto();
            }
            UnitOfMeasureDto dto = new UnitOfMeasureDto();
            dto.UnitName = unit.UnitName;
            dto.UnitSymbol = unit.UnitSymbol;
            dto.UnitCode = unit.UnitCode;
            dto.SellerId = unit.SellerId;
            dto.Description = unit.Description;
            dto.IsActive = unit.IsActive;
            dto.Id = unit.Id;
            return dto;
        }
        public IQueryable<UnitOfMeasureDto> GetUnitCounts(long sellerId, string? name = "")
        {
            var units = _db.Wh_UnitOfMeasures.Where(n => n.SellerId == sellerId && n.UnitName.Contains(name)).Select(n => new UnitOfMeasureDto
            {
                Id = n.Id,
                UnitName = n.UnitName,
                UnitSymbol = n.UnitSymbol,
                UnitCode = n.UnitCode,
                Description = n.Description,
                IsActive = n.IsActive,
                SellerId = n.SellerId

            }).AsQueryable();

            return units;
        }
        public async Task<SelectList> SelectList_UnitCountAsync(long sellerId)
        {
            var lst = await _db.Wh_UnitOfMeasures.Where(n => n.SellerId == sellerId).Select(n => new { id = n.Id, name = n.UnitName }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(lst, "id", "name");
        }

        //----------
        //---------
        // Product and services
        public async Task<string> StuffCodeGenerator(long sellerId)
        {
            int productQty = await _db.Wh_Products.Where(n => n.SellerId == sellerId).CountAsync();
            int seq = productQty + 1;
            return (sellerId + seq.ToString("00000"));

        }

        // لیست کالاها با فیلتر
        public async Task<List<ProductBaseDto>> GetAllProductsAsync(ProductFilter filter)
        {
            var query = _db.Wh_Products
                .Include(n => n.ProductCategory)
                .Include(n => n.BaseUnit)
                .Include(n => n.PakageUnit)
                .Where(p => p.SellerId == filter.SellerId);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.ProductName.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(p => p.ProductCode.Contains(filter.Code));
            }

            if (filter.CategoryIds.Count > 0)
            {
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId.Value));
            }

            var products = await query.ToListAsync();


            return products.Select(p => new ProductBaseDto
            {
                Id = p.ProductId,
                SellerId = p.SellerId,
                ProductCode = p.ProductCode,
                UniqueId = p.UniqueId,
                ProductName = p.ProductName,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.ProductCategory?.CategoryName,
                BaseUnitId = p.BaseUnitId,
                BaseUnitName = p.BaseUnit.UnitName,
                PakageCountId = p.PakageCountId,
                QuantityInPakage = p.QuantityInPakage,
                ProductType = p.ProductType,
                UnitPrice = p.UnitPrice,
                VATRate = p.VATRate,
                IsActive = p.IsActive,
                HasInventory = p.HasInventory,
                PakageUnitName = p.PakageUnit.UnitName,
            }).ToList();
        }

        // افزودن کالا
        public async Task<clsResult> CreateProductAsync(ProductBaseDto productDto)
        {
            clsResult result = new clsResult();
            var checkDuplicate = await _db.Wh_Products.Where(p => p.ProductCode == productDto.ProductCode && p.SellerId == productDto.SellerId).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                result.Success = false;
                result.Message = $"پیش از این کالایی با کد {checkDuplicate.ProductCode} در سیستم تعریف شده است.";
                return result;
            }

            if (await _db.Wh_Products.AnyAsync(n => n.SellerId == productDto.SellerId && n.ProductCode == productDto.ProductCode))
                productDto.ProductCode = await StuffCodeGenerator(productDto.SellerId);

            var product = new Wh_Product
            {
                SellerId = productDto.SellerId,
                ProductCode = productDto.ProductCode,
                UniqueId = productDto.UniqueId,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                BaseUnitId = productDto.BaseUnitId,
                PakageCountId = productDto.PakageCountId,
                QuantityInPakage = productDto.QuantityInPakage,
                ProductType = productDto.ProductType,
                UnitPrice = productDto.UnitPrice,
                VATRate = productDto.VATRate,
                IsActive = productDto.IsActive,
                HasInventory = productDto.HasInventory
            };

            _db.Wh_Products.Add(product);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"کالا {product.ProductName} با موفقیت ثبت شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }

        // ویرایش کالا
        public async Task<clsResult> UpdateProductAsync(ProductBaseDto productDto)
        {
            clsResult result = new clsResult();
            var existingProduct = await _db.Wh_Products.Where(p => p.ProductId == productDto.Id).FirstOrDefaultAsync();
            if (existingProduct == null)
            {
                result.Success = false;
                result.Message = "کالا یافت نشد";
                return result;
            }

            existingProduct.ProductCode = productDto.ProductCode;
            existingProduct.UniqueId = productDto.UniqueId;
            existingProduct.ProductName = productDto.ProductName;
            existingProduct.Description = productDto.Description;
            existingProduct.CategoryId = productDto.CategoryId;
            existingProduct.BaseUnitId = productDto.BaseUnitId;
            existingProduct.PakageCountId = productDto.PakageCountId;
            existingProduct.QuantityInPakage = productDto.QuantityInPakage;
            existingProduct.ProductType = productDto.ProductType;
            existingProduct.UnitPrice = productDto.UnitPrice;
            existingProduct.VATRate = productDto.VATRate;
            existingProduct.IsActive = productDto.IsActive;
            existingProduct.HasInventory = productDto.HasInventory;

            _db.Wh_Products.Update(existingProduct);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"کالا {existingProduct.ProductName} با موفقیت به‌روزرسانی شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در به‌روزرسانی اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }

        // حذف کالا
        public async Task<clsResult> DeleteProductAsync(long productId)
        {
            clsResult result = new clsResult();
            var product = await _db.Wh_Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                result.Success = false;
                result.Message = "کالا یافت نشد";
                return result;
            }

            _db.Wh_Products.Remove(product);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"کالا {product.ProductName} با موفقیت حذف شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در حذف اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }

        // فعال یا غیرفعال کردن کالا
        public async Task<clsResult> ToggleProductStatusAsync(long productId)
        {
            clsResult result = new clsResult();
            var product = await _db.Wh_Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                result.Success = false;
                result.Message = "کالا یافت نشد";
                return result;
            }

            product.IsActive = !product.IsActive;
            _db.Wh_Products.Update(product);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"وضعیت کالا {product.ProductName} با موفقیت تغییر کرد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در تغییر وضعیت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }

        // دریافت کالا با شناسه
        public async Task<ProductBaseDto> GetProductByIdAsync(long productId)
        {
            var product = await _db.Wh_Products.Include(n => n.ProductCategory)
                .Include(n => n.PakageUnit)
                .Include(n => n.BaseUnit)
                .Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("کالا یافت نشد");
            }

            return new ProductBaseDto
            {
                Id = product.ProductId,
                SellerId = product.SellerId,
                ProductCode = product.ProductCode,
                UniqueId = product.UniqueId,
                ProductName = product.ProductName,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.ProductCategory?.CategoryName,
                BaseUnitId = product.BaseUnitId,
                BaseUnitName = product.BaseUnit.UnitName,
                PakageCountId = product.PakageCountId,
                PakageUnitName = product.PakageUnit.UnitName,
                QuantityInPakage = product.QuantityInPakage,
                ProductType = product.ProductType,
                UnitPrice = product.UnitPrice,
                VATRate = product.VATRate,
                IsActive = product.IsActive,
                HasInventory = product.HasInventory,
                IsService = product.IsService,

            };
        }
        public async Task<ProductBaseDto?> GetProductByNameAsync(string name, long sellerId)
        {
            var product = await _db.Wh_Products.Include(n => n.ProductCategory)
                .Include(n => n.PakageUnit)
                .Include(n => n.BaseUnit)
                .Where(p => p.ProductName == name && p.SellerId == sellerId).FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }

            return new ProductBaseDto
            {
                Id = product.ProductId,
                SellerId = product.SellerId,
                ProductCode = product.ProductCode,
                UniqueId = product.UniqueId,
                ProductName = product.ProductName,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.ProductCategory?.CategoryName,
                BaseUnitId = product.BaseUnitId,
                BaseUnitName = product.BaseUnit.UnitName,
                PakageCountId = product.PakageCountId,
                PakageUnitName = product.PakageUnit.UnitName,
                QuantityInPakage = product.QuantityInPakage,
                ProductType = product.ProductType,
                UnitPrice = product.UnitPrice,
                VATRate = product.VATRate,
                IsActive = product.IsActive,
                HasInventory = product.HasInventory,
                IsService = product.IsService,

            };
        }

        // دریافت کالاها به صورت AsQueryable با فیلتر
        public IQueryable<ProductBaseDto> GetProducts(ProductFilter filter)
        {
            var query = _db.Wh_Products
                .Include(n => n.ProductCategory)
                .Include(n => n.BaseUnit)
                .Include(n => n.PakageUnit)
                .Where(p => p.SellerId == filter.SellerId);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.ProductName.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(p => p.ProductCode.Contains(filter.Code));
            }

            if (filter.CategoryIds.Count > 0)
            {
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId.Value));
            }

            return query.Select(p => new ProductBaseDto
            {
                Id = p.ProductId,
                SellerId = p.SellerId,
                ProductCode = p.ProductCode,
                UniqueId = p.UniqueId,
                ProductName = p.ProductName,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.ProductCategory.CategoryName,
                BaseUnitId = p.BaseUnitId,
                BaseUnitName = p.BaseUnit.UnitName,
                PakageCountId = p.PakageCountId,
                PakageUnitName = p.PakageUnit.UnitName,
                QuantityInPakage = p.QuantityInPakage,
                ProductType = p.ProductType,
                UnitPrice = p.UnitPrice,
                VATRate = p.VATRate,
                IsActive = p.IsActive,
                HasInventory = p.HasInventory,
                IsService = p.IsService,
            }).AsQueryable();
        }

        // SelectList کالاها با فیلتر
        public async Task<SelectList> SelectList_ProductsAsync(ProductFilter filter)
        {
            var query = _db.Wh_Products.Where(p => p.SellerId == filter.SellerId);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.ProductName.Contains(filter.Name));
            }
            if (filter.CategoryIds.Count > 0)
            {
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId.Value));
            }

            var products = await query.Select(p => new { id = p.ProductId, name = p.ProductName }).OrderBy(p => p.name).ToListAsync();
            return new SelectList(products, "id", "name");
        }
        public async Task<ProductBaseDto> GetOrCreateProductByNameAsync(string name, string? code, int qtyContent, long sellerId)
        {
            var existProduct = await GetProductByNameAsync(name, sellerId);
            if (existProduct != null)
                return existProduct;

            ProductBaseDto p = new ProductBaseDto
            {
                BaseUnitName = qtyContent == 1 ? "عدد" : "کیلوگرم",
                HasInventory = true,
                IsActive = true,
                PakageUnitName = qtyContent == 1 ? "عدد" : "بسته",
                ProductName = name,
                QuantityInPakage = qtyContent,
                ProductCode = code,
                VATRate = 0,
                SellerId = sellerId,
            };
            var addProduct = await CreateProductsInBulkAsync(new List<ProductBaseDto> { p }, sellerId);
            if (addProduct.Success)
            {
                var Product = await GetProductByNameAsync(name, sellerId);
                return Product;
            }

            return null;
        }

        public async Task<clsResult> CreateProductsInBulkAsync(List<ProductBaseDto> productsDto, long sellerId)
        {
            clsResult result = new clsResult();
            result.ShowMessage = true;
            result.Success = false;

            var errorMessages = new List<string>();

            // Load units of measure once to avoid multiple calls
            var units = await _db.Wh_UnitOfMeasures.Where(u => u.SellerId == sellerId).ToListAsync();

            // Load existing products once to check for duplicates within the same category
            var existingProducts = await _db.Wh_Products.Where(p => p.SellerId == sellerId).ToListAsync();

            foreach (var productDto in productsDto)
            {
                // Check if product with the same name in the same category already exists
                var duplicateProduct = existingProducts.FirstOrDefault(p => p.ProductName == productDto.ProductName && p.CategoryId == productDto.CategoryId);
                if (duplicateProduct != null)
                {
                    errorMessages.Add($"کالای '{productDto.ProductName}' در گروه مشابه از قبل وجود دارد و تکراری است.");
                    continue; // Skip to the next product
                }

                // Get BaseUnitId and PakageCountId from units list
                var baseUnit = units.FirstOrDefault(u => u.UnitName == productDto.BaseUnitName);
                var pakageUnit = units.FirstOrDefault(u => u.UnitName == productDto.PakageUnitName);

                if (baseUnit == null || pakageUnit == null)
                {
                    errorMessages.Add($"واحد اندازه‌گیری '{productDto.BaseUnitName}' یا '{productDto.PakageUnitName}' برای کالا '{productDto.ProductName}' یافت نشد.");
                    continue; // Skip to the next product
                }

                // Initialize Wh_Product and set properties individually
                var product = new Wh_Product();
                product.SellerId = sellerId;
                product.ProductCode = productDto.ProductCode;
                product.UniqueId = productDto.UniqueId;
                product.ProductName = productDto.ProductName;
                product.Description = productDto.Description;
                product.CategoryId = productDto.CategoryId;
                product.BaseUnitId = baseUnit.Id;
                product.PakageCountId = pakageUnit.Id;
                product.QuantityInPakage = productDto.QuantityInPakage;
                product.ProductType = productDto.ProductType;
                product.UnitPrice = productDto.UnitPrice;
                product.VATRate = productDto.VATRate;
                product.IsActive = productDto.IsActive;
                product.HasInventory = productDto.HasInventory;

                _db.Wh_Products.Add(product);
            }

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "کالاها با موفقیت ثبت شدند.";

                if (errorMessages.Any())
                {
                    result.Message += "\n" + string.Join("\n", errorMessages);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است: " + ex.Message;
            }

            return result;
        }


    }


}