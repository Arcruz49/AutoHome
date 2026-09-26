namespace AutoHome.Infrastructure.Tuya;

public sealed class TuyaOptions
{
    public const string SectionName = "Tuya";

    public string PythonPath { get; set; } = "python3";
    public int TimeoutSeconds { get; set; } = 30;
}