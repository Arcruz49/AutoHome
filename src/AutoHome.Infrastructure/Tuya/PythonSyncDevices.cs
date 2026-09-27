using System.Diagnostics;
using System.Text.Json;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Common;
using Microsoft.Extensions.Options;

namespace AutoHome.Infrastructure.Tuya;

public class PythonSyncDevices : ITuyaSync
{
    private readonly TuyaOptions _options;

    public PythonSyncDevices(IOptions<TuyaOptions> options)
    {
        _options = options.Value;
    }

    public async Task<Result<int>> SyncAsync(CancellationToken ct = default)   
    {
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", "sync_devices.py");

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

        startInfo.Environment["TUYA_API_KEY"] = _options.ApiKey;
        startInfo.Environment["TUYA_API_SECRET"] = _options.ApiSecret;
        startInfo.Environment["TUYA_API_REGION"] = _options.ApiRegion;

        using var process = Process.Start(startInfo);
        if (process is null) return Result<int>.Failure("não foi possível iniciar o python");

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
            return Result<int>.Failure("sync expirou");
        }

        var stdout = await stdoutTask;
        var stderr = await stderrTask;

        if (process.ExitCode != 0) return Result<int>.Failure(string.IsNullOrWhiteSpace(stderr) ? "sync falhou" : stderr);

        using var doc = JsonDocument.Parse(stdout);
        var count = doc.RootElement.GetProperty("count").GetInt32();

        return Result<int>.Success(count);
    }
    
}