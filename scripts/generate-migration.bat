@echo off
set /p MIGRATION_NAME=Enter migration name: 

dotnet ef migrations add %MIGRATION_NAME% -p src/Infrastructure -s src/WebService
pause