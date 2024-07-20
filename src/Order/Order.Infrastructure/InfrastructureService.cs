using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Model.Email;
using Order.Domain.Repository;
using Order.Application.Consumer;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Order.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<OrderdbContext>(p =>
            p.UseNpgsql(configuration["ConnectionStrings:OrderConnectionString"]));

        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IEmailSend, EmailSend>();

        service.Configure<EmailConfig>(configuration.GetSection("stmp"));


        service.AddMassTransit(x =>
        {
            x.AddConsumer<BasketQueueEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqHost = configuration["EventBusSettings:HostAddress"];
                cfg.Host(new Uri(rabbitMqHost), c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });

                cfg.ReceiveEndpoint(EventBusConstants.BasketQueue,
                    e => { e.ConfigureConsumer<BasketQueueEventConsumer>(context); });
            });
        });

        //identityServer 


        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(op =>
            {
                //op.Authority = "https://localhost:7015";
                op.Authority = "http://identity.api";
                op.Audience = "webShop_client";
                op.RequireHttpsMetadata = false;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "webShop_Api",
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        //policy
        service.AddAuthorization(op =>
        {
            op.AddPolicy("orderManagement", policy =>
                policy.RequireClaim("scope", "orderApi.Management"));

            op.AddPolicy("orderUser", policy =>
                policy.RequireClaim("scope", "orderApi.User"));
        });


        service.AddMassTransitHostedService();

        return service;
    }
}