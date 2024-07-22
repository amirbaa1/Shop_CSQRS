using System.Reflection;
using Common.Infrastructure.Extensions;
using Product.Api.Extensions;
using Product.Infrastructure.Data;
using Product.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//v1
// builder.Services.AddApplication();
// builder.Services.AddInfrastructure(builder.Configuration);

// v2
builder.Services.RegisterVariables(builder.Configuration);
builder.Services.RegisterMassTransit(Assembly.GetExecutingAssembly());
builder.Services.RegisterMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.RegisterProductService(builder.Configuration);


var app = builder.Build();

//app.MigrateDatabase<ProductDbContext>();

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