using Microsoft.Extensions.DependencyInjection;
using MOSZoo.Domain.Interfaces;
using MOSZoo.Infrastructure.Repositories;

namespace MOSZoo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация инфраструктурных сервисов.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAnimalRepository,          InMemoryAnimalRepository>();
        services.AddSingleton<IEnclosureRepository,       InMemoryEnclosureRepository>();
        services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();
        return services;
    }
}