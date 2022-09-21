using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model.Common
{
    public interface ICategory
    {
        Guid CategoryId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
