using CKL.Apps.VeraCryptTool.Commands;
using CKL.Libs.Crypt;
using NUnit.Framework;

namespace CKL.Apps.VeraCryptTool.Tests;

public class CreateKeyFileCommandIntegrationTests
{
    private string _tempDirectory = "";

    [SetUp]
    public void SetUp()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), "ckl-apps-vera-crypt-tool-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDirectory);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_tempDirectory))
        {
            Directory.Delete(_tempDirectory, recursive: true);
        }
    }

    [Test]
    public void Execute_ThenDecryptWithSamePin_RoundTripsStrongPassword()
    {
        var keyFilePath = Path.Combine(_tempDirectory, "key-file.kf");

        var createResult = CreateKeyFileCommand.Execute(keyFilePath, "1234", "random-password-12345!");

        Assert.That(createResult.Succeeded, Is.True);
        Assert.That(File.Exists(keyFilePath), Is.True);
        var decryptResult = CryptoService.DecryptString(File.ReadAllText(keyFilePath), "1234");
        Assert.That(decryptResult.Succeeded, Is.True);
        Assert.That(decryptResult.Value, Is.EqualTo("random-password-12345!"));
    }

    [Test]
    public void Execute_ThenDecryptWithWrongPin_Fails()
    {
        var keyFilePath = Path.Combine(_tempDirectory, "key-file.kf");
        CreateKeyFileCommand.Execute(keyFilePath, "1234", "random-password-12345!");

        var decryptResult = CryptoService.DecryptString(File.ReadAllText(keyFilePath), "0000");

        Assert.That(decryptResult.Succeeded, Is.False);
    }

    [Test]
    public void Execute_InvalidKeyFilePath_ReturnsFailedResult()
    {
        var invalidPath = Path.Combine(_tempDirectory, "no-such-folder", "key-file.kf");

        var result = CreateKeyFileCommand.Execute(invalidPath, "1234", "pw");

        Assert.That(result.Succeeded, Is.False);
    }
}
