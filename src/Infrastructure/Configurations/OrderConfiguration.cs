using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable($"{nameof(Order)}s").HasKey(k => k.Id);

        builder.Property(t => t.OrderNumber).IsRequired();

        builder.Property(t => t.OrderDate).IsRequired();

        builder.Property(t => t.CustomerName).IsRequired();

        var converter = new ValueConverter<OrderStatus, string>(
                      v => v.ToString(),
                      v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));

        builder.Property(e => e.Status).HasConversion(converter);


        builder.Property(p => p.CustomerEmail)
            .HasConversion(p => p.Value, p => Email.Create(p).Value);

        builder.OwnsOne(p => p.Address, p =>
        {
            p.Property(pp => pp.PostalCode).HasColumnName("PostalCode");
            p.Property(pp => pp.HouseNumber).HasColumnName("HouseNumber");
            p.Property(pp => pp.Extra).HasColumnName("Extra");
        });

        builder.HasMany(p => p.Items).WithOne(p => p.Order)
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent?.SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
