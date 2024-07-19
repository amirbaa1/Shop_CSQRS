using System.Text;
using Identity.Domain.Models;
using Identity.Domain.Repository;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Repository;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        //data

        service.AddDbContext<AuthDbContext>(
            x => x.UseNpgsql(configuration["ConnectionStrings:IdentityConnectionString"]));

        service.AddIdentity<AppUser, IdentityRole>(
                op =>
                {
                    op.SignIn.RequireConfirmedEmail = false;

                    op.Password.RequireLowercase = true;
                    op.Password.RequireUppercase = true;

                    op.User.RequireUniqueEmail = false;
                    op.SignIn.RequireConfirmedEmail = true;
                })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        service.AddScoped<IAuthRepository, AuthRepository>();
        service.AddScoped<ITokenGenerator, TokenGenerator>();

        service.Configure<JwtOption>(configuration.GetSection("TokenAuthAPI:JWTOption"));


        //jwt
        service.AddAuthentication(op =>
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
                        Encoding.ASCII.GetBytes(configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = configuration["TokenAuthAPI:JWTOption:Issuer"],
                ValidAudience = configuration["TokenAuthAPI:JWTOption:Audience"],
                ClockSkew = TimeSpan.Zero,
            };
        });



        //identityServer 
        service.AddIdentityServer()
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


        return service;
    }
}