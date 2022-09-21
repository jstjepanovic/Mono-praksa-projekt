using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Common
{
    public class Paging
    {
        public int? PageNumber { get; set; }
        public int? RecordsByPage { get; set; }

        public Paging(int? pageNumber, int? recordsByPage)
        {
            this.PageNumber = pageNumber;
            this.RecordsByPage = recordsByPage;
        }

        public Paging()
        {

        }
    }
}
