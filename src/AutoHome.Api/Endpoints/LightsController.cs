using AutoHome.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace AutoHome.Api.Endpoints;

[ApiController]
[Route("light")]
public class LightsController : BaseController
{
    
    public LightsController()
    {
    }
    
    //read stats
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
