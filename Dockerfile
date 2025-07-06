# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the .csproj file and restore dependencies
COPY EFCoreDemo/WebService/WebService.csproj ./WebService/
RUN dotnet restore ./WebService/WebService.csproj

# Copy the rest of the app
COPY EFCoreDemo/WebService/ ./WebService/

# Publish the app
RUN dotnet publish ./WebService/WebService.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebService.dll"]