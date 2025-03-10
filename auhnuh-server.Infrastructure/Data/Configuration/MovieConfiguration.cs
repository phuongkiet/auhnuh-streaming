using auhnuh_server.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace auhnuh_server.Infrastructure.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Primary Key
            builder.HasKey(m => m.MovieId);
            builder.Property(m => m.MovieId).ValueGeneratedOnAdd();

            // Properties
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(m => m.ReleaseDate)
                .IsRequired();

            builder.Property(m => m.Publisher)
                .HasMaxLength(255);

            builder.Property(m => m.TotalSeason)
                .HasDefaultValue(0);

            builder.Property(m => m.Thumbnail)
                .IsRequired();

            builder.Property(m => m.Status)
                .HasConversion<int>() // Store enum as int
                .IsRequired();

            // Relationships
            builder.HasMany(m => m.MovieCategories)
                .WithOne(mc => mc.Movies)
                .HasForeignKey(mc => mc.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Seasons)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
