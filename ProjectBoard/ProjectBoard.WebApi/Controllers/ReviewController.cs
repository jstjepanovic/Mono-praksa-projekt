using AutoMapper;
using ProjectBoard.Common;
using ProjectBoard.Model;
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
    public class ReviewController : ApiController
    {
        protected IReviewService ReviewService;
        IMapper Mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            ReviewService = reviewService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("find-review")]
        public async Task<HttpResponseMessage> FindReviewAsync( Guid? boardGameId = null,
                                                                int rpp = 5,
                                                                int pageNumber = 1,
                                                                string orderBy = "Rating",
                                                                string sortOrder = "desc",
                                                                double? rating = null,
                                                                double? weight = null,
                                                                DateTime? timeUpdated = null)
        {
            var reviews = await ReviewService.FindReviewAsync(boardGameId, new Paging(pageNumber, rpp), new Sorting(orderBy, sortOrder), new ReviewFilter(rating, weight, timeUpdated));

            var reviewsRest = Mapper.Map<List<Review>, List<ReviewGetRest>>(reviews);
            return Request.CreateResponse(HttpStatusCode.OK, reviewsRest);
        }

        [HttpGet]
        [Route("get-review/{reviewId}")]
        public async Task<HttpResponseMessage> GetReviewAsync(Guid reviewId)
        {
            var review = Mapper.Map<Review, ReviewGetRest>(await ReviewService.GetReviewAsync(reviewId));
            return Request.CreateResponse(HttpStatusCode.OK, review);
        }

        [HttpGet]
        [Route("count-review/{boardGameId}")]
        public async Task<HttpResponseMessage> CountReviewAsync(Guid boardGameId)
        {
            var reviewCount = await ReviewService.CountReviewAsync(boardGameId);
            return Request.CreateResponse(HttpStatusCode.OK, reviewCount);
        }

        [HttpPost]
        [Route("create-review")]
        public async Task<HttpResponseMessage> CreateReviewAsync(ReviewRest reviewCreate)
        {
            var review = Mapper.Map<ReviewRest, Review>(reviewCreate);
            var newReview = await ReviewService.CreateReviewAsync(review);
            return Request.CreateResponse(HttpStatusCode.OK, newReview);
        }

        [HttpPut]
        [Route("update-review/{reviewId}")]
        public async Task<HttpResponseMessage> UpdateReviewAsync(Guid reviewId, ReviewRest reviewUpdate)
        {
            var review = Mapper.Map<ReviewRest, Review>(reviewUpdate);
            await ReviewService.UpdateReviewAsync(reviewId, review);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("delete-review/{reviewId}")]
        public async Task<HttpResponseMessage> DeleteReviewAsync(Guid reviewId)
        {
            await ReviewService.DeleteReviewAsync(reviewId);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
