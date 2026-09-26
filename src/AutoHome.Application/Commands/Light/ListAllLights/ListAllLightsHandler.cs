using System.Security.Authentication;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Common;
using AutoHome.Application.DTOs;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Domain.Entities;

namespace AutoHome.Application.Commands;

public class ListAllLightsHandler
{
    private readonly ILightRepository _lightRepository;
    public ListAllLightsHandler(ILightRepository lightRepository)
    {
        _lightRepository = lightRepository;
    }
    public async Task<Result<List<LightDto>>> HandleAsync(ListAllLightsCommand command, CancellationToken ct)
    {
        var lights = await _lightRepository.GetAllAsync(ct);

        var dtos = lights?.Select(l => new LightDto(l.Id, l.DeviceId, l.Name, l.IpAddress, l.SupportsColour, l.CreatedAt)).ToList();

        return Result<List<LightDto>>.Success(dtos ?? []);
    }
}