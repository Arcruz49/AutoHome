using AutoHome.Domain.Entities;

namespace AutoHome.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> AnyAsync(CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
}