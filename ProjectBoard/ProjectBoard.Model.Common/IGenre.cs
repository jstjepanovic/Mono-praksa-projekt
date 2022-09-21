using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model.Common
{
    public interface IGenre
    {
        Guid GenreId { get; set; }
        string Name { get; set; }
    }
}
