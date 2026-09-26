using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class RemoveLightHandler
{
    private readonly ILightRepository _lightRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveLightHandler(ILightRepository lightRepository, IUnitOfWork unitOfWork)
    {
        _lightRepository = lightRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task HandleAsync(RemoveLightCommand command, CancellationToken ct)
    {
        var light = await _lightRepository.GetByIdAsync(command.Id) ?? throw new NullReferenceException("Light not found");

        _lightRepository.Delete(light);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}