namespace AutoHome.Infrastructure.Auth;

public sealed class AuthOptions
{
    public const string SectionName = "Auth";

    public string GoogleClientId { get; set; } = string.Empty;
}