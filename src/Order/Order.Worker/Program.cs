using Microsoft.Extensions.Hosting;
using Common.Infrastructure.Extensions;
using System.Reflection;
using Order.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);

DotNetEnv.Env.Load();

builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterOrderService(builder.Configuration);

var host = builder.Build();
host.Run();
