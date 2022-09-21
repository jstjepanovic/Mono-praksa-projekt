using ProjectBoard.Model;
using System;

namespace ProjectBoard.WebApi.Models
{
    public class BoardGameGetRest
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
        public Genre Genre { get; set; }
        public BoardGameGetRest() { }

    }
}