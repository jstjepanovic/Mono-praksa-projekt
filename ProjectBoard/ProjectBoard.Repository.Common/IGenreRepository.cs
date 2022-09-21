using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IGenreRepository
    {
        Task<Genre> GetGenreAsync(Guid genreId);
        Task<List<Genre>> GetAllGenreAsync();
    }
}
