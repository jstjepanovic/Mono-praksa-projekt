using System;

namespace ProjectBoard.Model.Common
{
    public interface IBoardGame
    {
        Guid BoardGameId { get; set; }
        string Name { get; set; }
        int NoPlayersMin { get; set; }
        int NoPlayersMax { get; set; }
        int Age { get; set; }
        int AvgPlayingTime { get; set; }
        double? Rating { get; set; }
        double? Weight { get; set; }
        string Publisher { get; set; }
        Guid GenreId { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime TimeUpdated { get; set; }
    }
}
