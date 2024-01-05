using GymManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplcation(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionsService, SubscriptionsService>();
        return services;
    }
}