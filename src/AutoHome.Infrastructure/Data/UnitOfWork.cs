using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoHome.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AutoHomeDbContext _db;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AutoHomeDbContext db)
    {
        _db = db;
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _db.SaveChangesAsync(ct);
    }

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _transaction = await _db.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        await _db.SaveChangesAsync(ct);
        await _transaction!.CommitAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        await _transaction!.RollbackAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}