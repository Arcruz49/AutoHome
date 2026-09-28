namespace AutoHome.Application.Abstractions;

public sealed record LightState(
    bool? On,
    string? Mode,
    int? Brightness,
    int? Temperature,
    string? Colour);