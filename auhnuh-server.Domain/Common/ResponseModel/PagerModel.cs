using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.Common.PageModel
{
    public class PagerModel
    {
        public int PageNo { get; set; }
        public int TotalItems { get; set; }
        public int TotalPage { get; set; }
    }
}
