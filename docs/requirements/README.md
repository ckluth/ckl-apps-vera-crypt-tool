# Requirements — CKL.Apps.VeraCryptTool

Requirements are stable and never reused; a changed requirement is
edited in place.
Provenance: [ckl-builder ADR-0035](https://github.com/ckluth/ckl-builder/blob/main/docs/decisions/0035-ckl-apps-vera-crypt-tool.md).

### R-01: Subcommand invocation contract
The tool must be invoked with a subcommand keyword as its first
argument (`create-keyfile` or `mount`); an unrecognized or missing
subcommand must print usage to `stderr` and exit `1`.

### R-02: `create-keyfile` command
`create-keyfile <keyFilePath> <pin> <strongPassword>` must encrypt
`strongPassword` using a PIN-derived key and write the result to
`keyFilePath`.

### R-03: `mount` command
`mount <volumeFilePath> <driveLetter> <keyFilePath>` must prompt for
the KeyFile's PIN via unmasked, clear-text console input typed once,
decrypt the KeyFile with it to recover the strong password, and mount
the VeraCrypt volume at `volumeFilePath` to `driveLetter` using that
password.

### R-04: `VeraCrypt.exe` auto-detection
The tool must locate `VeraCrypt.exe` by probing
`C:\Program Files\VeraCrypt\VeraCrypt.exe` then
`C:\Program Files (x86)\VeraCrypt\VeraCrypt.exe`, and fail with a
clear error if neither exists. No configuration file is used.

### R-05: Result-pattern error reporting and exit codes
Every failure must print one clear, user-facing message to `stderr`
(never exception stack traces) and exit `1`; success exits `0`.
