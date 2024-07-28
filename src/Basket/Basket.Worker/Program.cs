using Microsoft.Extensions.Hosting;
using System.Reflection;
using Common.Infrastructure.Extensions;
using Basket.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterBasket();



var host = builder.Build();
host.Run();
