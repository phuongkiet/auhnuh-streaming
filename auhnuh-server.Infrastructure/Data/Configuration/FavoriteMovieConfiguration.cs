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
    public class FavoriteMovieConfiguration : IEntityTypeConfiguration<FavoriteMovie>
    {
        public void Configure(EntityTypeBuilder<FavoriteMovie> builder)
        {
            builder.HasKey(fm => new { fm.UserId, fm.MovieId });

            // Relationships
            builder.HasOne(fm => fm.User)
                .WithMany()
                .HasForeignKey(fm => fm.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(fm => fm.Movie)
                .WithMany()
                .HasForeignKey(fm => fm.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
