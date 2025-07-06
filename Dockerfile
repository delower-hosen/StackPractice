# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything needed for restore/build
COPY EFCoreDemo/EFCoreDemo.sln ./
COPY EFCoreDemo/WebService/ ./WebService/
COPY EFCoreDemo/Domain/ ./Domain/
COPY EFCoreDemo/ServiceCollector/ ./ServiceCollector/
COPY EFCoreDemo/Services/ ./Services/

# Restore and publish
RUN dotnet restore WebService/WebService.csproj
RUN dotnet publish WebService/WebService.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebService.dll"]