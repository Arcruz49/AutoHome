using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace VitalSyncAPI.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AutoHomeDbContext _db;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AutoHomeDbContext db)
    {
        _db = db;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
        await _transaction!.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        await _transaction!.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}