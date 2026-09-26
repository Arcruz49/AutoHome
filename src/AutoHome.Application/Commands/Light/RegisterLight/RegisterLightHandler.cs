using System.Security.Authentication;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Common;
using AutoHome.Application.DTOs;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class RegisterLightHandler
{
    private readonly ILightRepository _lightRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterLightHandler(ILightRepository lightRepository, IUnitOfWork unitOfWork)
    {
        _lightRepository = lightRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task HandleAsync(RegisterLightCommand command, CancellationToken ct)
    {
        var light = new Light
        {
            Id = Guid.NewGuid(),
            DeviceId = command.DeviceId,
            Name = command.Name,
            IpAddress = command.IpAddress,
            SupportsColour = command.SupportsColour,
            CreatedAt = DateTime.UtcNow,
        };

        await _lightRepository.RegisterAsync(light, ct);
        await _unitOfWork.SaveChangesAsync(ct);

    }
}