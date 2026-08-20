using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool.VeraCrypt;

/// <summary>Locates VeraCrypt.exe by probing known default install paths (R-04). No configuration file.</summary>
internal static class VeraCryptLocator
{
    private static readonly string[] CandidatePaths =
    [
        @"C:\Program Files\VeraCrypt\VeraCrypt.exe",
        @"C:\Program Files (x86)\VeraCrypt\VeraCrypt.exe",
    ];

    public static Result<string> Locate() => Locate(File.Exists);

    /// <summary>Testable overload — pass a fake <c>fileExists</c> predicate in tests.</summary>
    internal static Result<string> Locate(Func<string, bool> fileExists)
    {
        foreach (var path in CandidatePaths)
        {
            if (fileExists(path))
            {
                return path;
            }
        }

        return Result<string>.Fail("VeraCrypt.exe was not found at any known install location.");
    }
}
