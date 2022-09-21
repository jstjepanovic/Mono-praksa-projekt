using System;

namespace ProjectBoard.WebApi.Models
{
    public class ReviewGetRest
    {
        public Guid ReviewId { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }
        public string ReviewText { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public string BoardGameName { get; set; }
        public ReviewGetRest() { }
    }
}