using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Events;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public ProductType ProductType { get; private set; }

        public double Width { get; private set; }

        protected Product()
        {

        }

        public static Product Create(ProductType productType, double width)
        {
            Guard.Against.Null(productType);
            Guard.Against.NegativeOrZero(width);

            return new Product(productType, width);
        }

        private Product(ProductType productType, double width)
        {
            this.ProductType=productType;
            this.Width= width;
            AddDomainEvent(new ProductChangeEvent());
        }

    }
}
