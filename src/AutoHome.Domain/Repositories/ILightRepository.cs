using AutoHome.Domain.Entities;

namespace AutoHome.Domain.Abstractions.Repositories;

public interface ILightRepository
{
    Task<Light?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Light>?> GetAllAsync(CancellationToken ct = default);
    Task RegisterAsync(Light light, CancellationToken ct = default);
    void Update(Light light);
    void Delete(Light light);
}
