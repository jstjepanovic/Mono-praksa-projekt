using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(Guid categoryId);
        Task<List<Category>> GetAllCategoryAsync();
    }
}
