using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Warehouse.WarehouseInterfaces
{
    public interface IWhProductService
    {
        Task<SelectList> SelectList_CategoriesFullnameAsync(long sellerId);
        Task<List<Wh_ProductCategoryDto>> GetAllCategoriesAsync(long sellerId);
        Task<List<Wh_ProductCategoryDto>> GetCategoryTreeAsync(long sellerId);
        Task<clsResult> CreateCategoryAsync(Wh_ProductCategoryDto categoryDto);
        Task<clsResult> CreateSubCategoryAsync(long sellerId, long parentCategoryId, Wh_ProductCategoryDto subCategoryDto);
        Task<clsResult> UpdateCategoryAsync(long sellerId, Wh_ProductCategoryDto categoryDto);
        Task<clsResult> DeleteCategoryAsync(long sellerId, long categoryId);
        Task<clsResult> ActivateCategoryAsync(long sellerId, long categoryId);
        Task<clsResult> DeactivateCategoryAsync(long sellerId, long categoryId);
        Task<bool> CategoryExistsAsync(long sellerId, long categoryId);
        Task<Wh_ProductCategoryDto> GetCategoryByIdAsync(long sellerId, long categoryId);

        // Unit Of Measures
        Task<clsResult> AddUnitCountAsync(UnitOfMeasureDto dto);
        Task<clsResult> UpdateUnitCountAsync(UnitOfMeasureDto dto);
        Task<UnitOfMeasureDto> GetUnitCountByIdAsync(int Id);
        IQueryable<UnitOfMeasureDto> GetUnitCounts(long sellerId, string? name = "");
        Task<SelectList> SelectList_UnitCountAsync(long sellerId);

        // Product and Services
        Task<List<ProductBaseDto>> GetAllProductsAsync(ProductFilter filter);
        Task<clsResult> CreateProductAsync(ProductBaseDto productDto);
        Task<clsResult> UpdateProductAsync(ProductBaseDto productDto);
        Task<clsResult> DeleteProductAsync(long productId);
        Task<clsResult> ToggleProductStatusAsync(long productId);
        Task<ProductBaseDto> GetProductByIdAsync(long productId);
        IQueryable<ProductBaseDto> GetProducts(ProductFilter filter);
        Task<SelectList> SelectList_ProductsAsync(ProductFilter filter);
        Task<ProductBaseDto> GetOrCreateProductByNameAsync(string name, string? code, int qtyContent, long sellerId);
        Task<clsResult> CreateProductsInBulkAsync(List<ProductBaseDto> productsDto, long sellerId);
    }
}