using AutoHome.Application.Common;

namespace AutoHome.Application.Abstractions;

public interface ITuyaSync
{
    Task<Result<int>> SyncAsync(CancellationToken ct = default);
}