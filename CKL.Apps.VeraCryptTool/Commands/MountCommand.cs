using System.Text;
using CKL.Apps.VeraCryptTool.Contracts;
using CKL.Apps.VeraCryptTool.VeraCrypt;
using CKL.Libs.Crypt;
using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool.Commands;

/// <inheritdoc cref="IMountCommand" />
public sealed class MountCommand : IMountCommand
{
    // Never instantiated — only exists to carry the static, Result-native API below.
    private MountCommand()
    {
    }

    /// <inheritdoc />
    public static Result Execute(string volumeFilePath, string driveLetter, string keyFilePath, string pin)
    {
        if (!File.Exists(keyFilePath))
        {
            return Result.Fail($"KeyFile not found: '{keyFilePath}'.");
        }

        if (!File.Exists(volumeFilePath))
        {
            return Result.Fail($"VeraCrypt volume file not found: '{volumeFilePath}'.");
        }

        var strongPasswordResult = RecoverStrongPassword(keyFilePath, pin);
        if (!strongPasswordResult.Succeeded)
        {
            return strongPasswordResult.ToResult();
        }

        var locateResult = VeraCryptLocator.Locate();
        if (!locateResult.Succeeded)
        {
            return locateResult.ToResult();
        }

        var normalizedDriveLetter = driveLetter.TrimEnd(':');
        return VeraCryptMounter.Mount(locateResult.Value, volumeFilePath, normalizedDriveLetter, strongPasswordResult.Value);
    }

    private static Result<string> RecoverStrongPassword(string keyFilePath, string pin)
    {
        byte[] keyFileBytes;
        try
        {
            keyFileBytes = File.ReadAllBytes(keyFilePath);
        }
        catch (Exception ex)
        {
            return Result<string>.Fail($"Failed to read KeyFile '{keyFilePath}': {ex.Message}");
        }

        var decryptResult = CryptoService.Decrypt(keyFileBytes, pin);
        if (!decryptResult.Succeeded)
        {
            // Deliberately generic — never surface *why* decryption failed (matches
            // ckl-libs-crypt's own hardened decryption-failure posture, ADR-0009).
            return Result<string>.Fail("Failed to unlock the KeyFile — wrong PIN or a corrupted KeyFile.");
        }

        return Encoding.UTF8.GetString(decryptResult.Value);
    }
}
