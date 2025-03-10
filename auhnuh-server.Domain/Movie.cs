using auhnuh_server.Domain.Common;

namespace auhnuh_server.Domain
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public int TotalSeason { get; set; }
        public int Thumbnail { get; set; }
        public MovieStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<MovieCategory> MovieCategories { get; set; }
        public ICollection<Season> Seasons { get; set; }
    }
}
