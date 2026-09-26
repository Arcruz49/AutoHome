namespace AutoHome.Application.Abstractions;

public sealed record ScannedDevice(
    string DeviceId,
    string IpAddress,
    string ProductId,
    string Version);