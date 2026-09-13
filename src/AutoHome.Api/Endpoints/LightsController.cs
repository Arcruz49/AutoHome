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
    private readonly IGoogleTokenValidator _googleTokenValidator;
    public LightsController(IGoogleTokenValidator googleTokenValidator)
    {
        _googleTokenValidator = googleTokenValidator;
    }
    
    
    [HttpPost("test")]
    public async Task<IActionResult> TokenValidator(string token)
    {
        var response = await _googleTokenValidator.ValidateAsync(token);        

        return Ok(response);
    }

    

}
