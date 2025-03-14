using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.DTO.WebRequest.Season
{
    public class AddSeasonDTO
    {
        public int MovieId { get; set; }
        public int SeasonNumber { get; set; }
        public int TotalEpisode { get; set; }
    }
}
