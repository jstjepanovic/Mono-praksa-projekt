using System;

namespace ProjectBoard.Common
{
    public class BoardGameFilter
    {
        public string Name { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public int? Age { get; set; }
        public double? Rating { get; set; }
        public double? Weight { get; set; }
        public string Publisher { get; set; }
        public DateTime? TimeUpdated { get; set; }

        public BoardGameFilter(string name, int? minPlayers, int? maxPlayers, int? age, double? rating, double? weight, string publisher, DateTime? timeUpdated)
        {
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            Age = age;
            Rating = rating;
            Weight = weight;
            Publisher = publisher;
            TimeUpdated = timeUpdated;
        }
    }
}
