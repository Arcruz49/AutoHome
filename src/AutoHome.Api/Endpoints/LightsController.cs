using AutoHome.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Api.Endpoints;

[ApiController]
// [Authorize]
[Route("light")]
public class LightsController : BaseController
{
    private readonly ListAllLightsHandler _listAllLightsHandler;
    public LightsController(ListAllLightsHandler listAllLightsHandler)
    {
        _listAllLightsHandler = listAllLightsHandler;
    }
    
    //list all
    [HttpGet("list")]
    public async Task<IActionResult> LightsList(CancellationToken ct)
    {
        var response = await _listAllLightsHandler.HandleAsync(new ListAllLightsCommand(), ct);
        
        return Ok(response) ;
    }

    //register
    [HttpPost("register ")]
    public async Task<IActionResult> AddLight()
    {
        return Ok();
    }

    //register
    [HttpPut("")]
    public async Task<IActionResult> UpdateLight()
    {
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
