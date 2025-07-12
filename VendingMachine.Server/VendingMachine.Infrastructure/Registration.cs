using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Abstractions;
using VendingMachine.Infrastructure.DbContexts;

namespace VendingMachine.Infrastructure;

public static class Registration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext();
        
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddScoped<IReadDbContext, ReadDbContext>();
        
        return services;
    }
    

}