using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace AutoHome.Api.Endpoints;

[ApiController]
[Route("auth")]
public class AuthController : BaseController
{
    
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

    // [HttpPost("register")]
    // public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    // {
        
    //     return Ok();
    // }

    // [Authorize]
    // [HttpGet("me")]
    // public IActionResult Me()
    // {
    //     var name = User.FindFirstValue(ClaimTypes.Name);
    //     var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //     return Ok(new { id, name });
    // }

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
