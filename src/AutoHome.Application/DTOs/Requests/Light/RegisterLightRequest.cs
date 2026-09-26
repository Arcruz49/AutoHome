namespace AutoHome.Application.DTOs.Requests;
public sealed record RegisterLightRequest(string DeviceId, string Name, string IpAddress, bool SupportsColour);