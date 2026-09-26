namespace AutoHome.Application.Commands;

public sealed record UpdateLightCommand(Guid Id, string DeviceId, string Name, string IpAddress, bool SupportsColour);