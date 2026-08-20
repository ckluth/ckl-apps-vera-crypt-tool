@echo off
setlocal

:: -------------------------------------------------------
:: Sample invocation of the "create-keyfile" command.
:: Edit the SET variables below, then run this script.
:: -------------------------------------------------------

set "KEYFILE_PATH=C:\Temp\vc-test\key-file.kf"
set "PIN=1234"
set "STRONG_PASSWORD=random-password-12345!"

dotnet run --project "%~dp0..\CKL.Apps.VeraCryptTool" -- create-keyfile "%KEYFILE_PATH%" "%PIN%" "%STRONG_PASSWORD%"

exit /b %ERRORLEVEL%
