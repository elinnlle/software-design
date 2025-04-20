using Microsoft.Extensions.DependencyInjection;
using MOSZoo.Application.Interfaces;
using MOSZoo.Application.Services;

namespace MOSZoo.Application.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация бизнес‑сервисов.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnimalTransferService,       AnimalTransferService>();
        services.AddScoped<IFeedingOrganizationService,  FeedingOrganizationService>();
        services.AddScoped<IZooStatisticsService,        ZooStatisticsService>();
        return services;
    }
}