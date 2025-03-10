using Microsoft.EntityFrameworkCore;

namespace auhnuh_server.Infrastructure.Data.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
    void Migrate();
}
