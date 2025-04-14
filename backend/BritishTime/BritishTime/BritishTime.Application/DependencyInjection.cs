using BritishTime.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace BritishTime.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.Scan(scan => scan
               .FromAssemblies(Assembly.GetExecutingAssembly())
               .AddClasses(c => c.Where(type => type.Name.EndsWith("Service")))
               .AsImplementedInterfaces()
               .WithScopedLifetime());

        return services;
    }
}
