namespace AutoHome.Application.DTOs.Requests;
public sealed record SwitchLightRequest(Guid Id, bool On);