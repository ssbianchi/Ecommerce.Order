using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Order.Repository.Mapping.Order
{
    public class OrderMapping : IEntityTypeConfiguration<Ecommerce.Order.Domain.Entity.Order.Order>
    {
        public void Configure(EntityTypeBuilder<Ecommerce.Order.Domain.Entity.Order.Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).IsRequired().HasColumnName("Id");
            builder.Property(x => x.SessionId).IsRequired().HasColumnName("SessionId");
            builder.Property(x => x.ProductId).IsRequired().HasColumnName("ProductId");
            builder.Property(x => x.Qtd).IsRequired().HasColumnName("Qtd");
            builder.Property(x => x.Price).IsRequired().HasColumnName("Price");
        }
    }
}
