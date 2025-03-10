using auhnuh_server.Common.Attibutes;
using auhnuh_server.Common.Constants;
using auhnuh_server.Domain;
using auhnuh_server.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace auhnuh_server.Infrastructure.Data
{
    [AutoRegisterAsConcreteClass]
    public class MovieDbContext : IdentityDbContext<User, Role, int>, IApplicationDbContext, IReadOnlyApplicationDbContext
    {
        public MovieDbContext() { }
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=auhnuh;Username=postgres;Password=12345");

            if (ApplicationEnvironment.IsDevelopment())
                optionsBuilder.EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .LogTo(Console.WriteLine);
        }

        DbSet<TEntity> IApplicationDbContext.CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        IQueryable<TEntity> IReadOnlyApplicationDbContext.CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>().AsNoTracking();
        }

        public void Migrate()
        {
            if (base.Database.GetPendingMigrations().Any())
                base.Database.Migrate();
        }
    }
}
