using ProjectBoard.Common;
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
    public class ListingService : IListingService
    {
        IListingRepository ListingRepository;
        public ListingService(IListingRepository listingRepository)
        {
            this.ListingRepository = listingRepository;
        }
        public async Task<List<Listing>> FindListingAsync(Guid? userId, Paging paging, Sorting sorting, ListingFilter listingFilter)
        {
            return await ListingRepository.FindListingAsync(userId, paging, sorting, listingFilter);
        }
        public async Task<Listing> GetListingAsync(Guid listingId)
        {
            return await ListingRepository.GetListingAsync(listingId);
        }
        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            listing.ListingId = Guid.NewGuid();
            listing.TimeUpdated = listing.TimeCreated = DateTime.UtcNow;
            return await ListingRepository.CreateListingAsync(listing);
        }
        public async Task<Listing> UpdateListingAsync(Guid listingId, Listing listing)
        {
            listing.TimeUpdated = DateTime.UtcNow;
            return await ListingRepository.UpdateListingAsync(listingId, listing);
        }
        public async Task<bool> DeleteListingAsync(Guid listingId)
        {
            return await ListingRepository.DeleteListingAsync(listingId);
        }

    }
}
