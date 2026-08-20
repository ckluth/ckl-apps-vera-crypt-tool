# CKL.Apps.VeraCryptTool

A console tool wrapping VeraCrypt: create a PIN-protected KeyFile
holding a strong password, and mount a VeraCrypt volume using the
strong password recovered from such a KeyFile.

See [`ckl-builder`](https://github.com/ckluth/ckl-builder) for the
ecosystem's repo-family conventions and the decision-trail this repo
follows (in particular
[ADR-0035](https://github.com/ckluth/ckl-builder/blob/main/docs/decisions/0035-ckl-apps-vera-crypt-tool.md)).

## Commands

```
dotnet run --project CKL.Apps.VeraCryptTool -- create-keyfile <keyFilePath> <pin> <strongPassword>
dotnet run --project CKL.Apps.VeraCryptTool -- mount <volumeFilePath> <driveLetter> <keyFilePath>
```

`mount` prompts for the KeyFile's PIN interactively (typed once, in
clear text).

## Verify locally

```
.local-build\run-build-and-test.cmd
```

---

*CKL.Apps.VeraCryptTool — © 2026 ckluth — MIT License*
