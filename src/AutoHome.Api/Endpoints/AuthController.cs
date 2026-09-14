using AutoHome.Application.Commands;
using AutoHome.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace AutoHome.Api.Endpoints;

[ApiController]
[Route("auth")]
public class AuthController : BaseController
{
    private readonly RegisterUserHandler _registerUserHandler;

    public AuthController(RegisterUserHandler registerUserHandler)
    {
        _registerUserHandler = registerUserHandler;
    }
    
    // // [EnableRateLimiting("login")]
    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] LoginRequest request)
    // {
    //     Response.Cookies.Append("autohome_token", result.token, new CookieOptions
    //     {
    //         HttpOnly = true,
    //         Secure = isHttps,
    //         SameSite = isHttps ? SameSiteMode.None : SameSiteMode.Lax,
    //         Expires = DateTime.UtcNow.AddMinutes(60)
    //     });

    //     return Ok();
    // }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken ct)
    {
        var result = await _registerUserHandler.HandleAsync(new RegisterUserCommand(request.IdToken), ct);
        return result.IsSuccess ? Ok(result) : BadRequest(new { error = result });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var isHttps = Request.IsHttps;
        Response.Cookies.Append("autohome_token", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = isHttps,
            SameSite = isHttps ? SameSiteMode.None : SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(-1)
        });

        return Ok();
    }

}
