using AutoHome.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Api.Endpoints;

[ApiController]
[Route("tuya")]
public class TuyaController : BaseController
{
    private readonly SyncDevicesHandler _syncDevicesHandler;

    public TuyaController(SyncDevicesHandler syncDevicesHandler)
    {
        _syncDevicesHandler = syncDevicesHandler;
    }
    
    [HttpPost("sync")]
    public async Task<IActionResult> SyncDevices(CancellationToken ct)
    {
        var result = await _syncDevicesHandler.HandleAsync(ct);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

}
