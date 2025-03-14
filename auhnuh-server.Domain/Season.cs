using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain
{
    public class Season
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int SeasonNumber { get; set; }
        public int? TotalEpisode {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Episode> Episodes { get; set; }
        public Movie Movie { get; set; }
    }
}
