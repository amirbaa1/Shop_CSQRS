
using Common.Infrastructure.Helpers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterVariables(this IServiceCollection services, IConfiguration configuration)
    {
        PublicVariables.RabbitmqHost = Environment.GetEnvironmentVariable("RabbitmqHost");
        PublicVariables.RabbitmqUserName = Environment.GetEnvironmentVariable("RabbitmqUsername");
        PublicVariables.RabbitmqPassword = Environment.GetEnvironmentVariable("RabbitmqPassword");
        PublicVariables.ProductConnectionString = Environment.GetEnvironmentVariable("ProductConnectionString");
        PublicVariables.StoreConnectionString = Environment.GetEnvironmentVariable("StoreConnectionString");
        PublicVariables.BasketConnectionString = Environment.GetEnvironmentVariable("BasketConnectionString");
        PublicVariables.OrderConnectionString = Environment.GetEnvironmentVariable("OrderConnectionString");
        PublicVariables.JwtSecret = Environment.GetEnvironmentVariable("Secret");
        PublicVariables.JwtIssuer = Environment.GetEnvironmentVariable("Issuer");
        PublicVariables.JwtAudience = Environment.GetEnvironmentVariable("Audience");

        if (string.IsNullOrEmpty(PublicVariables.RabbitmqHost) &&
            !string.IsNullOrEmpty(configuration["EventBusSettings:RabbitmqHost"]))
            PublicVariables.RabbitmqHost = configuration["EventBusSettings:RabbitmqHost"];
        
        if (string.IsNullOrEmpty(PublicVariables.RabbitmqUserName) &&
            !string.IsNullOrEmpty(configuration["EventBusSettings:RabbitmqUserName"]))
            PublicVariables.RabbitmqUserName = configuration["EventBusSettings:RabbitmqUserName"];
        
        if (string.IsNullOrEmpty(PublicVariables.RabbitmqPassword) &&
            !string.IsNullOrEmpty(configuration["EventBusSettings:RabbitmqPassword"]))
            PublicVariables.RabbitmqPassword = configuration["EventBusSettings:RabbitmqPassword"];

        if (string.IsNullOrEmpty(PublicVariables.ProductConnectionString) &&
            !string.IsNullOrEmpty(configuration["ConnectionStrings:ProductConnectionString"]))
            PublicVariables.ProductConnectionString = configuration["ConnectionStrings:ProductConnectionString"];

        if (string.IsNullOrEmpty(PublicVariables.BasketConnectionString) &&
          !string.IsNullOrEmpty(configuration["ConnectionStrings:BasketConnectionString"]))
            PublicVariables.BasketConnectionString = configuration["ConnectionStrings:BasketConnectionString"];

        if (string.IsNullOrEmpty(PublicVariables.StoreConnectionString) &&
         !string.IsNullOrEmpty(configuration["ConnectionStrings:StoreConnectionString"]))
            PublicVariables.StoreConnectionString = configuration["ConnectionStrings:StoreConnectionString"];

        if (string.IsNullOrEmpty(PublicVariables.OrderConnectionString) &&
        !string.IsNullOrEmpty(configuration["ConnectionStrings:OrderConnectionString"]))
            PublicVariables.OrderConnectionString = configuration["ConnectionStrings:OrderConnectionString"];

        if (string.IsNullOrEmpty(PublicVariables.JwtSecret) &&
            !string.IsNullOrEmpty(configuration["JWTOption:Secret"]))
            PublicVariables.JwtSecret = configuration["JWTOption:Secret"];
        if (string.IsNullOrEmpty(PublicVariables.JwtIssuer) &&
            !string.IsNullOrEmpty(configuration["JWTOption:Issuer"]))
            PublicVariables.JwtIssuer = configuration["JWTOption:Issuer"];
        if (string.IsNullOrEmpty(PublicVariables.JwtAudience) &&
            !string.IsNullOrEmpty(configuration["JWTOption:Audience"]))
            PublicVariables.JwtAudience = configuration["JWTOption:Audience"];
    }


    public static void RegisterMassTransit(this IServiceCollection service, Assembly assembly)
    {
        service.AddMassTransit(x =>
        {
            //send
            //x.AddRequestClient(assembly);
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
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        // service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //service.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void RegisterAutoMapper(this IServiceCollection service, Assembly assembly)
    {
        service.AddAutoMapper(assembly);
    }

    public static void RegisterDatabase(this IServiceCollection service, IConfiguration configuration)
    {
        // service.AddDbContext<StoreDbContext>(x =>
        //     x.UseNpgsql());
    }
}