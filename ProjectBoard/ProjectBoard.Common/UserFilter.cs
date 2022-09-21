using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Common
{
    public class UserFilter
    {
        public string Search{ get; set; }
        public UserFilter(string search)
        {
            this.Search = search;
        }

        public UserFilter() { }
    }
}
