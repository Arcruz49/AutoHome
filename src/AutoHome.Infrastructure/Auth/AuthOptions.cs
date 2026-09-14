namespace AutoHome.Infrastructure.Auth;

public sealed class AuthOptions
{
    public const string SectionName = "Auth";
    public string JwtSecret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; } = 60;

    public string GoogleClientId { get; set; } = string.Empty;
}