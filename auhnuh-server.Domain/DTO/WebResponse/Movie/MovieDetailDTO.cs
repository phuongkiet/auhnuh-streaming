using auhnuh_server.Domain.DTO.WebResponse.Season;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.DTO.WebResponse.Movie
{
    public class MovieDetailDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public int TotalSeason { get; set; }
        public string? Thumbnail { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> MovieCategories { get; set; }
        public List<SeasonDTO> Seasons { get; set; }
    }
}
