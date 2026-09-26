using AutoHome.Application.Commands;
using AutoHome.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Api.Endpoints;

[ApiController]
// [Authorize]
[Route("light")]
public class LightsController : BaseController
{
    private readonly ListAllLightsHandler _listAllLightsHandler;
    private readonly RegisterLightHandler _registerLightHandler;
    private readonly UpdateLightHandler _updateLightHandler;
    private readonly RemoveLightHandler _removeLightHandler;
    private readonly ScanLightsHandler _scanLightsHandler;
    public LightsController(ListAllLightsHandler listAllLightsHandler, RegisterLightHandler registerLightHandler, UpdateLightHandler updateLightHandler,
        RemoveLightHandler removeLightHandler, ScanLightsHandler scanLightsHandler)
    {
        _listAllLightsHandler = listAllLightsHandler;
        _registerLightHandler = registerLightHandler;
        _updateLightHandler = updateLightHandler;
        _removeLightHandler = removeLightHandler;
        _scanLightsHandler = scanLightsHandler;
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> LightsList(CancellationToken ct)
    {
        var response = await _listAllLightsHandler.HandleAsync(new ListAllLightsCommand(), ct);
        
        return Ok(response) ;
    }

    [HttpGet("scan")]
    public async Task<IActionResult> ScanLights(CancellationToken ct)
    {
        var response = await _scanLightsHandler.HandleAsync(ct);
        
        return Ok(response) ;
    }

    [HttpPost("register")]
    public async Task<IActionResult> AddLight(RegisterLightRequest request, CancellationToken ct)
    {
        var command = new RegisterLightCommand(request.DeviceId, request.Name, request.IpAddress, request.SupportsColour);

        await _registerLightHandler.HandleAsync(command, ct);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLight(UpdateLightRequest request, CancellationToken ct)
    {
        var command = new UpdateLightCommand(request.Id, request.DeviceId, request.Name, request.IpAddress, request.SupportsColour);

        await _updateLightHandler.HandleAsync(command, ct);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLight(Guid id, CancellationToken ct)
    {
        var command = new RemoveLightCommand(id);

        await _removeLightHandler.HandleAsync(command, ct);

        return Ok();
    }

    // light stats
    [HttpGet("")]
    public async Task<IActionResult> LightStats()
    {
        return Ok();
    }

    //turn on
    [HttpPost("turn-on")]
    public async Task<IActionResult> TurnOnLight()
    {
        return Ok();
    }

    //turn off
    [HttpPost("turn-off")]
    public async Task<IActionResult> TurnOffLight()
    {
        return Ok();
    }

    //brightness
    [HttpPost("brightness")]
    public async Task<IActionResult> LightBrightness()
    {
        return Ok();
    }

    //temperature
    [HttpPost("temperature")]
    public async Task<IActionResult> LightTemperature()
    {
        return Ok();
    }

    //change color
    [HttpPost("color")]
    public async Task<IActionResult> LightColor()
    {
        return Ok();
    }

    

}
