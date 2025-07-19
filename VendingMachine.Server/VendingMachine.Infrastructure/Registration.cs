using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Application.Services;
using VendingMachine.Infrastructure.DbContexts;
using VendingMachine.Infrastructure.Repositories;

namespace VendingMachine.Infrastructure;

public static class Registration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase()
            .AddDbContext()
            .AddRepositories()
            .AddServices();
        
        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
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
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IExcelProductImportService, ExcelProductImportService>();
        
        return services;
    }
}