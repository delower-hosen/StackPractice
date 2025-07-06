# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as separate layer
COPY EFCoreDemo/*.csproj ./EFCoreDemo/
RUN dotnet restore EFCoreDemo/EFCoreDemo.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish EFCoreDemo/EFCoreDemo.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "EFCoreDemo.dll"]
