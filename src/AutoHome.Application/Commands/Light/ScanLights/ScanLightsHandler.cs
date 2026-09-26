using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Common;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class ScanLightsHandler
{
    private readonly ILightScanner _lightScanner;
    public ScanLightsHandler(ILightScanner lightScanner)
    {
        _lightScanner = lightScanner;
    }
    public async Task<Result<List<ScannedDevice>>> HandleAsync(CancellationToken ct)
    {
        var lights = await _lightScanner.ScanAsync();

        return lights;
    }
}