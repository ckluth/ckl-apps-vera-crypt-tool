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
        var encryptResult = CryptoService.EncryptString(strongPassword, pin);
        if (!encryptResult.Succeeded)
        {
            return Result.Fail("Failed to encrypt the strong password into the KeyFile.");
        }

        return WriteKeyFile(keyFilePath, encryptResult.Value);
    }

    private static Result WriteKeyFile(string keyFilePath, string keyFileContent)
    {
        try
        {
            File.WriteAllText(keyFilePath, keyFileContent);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to write KeyFile '{keyFilePath}': {ex.Message}");
        }

        return Result.Success;
    }
}
