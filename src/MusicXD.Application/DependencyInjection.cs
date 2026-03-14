using Microsoft.Extensions.DependencyInjection;
using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.Services;

namespace MusicXD.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<IActivityFeedService, ActivityFeedService>();
        return services;
    }
}
