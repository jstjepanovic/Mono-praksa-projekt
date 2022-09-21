using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IListingRepository
    {
        Task<List<Listing>> FindListingAsync(Guid? userId, Paging paging, Sorting sorting, ListingFilter listingFilter);
        Task<Listing> GetListingAsync(Guid listingId);
        Task<Listing> UpdateListingAsync(Guid listingId, Listing listing);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<bool> DeleteListingAsync(Guid listingId);
    }
}
