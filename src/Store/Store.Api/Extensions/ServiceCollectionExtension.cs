using Common.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Repository;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repository;
using System.Text;
using Common.Infrastructure.Service;

namespace Store.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterStoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(x =>
                x.UseNpgsql(configuration["ConnectionStrings:StoreConnectionString"]));


            //identity
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            services.AddAuthorization(op =>
            {
                op.AddPolicy("storeManagement", policy =>
                    policy.RequireClaim("scope", "storeApi.Management"));
            });


            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<StoreService>();
        }
    }
}
