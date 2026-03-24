using Microsoft.Extensions.DependencyInjection;
using MediatR;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;

namespace TEST.AMRCloud.Services.Scoping.Application.Registration;

/// <summary>
/// Dependency Injection registration for Application layer services.
/// Register all MediatR handlers and application services here.
/// </summary>
public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly));

        // Register repositories
        services.AddScoped<IEngagementRepository, EngagementRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFundRepository, FundRepository>();

        return services;
    }
}
