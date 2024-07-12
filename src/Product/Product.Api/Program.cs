
using MediatR;
using Product.Application;
using Product.Infrastructure;
using Product.Infrastructure.Data;
using Product.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
var app = builder.Build();

//app.MigrateDatabase<ProductDbContext>();

//app.MigrateDatabase<ProductDbContext>(
//    seeder: null,
//    tablesToCheck: new[] { "Categories", "Products" });


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();