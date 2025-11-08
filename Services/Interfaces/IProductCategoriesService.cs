using EcommerceApp.DTOs;

namespace EcommerceApp.Services.Interfaces
{
    public interface IProductCategoriesService
    {
        Task<CategoryResponseDtos> AddCategoryAsync(CategoryDTOs categoryDto);
        Task<CategoryResponseDtos> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<CategoryResponseDtos>> GetAllCategoriesAsync(int limit);
        Task<CategoryResponseDtos> UpdateCategoryAsync(int categoryId, CategoryDTOs categoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
