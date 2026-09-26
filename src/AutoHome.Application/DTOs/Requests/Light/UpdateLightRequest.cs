namespace AutoHome.Application.DTOs.Requests;
public sealed record UpdateLightRequest(Guid Id, string DeviceId, string Name, string IpAddress, bool SupportsColour);