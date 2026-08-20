namespace CKL.Apps.VeraCryptTool;

/// <summary>
/// Reads the KeyFile PIN as one line of unmasked, clear-text console input — no
/// asterisk-echo, no re-entry/confirmation (see ADR-0035: masking/confirmation is
/// deliberately not used for <c>mount</c>, unlike a typical password prompt).
/// </summary>
internal static class PinReader
{
    public static string ReadPin() => ReadPin(Console.In);

    /// <summary>Testable overload — pass any <see cref="TextReader"/> (e.g. a <see cref="StringReader"/> in tests).</summary>
    internal static string ReadPin(TextReader reader) => reader.ReadLine() ?? string.Empty;
}
