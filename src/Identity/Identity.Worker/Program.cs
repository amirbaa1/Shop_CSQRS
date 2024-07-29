

using Microsoft.Extensions.Hosting;
using Common.Infrastructure.Extensions;
using Identity.Worker.Extensions;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterIdentityService(builder.Configuration);

var app = builder.Build();
app.Run();
