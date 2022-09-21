using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model.Common
{
    public interface IListing
    {
        Guid ListingId { get; set; }
        double Price { get; set; }
        string Condition { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime TimeUpdated { get; set; }
        Guid UserId { get; set; }
        Guid BoardGameId { get; set; }
        Guid? OrderId { get; set; }
    }
}
