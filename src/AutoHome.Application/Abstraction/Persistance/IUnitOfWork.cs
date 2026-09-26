namespace AutoHome.Application.Abstractions.Persistance;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitAsync(CancellationToken ct);
    Task RollbackAsync(CancellationToken ct);
}