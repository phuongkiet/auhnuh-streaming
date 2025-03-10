using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Domain
{
    public class WatchHistory
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; } = null!;

        public int LastWatchedAt { get; set; } // Thời gian xem cuối cùng (giây)
        public int WatchedDuration { get; set; } // Tổng số giây đã xem
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
