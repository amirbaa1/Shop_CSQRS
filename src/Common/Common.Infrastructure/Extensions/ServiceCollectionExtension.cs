using System.Reflection;
using Common.Infrastructure.Helpers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterVariables(this IServiceCollection services, IConfiguration configuration)
    {
        PublicVariables.RabbitmqHost = Environment.GetEnvironmentVariable("RabbitmqHost");
        PublicVariables.RabbitmqUserName = Environment.GetEnvironmentVariable("RabbitmqUsername");
        PublicVariables.RabbitmqPassword = Environment.GetEnvironmentVariable("RabbitmqPassword");
        PublicVariables.ProductConnectionString = Environment.GetEnvironmentVariable("ProductConnectionString");
        PublicVariables.JwtSecret = Environment.GetEnvironmentVariable("Secret");
        PublicVariables.JwtIssuer = Environment.GetEnvironmentVariable("Issuer");
        PublicVariables.JwtAudience = Environment.GetEnvironmentVariable("Audience");

        if (string.IsNullOrEmpty(PublicVariables.RabbitmqHost) &&
            !string.IsNullOrEmpty(configuration["RabbitmqHost"]))
            PublicVariables.RabbitmqHost = configuration["RabbitmqHost"];
        if (string.IsNullOrEmpty(PublicVariables.RabbitmqUserName) &&
            !string.IsNullOrEmpty(configuration["RabbitmqUserName"]))
            PublicVariables.RabbitmqUserName = configuration["RabbitmqUserName"];
        if (string.IsNullOrEmpty(PublicVariables.RabbitmqPassword) &&
            !string.IsNullOrEmpty(configuration["RabbitmqPassword"]))
            PublicVariables.RabbitmqPassword = configuration["RabbitmqPassword"];
        if (string.IsNullOrEmpty(PublicVariables.ProductConnectionString) &&
            !string.IsNullOrEmpty(configuration["ProductConnectionString"]))
            PublicVariables.ProductConnectionString = configuration["ProductConnectionString"];
        if (string.IsNullOrEmpty(PublicVariables.JwtSecret) &&
            !string.IsNullOrEmpty(configuration["Secret"]))
            PublicVariables.JwtSecret = configuration["Secret"];
        if (string.IsNullOrEmpty(PublicVariables.JwtIssuer) &&
            !string.IsNullOrEmpty(configuration["Issuer"]))
            PublicVariables.JwtIssuer = configuration["Issuer"];
        if (string.IsNullOrEmpty(PublicVariables.JwtAudience) &&
            !string.IsNullOrEmpty(configuration["Audience"]))
            PublicVariables.JwtIssuer = configuration["Audience"];
    }
    

    public static void RegisterMassTransit(this IServiceCollection service, Assembly assembly)
    {
        service.AddMassTransit(x =>
        {
            //send
            // x.AddRequestClient<ProductStoreEvent>();
            //get
            x.AddConsumers(assembly);

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(PublicVariables.RabbitmqHost, c =>
                {
                    c.Username(PublicVariables.RabbitmqUserName);
                    c.Password(PublicVariables.RabbitmqPassword);
                });
                cfg.ConfigureEndpoints(context);

                cfg.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });
            });
        });
    }

    public static void RegisterMediatR(this IServiceCollection service, Assembly assembly)
    {
        service.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(assembly));
    }

    public static void RegisterAutoMapper(this IServiceCollection service, Assembly assembly)
    {
        service.AddAutoMapper(assembly);
    }

    public static void RegisterDatabase(this IServiceCollection service)
    {
    }
}