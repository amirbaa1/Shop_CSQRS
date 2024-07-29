using System.Reflection;
using Common.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;
using Order.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterOrderService(builder.Configuration);
var app = builder.Build();

app.Run();