FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/MusicXD.API/MusicXD.API.csproj", "MusicXD.API/"]
COPY ["src/MusicXD.Application/MusicXD.Application.csproj", "MusicXD.Application/"]
COPY ["src/MusicXD.Domain/MusicXD.Domain.csproj", "MusicXD.Domain/"]
COPY ["src/MusicXD.Infrastructure/MusicXD.Infrastructure.csproj", "MusicXD.Infrastructure/"]
RUN dotnet restore "MusicXD.API/MusicXD.API.csproj"
COPY src/ .
RUN dotnet build "MusicXD.API/MusicXD.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicXD.API/MusicXD.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicXD.API.dll"]
