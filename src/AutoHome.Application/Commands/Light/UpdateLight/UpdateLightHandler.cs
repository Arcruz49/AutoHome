using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class UpdateLightHandler
{
    private readonly ILightRepository _lightRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateLightHandler(ILightRepository lightRepository, IUnitOfWork unitOfWork)
    {
        _lightRepository = lightRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task HandleAsync(UpdateLightCommand command, CancellationToken ct)
    {
        var light = await _lightRepository.GetByIdAsync(command.Id) ?? throw new NullReferenceException("Light not found");

        light.DeviceId = command.DeviceId;
        light.IpAddress = command.IpAddress;
        light.Name = command.Name;
        light.SupportsColour = command.SupportsColour;

        _lightRepository.Update(light);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}