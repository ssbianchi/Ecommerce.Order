using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.Repository.Context
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ILoggerFactory logger = LoggerFactory.Create(c => c.AddConsole());
            // optionsBuilder.UseLoggerFactory(logger);

            base.OnConfiguring(optionsBuilder);
        }

    }
}
