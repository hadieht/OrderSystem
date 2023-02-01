using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable($"{nameof(OrderItem)}s").HasKey(k => k.Id);

            builder.Property(t => t.Quantity).IsRequired();

            builder.HasOne(e => e.Order)
                .WithMany(c => c.Items);

            builder.HasOne(a => a.Product);
        }
    }
}
