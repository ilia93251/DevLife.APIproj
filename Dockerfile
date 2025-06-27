# -----------------------------------
# Build Stage
# -----------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Add nuget config to enable access to packages
COPY ["nuget.config", "./"]

# Copy csproj and restore
COPY ["DevLife.APIproj/DevLife.APIproj.csproj", "DevLife.APIproj/"]
RUN dotnet restore "DevLife.APIproj/DevLife.APIproj.csproj"

# Copy all project files and publish
COPY . .
WORKDIR "/src/DevLife.APIproj"
RUN dotnet publish "DevLife.APIproj.csproj" -c Release -o /app/publish

# -----------------------------------
# Final Runtime Image
# -----------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevLife.APIproj.dll"]
