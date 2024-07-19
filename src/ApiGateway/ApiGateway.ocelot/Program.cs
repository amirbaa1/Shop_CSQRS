using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Polly;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



IWebHostEnvironment webHostEnvironment = builder.Environment;
builder.Configuration.SetBasePath(webHostEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddOcelot(webHostEnvironment)
    .AddEnvironmentVariables();


builder.Services.AddOcelot(builder.Configuration)
    .AddPolly()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });


var authenticationSchemeKey = "ApiGatewayAdminAuthenticationScheme";

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(authenticationSchemeKey, op =>
//    {
//        op.Authority = "http://identity.api";
//        op.Audience = "webShop_client";
//        op.RequireHttpsMetadata = false;
//    });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
            ClockSkew = TimeSpan.Zero,
        };
    });

//policy

builder.Services.AddAuthorization(op =>
{
    op.AddPolicy("productManagement", policy =>
        policy.RequireClaim("scope", "productApi.Management"));

    op.AddPolicy("orderManagement", policy =>
        policy.RequireClaim("scope", "orderApi.Management"));

    op.AddPolicy("orderUser", policy =>
     policy.RequireClaim("scope", "orderApi.User"));

    op.AddPolicy("storeManagement", policy =>
        policy.RequireClaim("scope", "storeApi.Management"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endp => { endp.MapControllers(); });
app.UseOcelot().Wait();

app.Run();
