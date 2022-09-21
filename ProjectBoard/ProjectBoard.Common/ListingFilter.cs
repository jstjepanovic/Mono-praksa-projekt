using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Common
{
    public class ListingFilter
    {
        public double? Price { get; set; }
        public string Condition { get; set; }
        public DateTime? TimeCreated { get; set; }

        public ListingFilter(double? price, string condition, DateTime? timeCreated)
        {
            this.Price = price;
            this.Condition = condition;
            this.TimeCreated = timeCreated;
        }
    }
}
