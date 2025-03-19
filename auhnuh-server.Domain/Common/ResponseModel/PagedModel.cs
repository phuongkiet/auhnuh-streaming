using auhnuh_server.Domain.Common.PageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain.Common.ResponseModel
{
    public class PagedModel<T> : PagerModel
    {
        public List<T> Results { get; set; }
    }
}
