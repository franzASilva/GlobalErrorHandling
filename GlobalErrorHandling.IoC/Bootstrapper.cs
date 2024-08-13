using GlobalErrorHandling.Domain.Services;
using GlobalErrorHandling.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalErrorHandling.IoC;

public static class Bootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDummyService, DummyService>();

        return services;
    }
}