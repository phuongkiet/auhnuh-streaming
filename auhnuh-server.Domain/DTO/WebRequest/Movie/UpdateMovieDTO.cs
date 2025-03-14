using auhnuh_server.Domain.DTO.WebRequest.MovieCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.DTO.WebRequest.Movie
{
    public class UpdateMovieDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public int TotalSeason { get; set; }
        public string Thumbnail { get; set; }
        public string Status { get; set; }
        public List<MovieCategoryDTO> MovieCategories { get; set; }
    }
}
