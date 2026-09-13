using AutoHome.Application.Common;

namespace AutoHome.Application.Abstractions;

public interface IGoogleTokenValidator
{
    Task<Result<GoogleUserInfo>> ValidateAsync(string idToken, CancellationToken ct = default);
}