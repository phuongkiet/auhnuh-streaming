using auhnuh_server.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auhnuh_server.Domain
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public int TotalSeason { get; set; }
        public string? Thumbnail { get; set; }
        public string? Casts { get; set; } 
        public string? Directors { get; set; }
        public MovieStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<MovieCategory> MovieCategories { get; set; }
        public ICollection<Season> Seasons { get; set; }
    }
}
