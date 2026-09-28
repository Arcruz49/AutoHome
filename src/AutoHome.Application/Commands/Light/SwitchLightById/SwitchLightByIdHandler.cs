using AutoHome.Application.Abstractions;
using AutoHome.Application.Common;
using AutoHome.Domain.Abstractions.Repositories;

namespace AutoHome.Application.Commands;

public class SwitchLightByIdHandler
{
    private readonly ILightControl _lightControl;
    private readonly ILightRepository _lightRepository;
    public SwitchLightByIdHandler(ILightControl lightControl, ILightRepository lightRepository)
    {
        _lightControl = lightControl;
        _lightRepository = lightRepository;
    }
    public async Task<Result<LightState>> HandleAsync(SwitchLightByIdCommand command, CancellationToken ct)
    {
        try
        {
            var light = await _lightRepository.GetByIdAsync(command.Id, ct);
            
            if(light == null) return Result<LightState>.Failure("Light not found");

            return await _lightControl.SwitchAsync(light.DeviceId, light.IpAddress, command.On, ct);
        }
        catch(NullReferenceException ex)
        {
            return Result<LightState>.Failure(ex.Message);
        }
        catch(Exception ex)
        {
            return Result<LightState>.Failure("Erro: " + ex);
        }
    }
}