using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool.Contracts;

/// <summary>Encrypts a strong password into a KeyFile, keyed by a PIN (R-02).</summary>
public interface ICreateKeyFileCommand
{
    /// <summary>
    /// Encrypts <paramref name="strongPassword"/> using <paramref name="pin"/> as the
    /// password for <c>CKL.Libs.Crypt</c>'s PBKDF2-derived key, via its byte-array
    /// <c>Encrypt</c> overload (the strong password never touches disk — only the
    /// resulting ciphertext bytes do), and writes the resulting raw binary container
    /// directly to <paramref name="keyFilePath"/> — no additional custom framing.
    /// </summary>
    static abstract Result Execute(string keyFilePath, string pin, string strongPassword);
}
