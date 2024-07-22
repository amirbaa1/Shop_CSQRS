using EventBus.Messages.Event.Product;
using EventBus.Messages.Event.Store;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Repository;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repository;
using System.Text;


namespace Store.Infrastructure
{
    public static class InfrastructureService
    {
        //public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        //    IConfiguration configuration)
        //{
        //    services.AddDbContext<StoreDbContext>(x =>
        //        x.UseNpgsql(configuration["ConnectionStrings:StoreConnectionString"]));

        //    services.AddScoped<IStoreRepository, StoreRepository>();


        //    services.AddMassTransit(x =>
        //    {
        //        //send
        //        x.AddRequestClient<UpdateProductStatusEvent>();
        //        x.AddRequestClient<MessageCheckStoreEvent>();
        //        //get
        //        x.AddConsumer<AddProductStoreConsumer>();
        //        x.AddConsumer<UpdateProductStoreConsumer>();
        //        x.AddConsumer<BasketStoreConsumer>();
        //        x.AddConsumer<CheckStoreConsumer>();

        //        x.SetKebabCaseEndpointNameFormatter();
        //        x.UsingRabbitMq((context, config) =>
        //        {
        //            //local
        //            //cfg.Host("localhost", "/", c =>
        //            //docker
        //            var rabbitMqHost = configuration["EventBusSettings:HostAddress"];
        //            config.Host(new Uri(rabbitMqHost), c =>
        //            {
        //                c.Username("guest");
        //                c.Password("guest");
        //            });

        //            config.ConfigureEndpoints(context);
        //            config.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });
        //            //
        //            // config.ReceiveEndpoint(EventBusConstants.AddProductStore,
        //            //     ep => { ep.ConfigureConsumer<AddProductStoreConsumer>(context); });
        //        });
        //    });

        //    //identity
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //        .AddJwtBearer(op =>
        //        {
        //            //op.Authority = "https://localhost:7015";
        //            op.Authority = "http://identity.api";
        //            op.Audience = "webShop_client";
        //            op.RequireHttpsMetadata = false;
        //            op.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidIssuer = "webShop_Api",
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(
        //                    Encoding.ASCII.GetBytes(configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
        //                ClockSkew = TimeSpan.Zero,
        //            };
        //        });

        //    //policy
        //    services.AddAuthorization(op =>
        //    {
        //        op.AddPolicy("storeManagement", policy =>
        //            policy.RequireClaim("scope", "storeApi.Management"));
        //    });


        //    return services;
        //}
    }
}