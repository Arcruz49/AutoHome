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
    private readonly LoginUserHandler _loginUserHandler;

    public AuthController(RegisterUserHandler registerUserHandler, LoginUserHandler loginUserHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
    }
    
    // [EnableRateLimiting("login")]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken ct)
    {
        var result = await _loginUserHandler.HandleAsync(new LoginUserCommand(request.IdToken), ct);
        return result.IsSuccess ? Ok(result) : BadRequest(new { error = result });
    }

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
