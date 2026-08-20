using CKL.Apps.VeraCryptTool.Commands;
using NUnit.Framework;

namespace CKL.Apps.VeraCryptTool.Tests;

public class MountCommandTests
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
    public void Execute_KeyFileMissing_ReturnsFailedResult()
    {
        var missingKeyFilePath = Path.Combine(_tempDirectory, "no-such-key-file.kf");
        var volumeFilePath = Path.Combine(_tempDirectory, "volume.vc");
        File.WriteAllText(volumeFilePath, "not a real volume, existence is all that matters here");

        var result = MountCommand.Execute(volumeFilePath, "Z", missingKeyFilePath, "1234");

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.ErrorMessage, Does.Contain("KeyFile not found"));
    }

    [Test]
    public void Execute_VolumeFileMissing_ReturnsFailedResult()
    {
        var keyFilePath = Path.Combine(_tempDirectory, "key-file.kf");
        CreateKeyFileCommand.Execute(keyFilePath, "1234", "random-password-12345!");
        var missingVolumeFilePath = Path.Combine(_tempDirectory, "no-such-volume.vc");

        var result = MountCommand.Execute(missingVolumeFilePath, "Z", keyFilePath, "1234");

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.ErrorMessage, Does.Contain("volume file not found"));
    }

    [Test]
    public void Execute_WrongPin_ReturnsFailedResult()
    {
        var keyFilePath = Path.Combine(_tempDirectory, "key-file.kf");
        CreateKeyFileCommand.Execute(keyFilePath, "1234", "random-password-12345!");
        var volumeFilePath = Path.Combine(_tempDirectory, "volume.vc");
        File.WriteAllText(volumeFilePath, "not a real volume, existence is all that matters here");

        var result = MountCommand.Execute(volumeFilePath, "Z", keyFilePath, "0000");

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.ErrorMessage, Does.Contain("wrong PIN"));
    }
}
