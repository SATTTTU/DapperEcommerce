using EcommerceApp.DTOs;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;
using EcommerceApp.Services.Interfaces;
using System.Collections.Generic;

namespace EcommerceApp.Services
{
    public class ProductCategoriesServices : IProductCategoriesService
    {
        public readonly ICategoriesRepository _categoriesRepository;
        public async Task<CategoryResponseDtos> AddCategoryAsync(CategoryDTOs categoryDto)
        {
            try
            {

                if (string.IsNullOrEmpty(categoryDto.Name))
                {
                    throw new ArgumentException("Category name cannot be null or empty.");
                }
                var category = new Categories
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                    ParentId = categoryDto.ParentId ?? 0
                };
                var addedCategory = await _categoriesRepository.AddAsync(category);

                var response = new CategoryResponseDtos
                {
                    Id = addedCategory.Id,
                    Name = addedCategory.Name,
                    Description = addedCategory.Description,
                    ParentId = addedCategory.ParentId
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}");
            }
        }


        public async Task<IEnumerable<CategoryResponseDtos>> GetAllCategoriesAsync(int limit)
        {
            try
            {
                if (limit <= 0)
                {
                    throw new ArgumentException("Limit must be greater than zero.");
                }
                var categories = await _categoriesRepository.GetAllAsync();
                var response = categories.Take(limit).Select(cat => new CategoryResponseDtos
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Description = cat.Description,
                    ParentId = cat.ParentId
                });
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve categories: {ex.Message}");
            }


        }



        public async Task<CategoryResponseDtos?> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                {
                    throw new ArgumentException("Category ID must be greater than zero.");
                }

                var category = await _categoriesRepository.GetByIdAsync(categoryId);

                if (category == null)
                {
                    throw new  KeyNotFoundException("Category not found"); 
                }

                var response = new CategoryResponseDtos
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentId = category.ParentId
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve category: {ex.Message}", ex);
            }
        }


        public async Task<CategoryResponseDtos> UpdateCategoryAsync(int categoryId, CategoryDTOs categoryDto)
        {
            try
            {
                if (categoryId <= 0)
                {
                    throw new ArgumentException("Category Id cant be negative");
                }
            
            }
            catch(Exception ex) {
            {
                
            }
        }
    }
    }
