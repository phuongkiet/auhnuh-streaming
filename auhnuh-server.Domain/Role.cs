using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain
{
    public class Role : IdentityRole<int>
    {
        public ICollection<User> Users { get; set; }
    }
}
