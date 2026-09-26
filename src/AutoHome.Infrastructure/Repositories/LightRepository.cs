using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;
using AutoHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Infrastructure.Repositories;

public class LightRepository(AutoHomeDbContext db) : BaseRepository<Light>(db), ILightRepository
{
    public async Task<Light?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await BaseFindAsync(id, ct);
    public async Task<List<Light>?> GetAllAsync(CancellationToken ct = default)
        => await _db.Lights.ToListAsync(ct);
    public async Task RegisterAsync(Light light, CancellationToken ct = default)
        => await BaseAddAsync(light, ct);
    public void Update(Light light)
        => BaseUpdate(light);
    public void Delete(Light light)
        => BaseRemove(light);
    
}