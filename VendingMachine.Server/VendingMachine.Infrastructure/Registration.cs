using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Abstractions;
using VendingMachine.Infrastructure.DbContexts;
using VendingMachine.Infrastructure.Services;

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

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IExcelProductImportService, ExcelProductImportService>();
        
        return services;
    }
}