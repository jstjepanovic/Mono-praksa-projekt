namespace ProjectBoard.WebApi.Models
{
    public class BoardGameRest
    {
        public string Name { get; set; }
        public int NoPlayersMin { get; set; }
        public int NoPlayersMax { get; set; }
        public int Age { get; set; }
        public int AvgPlayingTime { get; set; }
        public string Publisher { get; set; }
        public System.Guid GenreId { get; set; }

        public BoardGameRest() { }
    }
}