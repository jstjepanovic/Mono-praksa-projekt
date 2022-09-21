using AutoMapper;
using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Service;
using ProjectBoard.Service.Common;
using ProjectBoard.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectBoard.WebApi.Controllers
{
    public class ListingController : ApiController
    {
        IListingService ListingService;
        IMapper Mapper;
        public ListingController(IListingService listingService, IMapper mapper)
        {
            this.ListingService = listingService;
            this.Mapper = mapper;
        }

        [HttpGet]
        [Route("find-listing")]
        public async Task<HttpResponseMessage> FindAsync(Guid? userId=null, double? price=null, string condition=null, DateTime? timeCreated=null, int? pageNumber = null, int? recordsPerPage = null, string orderBy = "TimeCreated", string sortOrder = "asc")
        {
            var paging = new Paging(pageNumber, recordsPerPage);
            var sorting = new Sorting(orderBy, sortOrder);
            var listingFilter = new ListingFilter(price, condition, timeCreated);
            List<Listing> listings = await ListingService.FindListingAsync(userId, paging, sorting, listingFilter);
            if (listings.Count > 0)
            {
                List<ListingGetRest> listingsRest = Mapper.Map<List<Listing>, List<ListingGetRest>>(listings);
                return Request.CreateResponse(HttpStatusCode.OK, listingsRest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, listings);
            }
        }

        [HttpGet]
        [Route("get-listing")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Guid listingId)
        {
            var listing = await ListingService.GetListingAsync(listingId);
            var listingRest= Mapper.Map<Listing, ListingRest>(listing);
            if (listing == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, listingRest);
            }
        }

        [HttpPost]
        [Route("create-listing")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] ListingRest listingRest)
        {
            Listing listing = Mapper.Map<ListingRest, Listing>(listingRest);
            var result = await ListingService.CreateListingAsync(listing);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, listingRest);
            }
        }

        [HttpPut]
        [Route("update-listing")]
        public async Task<HttpResponseMessage> PutAsync([FromUri] Guid listingId, [FromBody] ListingRest listingRest)
        {
            Listing listing = Mapper.Map<ListingRest, Listing>(listingRest);
            var result = await ListingService.UpdateListingAsync(listingId, listing);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, listingRest);
            }
        }

        [HttpDelete]
        [Route("delete-listing")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid listingId)
        {
            var result = await ListingService.DeleteListingAsync(listingId);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Listing deleted!");
            }
        }
    }
}

