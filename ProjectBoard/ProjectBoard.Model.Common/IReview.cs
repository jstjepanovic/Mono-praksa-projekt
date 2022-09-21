using System;

namespace ProjectBoard.Model.Common
{
    public interface IReview
    {
        Guid ReviewId { get; set; }
        Guid UserId { get; set; }
        Guid BoardGameId { get; set; }
        double Rating { get; set; }
        double Weight { get; set; }
        string ReviewText { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime TimeUpdated { get; set; }
        public string Username { get; set; }
        public string BoardGameName { get; set; }
    }
}
