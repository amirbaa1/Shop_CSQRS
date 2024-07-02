using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Basket.Application.Features.Basket.Commands.CheckOut;
using EventBus.Messages.Common;
using MassTransit;

namespace Basket.Application
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            

            return service;
        }
    }
}