# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the .csproj first and restore dependencies
COPY EFCoreDemo/EFCoreDemo.csproj ./EFCoreDemo/
RUN dotnet restore ./EFCoreDemo/EFCoreDemo.csproj

# Now copy the full source code
COPY EFCoreDemo/ ./EFCoreDemo/

# Build the project
RUN dotnet publish ./EFCoreDemo/EFCoreDemo.csproj -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "EFCoreDemo.dll"]