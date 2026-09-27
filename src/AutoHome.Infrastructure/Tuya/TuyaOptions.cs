namespace AutoHome.Infrastructure.Tuya;

public sealed class TuyaOptions
{
    public const string SectionName = "Tuya";

    public string PythonPath { get; set; } = "python3";
    public int TimeoutSeconds { get; set; } = 30;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string ApiRegion { get; set; } = string.Empty;
    public string DevicesFilePath { get; set; } = string.Empty;
}