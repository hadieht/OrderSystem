using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable($"{nameof(Product)}s").HasKey(k => k.Id);

            builder.Property(t => t.ProductType).IsRequired();

            builder.Property(t => t.Width).IsRequired();

            var converter = new ValueConverter<ProductType, string>(
                        v => v.ToString(),
                        v => (ProductType)Enum.Parse(typeof(ProductType), v));

            builder.Property(e => e.ProductType).HasConversion(converter);

        }
    }
}
