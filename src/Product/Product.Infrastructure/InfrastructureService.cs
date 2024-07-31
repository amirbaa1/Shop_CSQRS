using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace Product.Infrastructure;

public static class InfrastructureService
{
//     public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
//     {
//         service.AddDbContext<ProductDbContext>(x =>
//             x.UseNpgsql(configuration["ConnectionStrings:ProductConnectionString"]));
//
//         // service.AddScoped<IProductRepository, ProductRepository>();
//         // service.AddScoped<ICategoryRepository, CategoryRepository>();
//
//         // service.AddMassTransit(x =>
//         // {
//         //     //send
//         //     x.AddRequestClient<ProductStoreEvent>();
//         //     //get
//         //     x.AddConsumer<UpdateProductStatusConsumer>();
//         //
//         //     x.SetKebabCaseEndpointNameFormatter();
//         //
//         //     x.UsingRabbitMq((context, cfg) =>
//         //     {
//         //         var rabbitMqHost = configuration["EventBusSettings:HostAddress"];
//         //         cfg.Host(new Uri(rabbitMqHost), c =>
//         //         {
//         //             c.Username("guest");
//         //             c.Password("guest");
//         //         });
//         //         cfg.ConfigureEndpoints(context);
//         //
//         //         //cfg.ReceiveEndpoint(EventBusConstants.UpdateProductQueue, ep => { });
//         //
//         //         cfg.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });
//         //     });
//         // });
//         
//
//
//         // identityServer
//     //
//     //     service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     // .AddJwtBearer(op =>
//     // {
//     //     //op.Authority = "https://localhost:7015";
//     //     op.Authority = "http://identity.api";
//     //     op.Audience = "webShop_client";
//     //     op.RequireHttpsMetadata = false;
//     //     op.TokenValidationParameters = new TokenValidationParameters
//     //     {
//     //         ValidateIssuer = true,
//     //         ValidIssuer = "webShop_Api",
//     //         ValidateAudience = true,
//     //         ValidateLifetime = true,
//     //         ValidateIssuerSigningKey = true,
//     //         IssuerSigningKey = new SymmetricSecurityKey(
//     //             Encoding.ASCII.GetBytes(configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
//     //         ClockSkew = TimeSpan.Zero,
//     //     };
//     // });
//     //
//     //     //policy
//     //     service.AddAuthorization(op =>
//     //     {
//     //         op.AddPolicy("productManagement", policy =>
//     //             policy.RequireClaim("scope", "productApi.Management"));
//     //     });
//     //
//     //
//     //     return service;
//     // }
}