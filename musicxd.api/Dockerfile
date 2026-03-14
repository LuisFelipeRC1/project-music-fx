# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MusicXD.sln .
COPY MusicXD.Domain/MusicXD.Domain.csproj MusicXD.Domain/
COPY MusicXD.Application/MusicXD.Application.csproj MusicXD.Application/
COPY MusicXD.Infrastructure/MusicXD.Infrastructure.csproj MusicXD.Infrastructure/
COPY MusicXD.API/MusicXD.API.csproj MusicXD.API/

RUN dotnet restore MusicXD.sln

COPY . .

RUN dotnet publish MusicXD.API/MusicXD.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN addgroup --system --gid 1001 appgroup && \
    adduser --system --uid 1001 --ingroup appgroup appuser

COPY --from=build --chown=appuser:appgroup /app/publish .

USER appuser

EXPOSE 8080

ENTRYPOINT ["dotnet", "MusicXD.API.dll"]
