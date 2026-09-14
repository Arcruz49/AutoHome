

using AutoHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Infrastructure.Repositories;
public abstract class BaseRepository<T> where T : class
{
    protected readonly AutoHomeDbContext _db;

    protected BaseRepository(AutoHomeDbContext db)
    {
        _db = db;
    }

    protected async Task BaseAddAsync(T entity, CancellationToken ct)
        => await _db.AddAsync(entity, ct);

    protected void BaseUpdate(T entity)
        => _db.Update(entity);

    protected void BaseRemove(T entity)
        => _db.Remove(entity);

    protected async Task<T?> BaseFindAsync(Guid id)
        => await _db.FindAsync<T>(id);

    protected IQueryable<T> BaseQuery()
        => _db.Set<T>().AsNoTracking();
}