using NUnit.Framework;

namespace CKL.Apps.VeraCryptTool.Tests;

public class PinReaderTests
{
    [Test]
    public void ReadPin_FromReaderWithOneLine_ReturnsThatLine()
    {
        using var reader = new StringReader("1234\n");

        var pin = PinReader.ReadPin(reader);

        Assert.That(pin, Is.EqualTo("1234"));
    }

    [Test]
    public void ReadPin_FromReaderWithNoInput_ReturnsEmptyString()
    {
        using var reader = new StringReader("");

        var pin = PinReader.ReadPin(reader);

        Assert.That(pin, Is.EqualTo(string.Empty));
    }
}
