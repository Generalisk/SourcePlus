@echo off
cd ../src
dotnet publish --os win --self-contained
dotnet publish --os linux --self-contained
pause
