using System.Security.Authentication;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Common;
using AutoHome.Application.DTOs;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class GetLightByIdHandler
{
    private readonly ILightRepository _lightRepository;
    public GetLightByIdHandler(ILightRepository lightRepository)
    {
        _lightRepository = lightRepository;
    }
    public async Task<Result<LightDto>> HandleAsync(GetLightByIdCommand command, CancellationToken ct)
    {
        var light = await _lightRepository.GetByIdAsync(command.Id, ct);

        if(light == null) return Result<LightDto>.Failure("Light not found");
        
        var dto = new LightDto(light.Id, light.DeviceId, light.Name, light.IpAddress, light.SupportsColour, light.CreatedAt);

        return Result<LightDto>.Success(dto);
    }
}