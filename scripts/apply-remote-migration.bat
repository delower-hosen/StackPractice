@echo off
echo Applying SQL migration to Render remote DB...

docker run -it --rm ^
  -v "%cd%\..:/workspace" ^
  -w /workspace ^
  postgres:16 ^
  psql "host=dpg-d1oh8eidbo4c73b2lh40-a.oregon-postgres.render.com user=stackuser dbname=stackpractice password=PG6Fo6g3NMmclcYAPZZZeMwEwg3ehD6U sslmode=require" ^
  -f migrate.sql

pause
