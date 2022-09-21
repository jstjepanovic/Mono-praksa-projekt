using ProjectBoard.Model.Common;
using System;

namespace ProjectBoard.Model
{
    public class BoardGame : IBoardGame
    {
        public Guid BoardGameId { get; set; }
        public string Name { get; set; }
        public int NoPlayersMin { get; set; }
        public int NoPlayersMax { get; set; }
        public int Age { get; set; }
        public int AvgPlayingTime { get; set; }
        public double? Rating { get; set; }
        public double? Weight { get; set; }
        public string Publisher { get; set; }
        public Guid GenreId { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public Genre Genre { get; set; }

        public BoardGame(Guid boardGameId, string name, int noPlayersMin, int noPlayersMax, int age, int avgPlayingTime, double? rating, double? weight,
                         string publisher, Guid genreId, DateTime timeCreated, DateTime timeUpdated, Genre genre)
        {
            BoardGameId = boardGameId;
            Name = name;
            NoPlayersMin = noPlayersMin;
            NoPlayersMax = noPlayersMax;
            Age = age;
            AvgPlayingTime = avgPlayingTime;
            Rating = rating;
            Weight = weight;
            Publisher = publisher;
            GenreId = genreId;
            TimeCreated = timeCreated;
            TimeUpdated = timeUpdated;
            Genre = genre;
        }

        public BoardGame() { }
    }
}
