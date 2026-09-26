namespace AutoHome.Application.DTOs;

public sealed record LightDto(Guid Id, string DeviceId, string Name, string IpAddress, bool SupportsColour, DateTime CreatedAt);