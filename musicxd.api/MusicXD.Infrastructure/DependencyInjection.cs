using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicXD.Application.Interfaces;
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

        services.AddHttpClient<ISpotifyService, SpotifyService>();

        services.AddHostedService<SpotifySyncJob>();

        return services;
    }
}
