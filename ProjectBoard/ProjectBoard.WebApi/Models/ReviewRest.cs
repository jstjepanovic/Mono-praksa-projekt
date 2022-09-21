using System;

namespace ProjectBoard.WebApi.Models
{
    public class ReviewRest
    {
        public Guid UserId { get; set; }
        public Guid BoardGameId { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }
        public string ReviewText { get; set; }

        public ReviewRest() { }
    }
}