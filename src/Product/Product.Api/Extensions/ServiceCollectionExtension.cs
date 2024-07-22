using System.Text;
using Common.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;

namespace Product.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterProductService(this IServiceCollection service, IConfiguration configuration)
    {
        //dbContext
        service.AddDbContext<ProductDbContext>(x => x.UseNpgsql(configuration["ConnectionStrings:ProductConnectionString"]));

        //using (var db = new ProductDbContext())
        //{
        //    db.Database.Migrate();
        //}

        // identityServer

        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    {
        //op.Authority = "https://localhost:7015";
        op.Authority = "http://identity.api";
        op.Audience = PublicVariables.JwtAudience;
        op.RequireHttpsMetadata = false;
        op.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = PublicVariables.JwtIssuer,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(PublicVariables.JwtSecret)),
            ClockSkew = TimeSpan.Zero,
        };
    });

        //policy
        service.AddAuthorization(op =>
        {
            op.AddPolicy("productManagement", policy =>
                policy.RequireClaim("scope", "productApi.Management"));
        });


        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}