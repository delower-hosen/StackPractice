# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything needed for restore/build
COPY src/WebService/ ./WebService/
COPY src/Domain/ ./Domain/
COPY src/ServiceCollector/ ./ServiceCollector/
COPY src/Services/ ./Services/
COPY src/StackPractice.sln ./

# Restore and publish
RUN dotnet restore WebService/WebService.csproj
RUN dotnet publish WebService/WebService.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebService.dll"]