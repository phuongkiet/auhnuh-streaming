using auhnuh_server.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Infrastructure.Data.Configuration
{
    public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
    {
        public void Configure(EntityTypeBuilder<WatchHistory> builder)
        {
            builder.HasKey(w => w.HistoryId);
            builder.Property(w => w.HistoryId).ValueGeneratedOnAdd();

            builder.HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(w => w.Episode)
                .WithMany()
                .HasForeignKey(w => w.EpisodeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
