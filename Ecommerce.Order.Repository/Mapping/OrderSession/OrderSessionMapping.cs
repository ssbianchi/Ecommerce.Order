using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.Repository.Mapping.OrderSession
{
    public class OrderSessionMapping : IEntityTypeConfiguration<Ecommerce.Order.Domain.Entity.OrderSession.OrderSession>
    {
        public void Configure(EntityTypeBuilder<Ecommerce.Order.Domain.Entity.OrderSession.OrderSession> builder)
        {
            builder.ToTable("OrderSessions");
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).IsRequired().HasColumnName("Id");
            builder.Property(x => x.UserId).IsRequired().HasColumnName("UserId");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("CreatedAt");
        }
    }
}
