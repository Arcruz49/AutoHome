using System.Diagnostics;
using System.Text.Json;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Common;
using Microsoft.Extensions.Options;

namespace AutoHome.Infrastructure.Tuya;

public class PythonLightControl : ILightControl
{
    private readonly TuyaOptions _options;

    public PythonLightControl(IOptions<TuyaOptions> options)
        => _options = options.Value;

    public Task<Result<LightState>> GetStateAsync(string deviceId, string ip, CancellationToken ct = default)
        => ExecuteAsync(deviceId, ip, "status", ct);

    public Task<Result<LightState>> SwitchAsync(string deviceId, string ip, bool on, CancellationToken ct = default)
        => ExecuteAsync(deviceId, ip, on ? "on" : "off", ct);
    private async Task<Result<LightState>> ExecuteAsync(string deviceId, string ip, string action, CancellationToken ct)
    {
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", "control.py");

        var devicesPath = Path.Combine(AppContext.BaseDirectory, _options.DevicesFilePath);

        var startInfo = new ProcessStartInfo
        {
            FileName = _options.PythonPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        startInfo.ArgumentList.Add(scriptPath);
        startInfo.ArgumentList.Add(devicesPath);
        startInfo.ArgumentList.Add(deviceId);
        startInfo.ArgumentList.Add(ip);
        startInfo.ArgumentList.Add(action);

        using var process = Process.Start(startInfo);
        if (process is null) return Result<LightState>.Failure("não foi possível iniciar o python");

        var stdoutTask = process.StandardOutput.ReadToEndAsync(ct);
        var stderrTask = process.StandardError.ReadToEndAsync(ct);

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        timeoutCts.CancelAfter(TimeSpan.FromSeconds(_options.TimeoutSeconds));

        try
        {
            await process.WaitForExitAsync(timeoutCts.Token);  
        }
        catch (OperationCanceledException)
        {
            process.Kill(entireProcessTree: true);
            return Result<LightState>.Failure("sync expirou");
        }

        var stdout = await stdoutTask;
        var stderr = await stderrTask;

        if (process.ExitCode != 0)
            return Result<LightState>.Failure(
                string.IsNullOrWhiteSpace(stderr) ? "comando falhou" : stderr);

        var state = JsonSerializer.Deserialize<LightState>(stdout, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return state is null ? Result<LightState>.Failure("resposta inválida do script") : Result<LightState>.Success(state);
    }
    
}