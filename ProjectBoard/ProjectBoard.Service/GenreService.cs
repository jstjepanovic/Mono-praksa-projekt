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
    class GenreService : IGenreService
    {
        IGenreRepository GenreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            this.GenreRepository = genreRepository;
        }
        public async Task<Genre> GetGenreAsync(Guid genreId)
        {
            return await GenreRepository.GetGenreAsync(genreId);
        }
        public async Task<List<Genre>> GetAllGenreAsync()
        {
            return await GenreRepository.GetAllGenreAsync();
        }
    }
}
