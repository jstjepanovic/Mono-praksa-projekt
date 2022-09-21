using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service.Common
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(Guid categoryId);
        Task<List<Category>> GetAllCategoryAsync();
    }
}
