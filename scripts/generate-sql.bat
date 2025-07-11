@echo off
echo Generating migration SQL...
dotnet ef migrations script -p src/Infrastructure -s src/WebService -o migrate.sql
echo Done. Output: migrate.sql
pause