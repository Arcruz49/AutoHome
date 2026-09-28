using AutoHome.Application.Common;

namespace AutoHome.Application.Abstractions;

public interface ILightControl
{
    Task<Result<LightState>> GetStateAsync(string deviceId, string ip, CancellationToken ct = default);
    Task<Result<LightState>> SwitchAsync(string deviceId, string ip, bool on, CancellationToken ct = default);
}