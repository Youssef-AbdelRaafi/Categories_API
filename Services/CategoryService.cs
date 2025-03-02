using CategoriesAPI.Data.Models;
using CategoriesAPI.Data.Repositories;
using CategoriesAPI.DTOs;

namespace CategoriesAPI.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _repository;

        public CategoryService(CategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryResponseDTO> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var createdCategory = await _repository.CreateCategoryAsync(category);
            return new CategoryResponseDTO
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                Description = createdCategory.Description
            };
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null) return null;

            return new CategoryResponseDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDTO dto)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category != null)
            {
                category.Name = dto.Name;
                category.Description = dto.Description;
                await _repository.UpdateCategoryAsync(category);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _repository.DeleteCategoryAsync(id);
        }
    }
}