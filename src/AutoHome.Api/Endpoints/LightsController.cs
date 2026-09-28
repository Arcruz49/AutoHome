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
    private readonly GetLightStateHandler _getLightStateHandler;
    private readonly SwitchLightByIdHandler _switchLightByIdHandler;
    public LightsController(ListAllLightsHandler listAllLightsHandler, RegisterLightHandler registerLightHandler, UpdateLightHandler updateLightHandler,
        RemoveLightHandler removeLightHandler, ScanLightsHandler scanLightsHandler, GetLightStateHandler getLightStateHandler, SwitchLightByIdHandler switchLightByIdHandler)
    {
        _listAllLightsHandler = listAllLightsHandler;
        _registerLightHandler = registerLightHandler;
        _updateLightHandler = updateLightHandler;
        _removeLightHandler = removeLightHandler;
        _scanLightsHandler = scanLightsHandler;
        _getLightStateHandler = getLightStateHandler;
        _switchLightByIdHandler = switchLightByIdHandler;
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> LightsList(CancellationToken ct)
    {
        var response = await _listAllLightsHandler.HandleAsync(new ListAllLightsCommand(), ct);
        
        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpGet("scan")]
    public async Task<IActionResult> ScanLights(CancellationToken ct)
    {
        var response = await _scanLightsHandler.HandleAsync(ct);
        
        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpPost("register")]
    public async Task<IActionResult> AddLight(RegisterLightRequest request, CancellationToken ct)
    {
        await _registerLightHandler.HandleAsync(new RegisterLightCommand(request.DeviceId, request.Name, request.IpAddress, request.SupportsColour), ct);

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
        await _removeLightHandler.HandleAsync(new RemoveLightCommand(id), ct);

        return Ok();
    }

    [HttpGet("light-state")]
    public async Task<IActionResult> LightState(Guid id, CancellationToken ct)
    {
        var response = await _getLightStateHandler.HandleAsync(new GetLightStateCommand(id), ct);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpPost("switch")]
    public async Task<IActionResult> TurnOnLight(SwitchLightRequest request, CancellationToken ct)
    {
        var response = await _switchLightByIdHandler.HandleAsync(new SwitchLightByIdCommand(request.Id, request.On), ct);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
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
