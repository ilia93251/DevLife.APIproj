
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src


COPY DevLife.APIproj/*.csproj ./DevLife.APIproj/
RUN dotnet restore ./DevLife.APIproj/DevLife.APIproj.csproj


COPY . .


WORKDIR /src/DevLife.APIproj
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevLife.APIproj.dll"]
