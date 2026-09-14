using AutoHome.Domain.Entities;

namespace AutoHome.Domain.Abstractions.Repositories;

public interface IConfigRepository
{
    Task<Config?> GetConfigAsync(CancellationToken ct = default);
    Task UpdateAsync(Config config, CancellationToken ct = default);
}