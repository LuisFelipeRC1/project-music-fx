using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicXD.Application.Common.Interfaces;
using MusicXD.Domain.Interfaces;
using MusicXD.Infrastructure.Data;
using MusicXD.Infrastructure.Repositories;
using MusicXD.Infrastructure.Services;
using StackExchange.Redis;

namespace MusicXD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        var redisConnection = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrEmpty(redisConnection))
        {
            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(redisConnection));
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IActivityFeedRepository, ActivityFeedRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}
