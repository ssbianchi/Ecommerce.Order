using Ecommerce.Order.Application.Order;
using Ecommerce.Order.Application.OrderSession;
using Ecommerce.Order.Application.RabbitRequest;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order.Application
{
    public static class ConfigurationModule
    {
        public static void RegisterApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(ConfigurationModule).Assembly);

            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IOrderSessionService, OrderSessionService>();
            service.AddScoped<IRabbitRequestService, RabbitRequestService>();
        }
    }
}