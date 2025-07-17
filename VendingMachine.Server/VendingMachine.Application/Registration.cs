using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application;

public static class Registration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Registration).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<>), typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<>),typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
        
        
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
    
}

