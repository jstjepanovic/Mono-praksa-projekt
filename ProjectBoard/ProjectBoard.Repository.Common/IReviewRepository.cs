using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IReviewRepository
    {
        Task<int> ReviewCheckAsync(Guid reviewId);
        Task<List<Review>> FindReviewAsync(Guid? boardGameId, Paging paging, Sorting sorting, ReviewFilter filter);
        Task<Review> GetReviewAsync(Guid reviewId);
        Task<int> CountReviewAsync(Guid boardGameId);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Guid reviewId, Review review);
        Task DeleteReviewAsync(Guid reviewId);
    }
}
