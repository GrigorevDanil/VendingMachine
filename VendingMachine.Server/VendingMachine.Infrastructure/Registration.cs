using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Application.Services;
using VendingMachine.Infrastructure.DbContexts;
using VendingMachine.Infrastructure.Repositories;
using VendingMachine.Infrastructure.Services;

namespace VendingMachine.Infrastructure;

public static class Registration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddDbContext()
            .AddRepositories()
            .AddRedis(configuration);
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ICacheService, CacheService>();
        
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = typeof(Registration).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IRepository<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddScoped<ICoinRepository, CoinRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString(Constants.REDIS_KEY);
        });
        
        return services;
    }
   
}