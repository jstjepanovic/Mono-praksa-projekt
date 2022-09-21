using ProjectBoard.Model.Common;
using System;

namespace ProjectBoard.Model
{
    public class Review : IReview
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public Guid BoardGameId { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }
        public string ReviewText { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public string Username { get; set; }
        public string BoardGameName { get; set; }

        public Review(Guid reviewId, Guid userId, Guid boardGameId, double rating, double weight, string reviewText, DateTime timeCreated, DateTime timeUpdated, string user, string boardGameName)
        {
            ReviewId = reviewId;
            UserId = userId;
            BoardGameId = boardGameId;
            Rating = rating;
            Weight = weight;
            ReviewText = reviewText;
            TimeCreated = timeCreated;
            TimeUpdated = timeUpdated;
            Username = user;
            BoardGameName = boardGameName;
        }

        public Review() { }
    }
}
