using AutoHome.Application.Abstractions;
using AutoHome.Application.Common;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace AutoHome.Infrastructure.Auth;

public class GoogleTokenValidator : IGoogleTokenValidator
{
    private readonly string _authOptions;

    public GoogleTokenValidator(IOptions<AuthOptions> options)
    {
        _authOptions = options.Value.GoogleClientId;
    }
    public async Task<Result<GoogleUserInfo>> ValidateAsync(string idToken, CancellationToken ct = default)
    {
        GoogleJsonWebSignature.Payload payload;

        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _authOptions }
        };

        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            if(!payload.EmailVerified) return Result<GoogleUserInfo>.Failure("E-mail não verificado");

            return Result<GoogleUserInfo>.Success(new GoogleUserInfo(payload.Email.Trim().ToLowerInvariant(), payload.Name, payload.Subject));
        }
        catch(InvalidJwtException ex)
        {
            return Result<GoogleUserInfo>.Failure(ex.Message);
        }
    }
}