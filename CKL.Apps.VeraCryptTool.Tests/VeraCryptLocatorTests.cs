using CKL.Apps.VeraCryptTool.VeraCrypt;
using NUnit.Framework;

namespace CKL.Apps.VeraCryptTool.Tests;

public class VeraCryptLocatorTests
{
    [Test]
    public void Locate_FirstCandidateExists_ReturnsFirstCandidatePath()
    {
        var result = VeraCryptLocator.Locate(path => path == @"C:\Program Files\VeraCrypt\VeraCrypt.exe");

        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Value, Is.EqualTo(@"C:\Program Files\VeraCrypt\VeraCrypt.exe"));
    }

    [Test]
    public void Locate_OnlySecondCandidateExists_ReturnsSecondCandidatePath()
    {
        var result = VeraCryptLocator.Locate(path => path == @"C:\Program Files (x86)\VeraCrypt\VeraCrypt.exe");

        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Value, Is.EqualTo(@"C:\Program Files (x86)\VeraCrypt\VeraCrypt.exe"));
    }

    [Test]
    public void Locate_NoCandidateExists_ReturnsFailedResult()
    {
        var result = VeraCryptLocator.Locate(_ => false);

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.ErrorMessage, Does.Contain("not found"));
    }
}
