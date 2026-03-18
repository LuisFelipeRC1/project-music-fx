using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MusicXD.Application.Interfaces;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyAuthService;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyClient;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyConfiguration;
using MusicXD.Infrastructure.Caching;
using MusicXD.Infrastructure.Jobs;
using MusicXD.Infrastructure.Persistence;
using MusicXD.Infrastructure.Services;

namespace MusicXD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<RedisCacheService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.Configure<SpotifyOptions>(configuration.GetSection("Spotify"));
        services.AddSingleton<ISpotifyAuthService, SpotifyAuthService>();
        services.AddHttpClient(SpotifyAuthService.HttpClientName, (serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SpotifyOptions>>().Value;
            httpClient.BaseAddress = new Uri($"{options.AuthUrl.TrimEnd('/')}/");
        });
        services.AddHttpClient<ISpotifyClient, SpotifyClient>((serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SpotifyOptions>>().Value;
            httpClient.BaseAddress = new Uri($"{options.BaseUrl.TrimEnd('/')}/");
        });
        services.AddHttpClient<ISpotifyService, SpotifyService>();

        services.AddHostedService<SpotifySyncJob>();

        return services;
    }
}
