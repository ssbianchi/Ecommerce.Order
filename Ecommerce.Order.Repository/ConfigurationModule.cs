using Ecommerce.Order.Domain.Entity.Order.Repository;
using Ecommerce.Order.Domain.Entity.Readonly.Repository;
using Ecommerce.Order.Repository.Context;
using Ecommerce.Order.Repository.Repository;
using Ecommerce.Order.Repository.Repository.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order.Repository
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EcommerceContext>(c =>
            {
                connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Ecommerce_Order;Trusted_Connection=True;";
                c.UseSqlServer(connectionString);
            });

            //Use for Dapper
            services.Configure<ConnectionStringOptions>(c =>
            {
                c.ConnectionString = connectionString;
            });

            services.AddScoped<IReadonlyRepository, ReadonlyRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IOrderSessionRepository, OrderSessionRepository>();

        }

    }
}
