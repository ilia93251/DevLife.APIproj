# Build stage (.NET 9.0 preview)
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

# Copy csproj and restore
COPY DevLife.APIproj/*.csproj ./
RUN dotnet restore

 
# Copy everything and publish
COPY DevLife.APIproj/. ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "DevLife.APIproj.dll"]
