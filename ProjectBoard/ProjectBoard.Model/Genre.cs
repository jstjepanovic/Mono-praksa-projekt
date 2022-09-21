using ProjectBoard.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model
{
    public class Genre : IGenre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
        public Genre(Guid genreId, string name)
        {
            this.GenreId = genreId;
            this.Name = name;
        }
        public Genre()
        {

        }
    }
}
