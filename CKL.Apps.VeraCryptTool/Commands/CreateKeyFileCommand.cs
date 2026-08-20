using CKL.Apps.VeraCryptTool.Contracts;
using CKL.Libs.Crypt;
using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool.Commands;

/// <inheritdoc cref="ICreateKeyFileCommand" />
public sealed class CreateKeyFileCommand : ICreateKeyFileCommand
{
    // Never instantiated — only exists to carry the static, Result-native API below.
    private CreateKeyFileCommand()
    {
    }

    /// <inheritdoc />
    public static Result Execute(string keyFilePath, string pin, string strongPassword)
    {
        var tempPlainTextPath = Path.GetTempFileName();
        try
        {
            return EncryptViaTempFile(keyFilePath, pin, strongPassword, tempPlainTextPath);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to write KeyFile '{keyFilePath}': {ex.Message}");
        }
        finally
        {
            File.Delete(tempPlainTextPath);
        }
    }

    private static Result EncryptViaTempFile(string keyFilePath, string pin, string strongPassword, string tempPlainTextPath)
    {
        File.WriteAllText(tempPlainTextPath, strongPassword);

        // captureLastAccessTime: false — the temp file's own timestamps are meaningless
        // metadata for a freshly staged plaintext password; no reason to embed them.
        var encryptResult = CryptoService.EncryptFile(tempPlainTextPath, keyFilePath, pin, captureLastAccessTime: false);
        return encryptResult.Succeeded
            ? Result.Success
            : Result.Fail("Failed to encrypt the strong password into the KeyFile.");
    }
}
