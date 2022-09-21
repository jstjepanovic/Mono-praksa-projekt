using System;
namespace ProjectBoard.Common
{
    public class ReviewFilter
    {
        public double? Rating { get; set; }
        public double? Weight { get; set; }
        public DateTime? TimeUpdated { get; set; }

        public ReviewFilter(double? rating, double? weight, DateTime? timeUpdated)
        {
            Rating = rating;
            Weight = weight;
            TimeUpdated = timeUpdated;
        }
    }
}
