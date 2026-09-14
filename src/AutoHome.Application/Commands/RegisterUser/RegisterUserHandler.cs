using System.Security.Authentication;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Common;
using AutoHome.Application.DTOs;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class RegisterUserHandler
{
    private readonly IGoogleTokenValidator _googleTokenValidator;
    private readonly IUserRepository _userRepository;
    private readonly IConfigRepository _configRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserHandler(IGoogleTokenValidator googleTokenValidator, IUserRepository userRepository, IConfigRepository configRepository, IUnitOfWork unitOfWork)
    {
        _googleTokenValidator = googleTokenValidator;
        _userRepository = userRepository;
        _configRepository = configRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<UserDto>> HandleAsync(RegisterUserCommand command, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrEmpty(command.IdToken)) return Result<UserDto>.Failure("Token inválido");

            var resultGoogleUserInfo = await _googleTokenValidator.ValidateAsync(command.IdToken, ct);

            if (!resultGoogleUserInfo.IsSuccess) return Result<UserDto>.Failure(resultGoogleUserInfo.Error ?? "");

            var isThereAnyUser = await _userRepository.AnyAsync(ct);
            var registerConfig = await VerifyRegisterConfig(ct);
            var duplicatedEmail = await IsDuplicatedUserEmail(resultGoogleUserInfo.Value?.Email ?? "", ct);

            if (isThereAnyUser && !registerConfig) return Result<UserDto>.Failure("Usuários sem permissão de registro");

            if (duplicatedEmail) return Result<UserDto>.Failure("Email já cadastrado");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = resultGoogleUserInfo.Value?.Email ?? throw new InvalidCredentialException("Email inválido"),
                Name = resultGoogleUserInfo.Value.Name ?? throw new InvalidCredentialException("Nome inválido"),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user, ct);

            await _unitOfWork.SaveChangesAsync();

            return Result<UserDto>.Success(new UserDto(user.Id, user.Name, user.Email));

        }
        catch (InvalidCredentialException ex)
        {
            return Result<UserDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<UserDto>.Failure(ex.Message);
        }
    }

    public async Task<bool> VerifyRegisterConfig(CancellationToken ct)
    {
        var result = await _configRepository.GetConfigAsync(ct);

        return result != null && result.RegisterRouteEnabled;
    }

    public async Task<bool> IsDuplicatedUserEmail(string email, CancellationToken ct)
        => await _userRepository.GetByEmailAsync(email, ct) != null;
    

}