namespace auhnuh_server.Infrastructure.Data.Interfaces;

public interface IReadOnlyApplicationDbContext
{
    IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class;
}
