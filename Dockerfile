# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy entire source folder with solution and projects
COPY src/ ./

# Restore all projects via the solution file
RUN dotnet restore StackPractice.sln

# Publish only the WebService project
RUN dotnet publish WebService/WebService.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WebService.dll"]