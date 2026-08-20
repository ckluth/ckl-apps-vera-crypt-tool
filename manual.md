# Manual — CKL.Apps.VeraCryptTool

A console tool wrapping VeraCrypt: create a PIN-protected KeyFile holding a
strong password, and mount a VeraCrypt volume using the strong password
recovered from such a KeyFile.

## Commands

### `create-keyfile`

```
dotnet run --project CKL.Apps.VeraCryptTool -- create-keyfile <keyFilePath> <pin> <strongPassword>
```

| Argument         | Meaning                                                    |
| ---------------- | ----------------------------------------------------------- |
| `keyFilePath`    | Path to write the encrypted KeyFile to.                     |
| `pin`            | Short PIN used to encrypt `strongPassword` into the KeyFile.|
| `strongPassword` | The volume's real, strong password, protected by the PIN.   |

Encrypts `strongPassword` with a PBKDF2-derived key from `pin` and writes the
result to `keyFilePath`. Exits `0` and prints `OK.` on success; otherwise
exits `1` and prints an error to `stderr`.

### `mount`

```
dotnet run --project CKL.Apps.VeraCryptTool -- mount <volumeFilePath> <driveLetter> <keyFilePath>
```

| Argument        | Meaning                                             |
| --------------- | ---------------------------------------------------- |
| `volumeFilePath`| Path to the VeraCrypt volume file (`.vc`) to mount.  |
| `driveLetter`   | Drive letter to mount the volume to (e.g. `X`).      |
| `keyFilePath`   | KeyFile previously created with `create-keyfile`.    |

Prompts interactively for the KeyFile's PIN (typed once, in clear text — no
masking/confirmation, by design), decrypts the KeyFile to recover the strong
password, locates `VeraCrypt.exe` automatically, and mounts the volume. Exits
`0` and prints `OK.` on success; a wrong PIN or any other failure exits `1`
with a clear error message and mounts nothing.

## Sample scripts

See `samples\create-keyfile.cmd` and `samples\mount.cmd` — copy either,
edit the `SET` variables at the top, and run.

## Verify locally

```
.local-build\run-build-and-test.cmd
```
