@echo off
REM Wechsle in den Ordner, in dem dieses Skript liegt
cd /d %~dp0

REM Starte Python HTTP-Server auf Port 8000
python -m http.server 8000

pause