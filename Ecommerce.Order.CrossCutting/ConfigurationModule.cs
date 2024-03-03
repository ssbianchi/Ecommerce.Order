using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order.CrossCutting
{
    public static class ConfigurationModule
    {
        public static void RegisterCrossCutting(this IServiceCollection service, IConfiguration configuration)
        {
            //service.Configure<ConsultaCEPWebAPIOptions>(c =>
            //{
            //    c.BaseUrl = configuration["ConsultaCEPWebAPI:BaseUrl"];
            //});

            //service.AddScoped<IConsultaCEPWebAPI, ConsultaCEPWebAPI>();
        }
    }
}
