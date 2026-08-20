# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project aims to adhere to
[Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0]

### Added

- Initial `CKL.Apps.VeraCryptTool` console application: `create-keyfile`
  (encrypts a strong password into a KeyFile, keyed by a PIN, via
  `CKL.Libs.Crypt`) and `mount` (recovers the strong password from a
  KeyFile using an interactively entered PIN, then mounts a VeraCrypt
  volume via `VeraCrypt.exe`).
