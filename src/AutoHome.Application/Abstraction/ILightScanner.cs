using AutoHome.Application.Common;

namespace AutoHome.Application.Abstractions;

public interface ILightScanner
{
    Task<Result<List<ScannedDevice>>> ScanAsync(CancellationToken ct = default);
}