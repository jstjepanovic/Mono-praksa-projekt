using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service.Common
{
    public interface IListingService
    {
        Task<List<Listing>> FindListingAsync(Guid? userId, Paging paging, Sorting sorting, ListingFilter listingFilter);
        Task<Listing> GetListingAsync(Guid listingId);
        Task<Listing> UpdateListingAsync(Guid listingId, Listing listing);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<bool> DeleteListingAsync(Guid listingId);
    }
}
