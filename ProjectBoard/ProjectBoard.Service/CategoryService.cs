using ProjectBoard.Model;
using ProjectBoard.Repository.Common;
using ProjectBoard.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository CategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }
        public async Task<Category> GetCategoryAsync(Guid categoryId)
        {
            return await CategoryRepository.GetCategoryAsync(categoryId);
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await CategoryRepository.GetAllCategoryAsync();
        }
    }
}
