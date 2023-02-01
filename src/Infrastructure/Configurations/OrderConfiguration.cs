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

        builder.Property(t => t.OrderID).IsRequired().HasMaxLength(50);

        builder.Property(t => t.OrderDate).IsRequired();

        builder.Property(t => t.CustomerName).IsRequired().HasMaxLength(200);

        var converter = new ValueConverter<OrderStatus, string>(
                      v => v.ToString(),
                      v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));

        builder.Property(e => e.Status)
            .HasConversion(converter)
            .HasMaxLength(50);


        builder.Property(p => p.CustomerEmail)
            .HasConversion(p => p.Value, p => Email.Create(p).Value)
            .HasMaxLength(200);

        builder.OwnsOne(p => p.Address, p =>
        {
            p.Property(pp => pp.PostalCode).HasColumnName("PostalCode")
                .HasMaxLength(8);
            p.Property(pp => pp.HouseNumber).HasColumnName("HouseNumber");
            p.Property(pp => pp.Extra).HasColumnName("Extra")
                .HasMaxLength(10);
        });

        builder.HasMany(p => p.Items).WithOne(p => p.Order)
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent?.SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
