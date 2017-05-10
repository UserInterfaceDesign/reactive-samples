@echo off

setlocal

title build
set NUGET=tools\nuget.exe
set LOG=build.log
set ROOT=.
set WHAT=src\ReactiveSamples.sln
set CONFIG=Release

if exist %LOG% del %LOG%

:nuget
call %NUGET% restore %WHAT% >> %LOG%
if %ERRORLEVEL%==0 ( 
echo Nuget restore success.
) else (
echo Nuget restore failure. Logfile: %LOG%.
goto end
)

:build
msbuild %WHAT% /p:Configuration=%CONFIG% /nologo /verbosity:normal >> %LOG% 2>>&1
if %ERRORLEVEL%==0 ( echo Build success. ) else (echo Build failure. Logfile: %LOG%. )

:end