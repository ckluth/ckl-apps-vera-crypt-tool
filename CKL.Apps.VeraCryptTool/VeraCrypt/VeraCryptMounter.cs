using System.Diagnostics;
using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool.VeraCrypt;

/// <summary>Invokes VeraCrypt.exe's own command-line mount switches (see ADR-0035).</summary>
internal static class VeraCryptMounter
{
    public static Result Mount(string veraCryptExePath, string volumeFilePath, string driveLetter, string password)
    {
        var startInfo = BuildStartInfo(veraCryptExePath, volumeFilePath, driveLetter, password);

        using var process = StartProcess(startInfo);
        if (process is null)
        {
            return Result.Fail($"Failed to start '{veraCryptExePath}'.");
        }

        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            return Result.Fail($"VeraCrypt.exe exited with code {process.ExitCode}.");
        }

        return Result.Success;
    }

    private static ProcessStartInfo BuildStartInfo(string veraCryptExePath, string volumeFilePath, string driveLetter, string password)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = veraCryptExePath,
            UseShellExecute = false,
        };

        // ArgumentList (not a hand-quoted argument string) so the password can contain
        // spaces/special characters (e.g. "!") without manual escaping.
        startInfo.ArgumentList.Add("/v");
        startInfo.ArgumentList.Add(volumeFilePath);
        startInfo.ArgumentList.Add("/l");
        startInfo.ArgumentList.Add(driveLetter);
        startInfo.ArgumentList.Add("/p");
        startInfo.ArgumentList.Add(password);
        startInfo.ArgumentList.Add("/q");
        startInfo.ArgumentList.Add("/s");

        return startInfo;
    }

    private static Process? StartProcess(ProcessStartInfo startInfo) => Process.Start(startInfo);
}
