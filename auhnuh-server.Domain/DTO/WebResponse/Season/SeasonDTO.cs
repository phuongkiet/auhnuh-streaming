using auhnuh_server.Domain.DTO.WebResponse.Episode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.DTO.WebResponse.Season
{
    public class SeasonDTO
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int TotalEpisode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<EpisodeDTO> Episodes { get; set; }
    }
}
