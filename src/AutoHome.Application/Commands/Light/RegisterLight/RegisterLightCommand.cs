namespace AutoHome.Application.Commands;

public sealed record RegisterLightCommand(string DeviceId, string Name, string IpAddress, bool SupportsColour);