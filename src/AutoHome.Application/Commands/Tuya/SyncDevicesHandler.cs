using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Common;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class SyncDevicesHandler
{
    private readonly ITuyaSync _tuyaSync;
    public SyncDevicesHandler(ITuyaSync tuyaSync)
    {
        _tuyaSync = tuyaSync;
    }
    public async Task<Result<int>> HandleAsync(CancellationToken ct)
    {
        var result = await _tuyaSync.SyncAsync();

        return result;
    }
}