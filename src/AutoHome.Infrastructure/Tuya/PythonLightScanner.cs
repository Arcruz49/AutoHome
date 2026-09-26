using System.Diagnostics;
using System.Text.Json;
using AutoHome.Application.Abstractions;
using AutoHome.Application.Common;
using Microsoft.Extensions.Options;

namespace AutoHome.Infrastructure.Tuya;

public class PythonLightScanner : ILightScanner
{
    private readonly TuyaOptions _options;

    public PythonLightScanner(IOptions<TuyaOptions> options)
    {
        _options = options.Value;
    }

    public async Task<Result<List<ScannedDevice>>> ScanAsync(CancellationToken ct = default)    {
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", "scan.py");

        var startInfo = new ProcessStartInfo
        {
            FileName = _options.PythonPath,
            Arguments = scriptPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(startInfo);
        if (process is null) return Result<List<ScannedDevice>>.Failure("não foi possível iniciar o python");

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
            return Result<List<ScannedDevice>>.Failure("scan expirou");
        }

        var stdout = await stdoutTask;
        var stderr = await stderrTask;

        if (process.ExitCode != 0) return Result<List<ScannedDevice>>.Failure(string.IsNullOrWhiteSpace(stderr) ? "scan falhou" : stderr);

        var devices = JsonSerializer.Deserialize<List<ScannedDevice>>(stdout, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return Result<List<ScannedDevice>>.Success(devices ?? []);
    }
    
}