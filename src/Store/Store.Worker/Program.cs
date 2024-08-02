using Microsoft.Extensions.Hosting;
using System.Reflection;
using Common.Infrastructure.Extensions;
using Store.Worker.Extensions;



var builder = Host.CreateApplicationBuilder(args);

DotNetEnv.Env.Load();

builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterStoreService(builder.Configuration);

var host = builder.Build();
host.Run();
