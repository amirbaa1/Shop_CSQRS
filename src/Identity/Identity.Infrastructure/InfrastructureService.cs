using System.Text;
using Identity.Domain.Models;
using Identity.Domain.Repository;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Repository;
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
        service.AddDbContext<AuthDbContext>(
            x => x.UseNpgsql(configuration["ConnectionStrings:AccountConnectionString"]));

        service.AddIdentity<AppUser, IdentityRole>(
                op => { op.SignIn.RequireConfirmedEmail = false; })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        service.AddScoped<IAuthRepository, AuthRepository>();
        service.AddScoped<ITokenGenerator, TokenGenerator>();

        service.Configure<JwtOption>(configuration.GetSection("TokenAuthAPI:JWTOption"));

        // ----------------- JWT -------------------//
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
                ClockSkew = TimeSpan.FromMinutes(5),
            };
        });
        //--------------------------------------//

        return service;
    }
}