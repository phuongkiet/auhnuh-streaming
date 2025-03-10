using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain
{
    public class MovieCategory
    {
        public int MovieId { get; set; }
        public int CategoryId { get; set; }
        public Movie Movies { get; set; }
        public Category Categories { get; set; }
    }
}
