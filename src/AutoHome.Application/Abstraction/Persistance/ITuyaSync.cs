using AutoHome.Application.Common;

public interface ITuyaSync
{
    Task<Result<int>> SyncAsync(CancellationToken ct = default);
}