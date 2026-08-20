using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool.Contracts;

/// <summary>Mounts a VeraCrypt volume using the strong password recovered from a KeyFile (R-03).</summary>
public interface IMountCommand
{
    /// <summary>
    /// Validates <paramref name="keyFilePath"/> and <paramref name="volumeFilePath"/> exist,
    /// decrypts the KeyFile with <paramref name="pin"/> (already read by the caller — this
    /// method performs no console I/O itself), locates VeraCrypt.exe, and mounts the volume.
    /// </summary>
    static abstract Result Execute(string volumeFilePath, string driveLetter, string keyFilePath, string pin);
}
