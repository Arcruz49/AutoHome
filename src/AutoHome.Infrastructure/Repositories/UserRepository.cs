using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;
using AutoHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Infrastructure.Repositories;

public class UserRepository(AutoHomeDbContext db) : BaseRepository<User>(db), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) 
        => await _db.Users.FirstOrDefaultAsync(a => a.Email == email, ct);

    public async Task<bool> AnyAsync(CancellationToken ct = default)
        => await _db.Users.AnyAsync(ct);
    public async Task AddAsync(User user, CancellationToken ct = default)
        => await base.BaseAddAsync(user, ct);
}