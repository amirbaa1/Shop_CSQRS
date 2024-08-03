
using Common.Infrastructure.Extensions;
using System.Reflection;
using Order.Api.Extensions;
using order.Infrastructure.Extensions;
using Order.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterOrderService(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase<OrderdbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();