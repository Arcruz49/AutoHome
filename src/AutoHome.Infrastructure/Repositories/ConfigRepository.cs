using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;
using AutoHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Infrastructure.Repositories;

public class ConfigRepository(AutoHomeDbContext db) : BaseRepository<Config>(db), IConfigRepository
{
    public async Task<Config?> GetConfigAsync(CancellationToken ct = default)
        => await _db.Configs.FirstOrDefaultAsync(ct);
    public async Task UpdateAsync(Config config, CancellationToken ct = default)
        => BaseUpdate(config);
}