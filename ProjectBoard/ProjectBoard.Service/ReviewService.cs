using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Repository.Common;
using ProjectBoard.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBoard.Service
{
    public class ReviewService : IReviewService
    {
        protected IReviewRepository ReviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            ReviewRepository = reviewRepository;
        }

        public async Task<List<Review>> FindReviewAsync(Guid? boardGameId, Paging paging, Sorting sorting, ReviewFilter filter)
        {
            return await ReviewRepository.FindReviewAsync(boardGameId ,paging, sorting, filter);
        }

        public async Task<Review> GetReviewAsync(Guid reviewId)
        {
            return await ReviewRepository.GetReviewAsync(reviewId);
        }
        public async Task<int> CountReviewAsync(Guid boardGameId)
        {
            return await ReviewRepository.CountReviewAsync(boardGameId);
        }
        public async Task<Review> CreateReviewAsync(Review review)
        {
            review.TimeCreated = review.TimeUpdated = DateTime.UtcNow;
            review.ReviewId = Guid.NewGuid();
            return await ReviewRepository.CreateReviewAsync(review);
        }
        public async Task<Review> UpdateReviewAsync(Guid reviewId, Review review)
        {
            review.TimeUpdated = DateTime.UtcNow;
            return await ReviewRepository.UpdateReviewAsync(reviewId, review);
        }
        public async Task DeleteReviewAsync(Guid reviewId)
        {
            await ReviewRepository.DeleteReviewAsync(reviewId);
        }
    }
}
