@echo off
setlocal

:: -------------------------------------------------------
:: Sample invocation of the "mount" command.
:: Edit the SET variables below, then run this script.
:: You will be prompted for the KeyFile's PIN interactively.
:: -------------------------------------------------------

set "VOLUME_FILE_PATH=C:\Temp\vc-test\vc-test.vc"
set "DRIVE_LETTER=X"
set "KEYFILE_PATH=C:\Temp\vc-test\key-file.kf"

dotnet run --project "%~dp0..\CKL.Apps.VeraCryptTool" -- mount "%VOLUME_FILE_PATH%" "%DRIVE_LETTER%" "%KEYFILE_PATH%"

exit /b %ERRORLEVEL%
