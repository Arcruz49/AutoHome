using AutoHome.Application.Abstractions;
using AutoHome.Application.Common;
using AutoHome.Domain.Abstractions.Repositories;

namespace AutoHome.Application.Commands;

public class LoginUserHandler
{
    private readonly IGoogleTokenValidator _googleTokenValidator;
    private readonly IUserRepository _userRepository;
    private readonly ITokenIssuer _tokenIssuer;
    public LoginUserHandler(IGoogleTokenValidator googleTokenValidator, IUserRepository userRepository, IConfigRepository configRepository, ITokenIssuer tokenIssuer)
    {
        _googleTokenValidator = googleTokenValidator;
        _userRepository = userRepository;
        _tokenIssuer = tokenIssuer;
    }
    public async Task<Result<string>> HandleAsync(LoginUserCommand command, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrEmpty(command.IdToken)) return Result<string>.Failure("Token inválido");

            var resultGoogleUserInfo = await _googleTokenValidator.ValidateAsync(command.IdToken, ct);

            if (!resultGoogleUserInfo.IsSuccess) return Result<string>.Failure(resultGoogleUserInfo.Error ?? "");

            if (resultGoogleUserInfo.Value == null) return Result<string>.Failure(resultGoogleUserInfo.Error ?? "");

            var user = await _userRepository.GetByEmailAsync(resultGoogleUserInfo.Value.Email, ct);
            if (user is null) return Result<string>.Failure("Usuário não cadastrado");

            return Result<string>.Success(_tokenIssuer.Issue(user.Id, user.Email, user.Name));

        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
}