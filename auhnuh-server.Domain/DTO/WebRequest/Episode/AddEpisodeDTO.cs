using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.DTO.WebRequest.Episode
{
    public class AddEpisodeDTO
    {
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string VideoUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
