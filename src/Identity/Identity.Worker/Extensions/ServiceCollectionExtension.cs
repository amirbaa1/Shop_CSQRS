﻿

using Common.Infrastructure.Helpers;
using Identity.Domain.Models;
using Identity.Domain.Repository;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Repository;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(
            x => x.UseNpgsql());

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.Configure<JwtOption>(configuration.GetSection("TokenAuthAPI:JWTOption"));


            //jwt
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(PublicVariables.JwtSecret)),
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = PublicVariables.JwtIssuer,
                    ValidAudience = PublicVariables.JwtAudience,
                    ClockSkew = TimeSpan.Zero,
                };
            });



            //identityServer 
            services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryClients(new List<Client>
        {
        new Client
        {
            ClientName = "Web API",
            ClientId = "WebAPI",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "productApi.Management","orderApi.Management","orderApi.User","storeApi.Management" },
        },
        })
        .AddInMemoryApiResources(new List<ApiResource>()
        {
        new ApiResource("orderapi.management", "order service api")
        {
            Scopes = { "orderApi.Management" },
        },
        new ApiResource("orderapi.user","order service api")
        {
            Scopes = {"orderApi.User"}
        },
        new ApiResource("productapi", "Product management Api")
        {
            Scopes = { "productApi.Management" }
        },
        new ApiResource("storeapi","store management")
        {
            Scopes= { "storeApi.Management" }
        }
        })
        .AddInMemoryApiScopes(new List<ApiScope>
        {
        new ApiScope("orderApi.Management"),
        new ApiScope("orderApi.User"),
        new ApiScope("productApi.Management")
        })
        .AddInMemoryIdentityResources(new List<IdentityResource>
        {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        }).AddAspNetIdentity<AppUser>();

        }
    }
}
