using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public virtual Order Order { get; }
    public int Quantity { get; set; }

    public Product Product { get; set; }

    protected OrderItem()
    {

    }

    public OrderItem(int quantity, Product product)
    {
        Quantity=Guard.Against.NegativeOrZero(quantity, nameof(quantity));
        Product=Guard.Against.Null(product, nameof(product)); ;
    }

}
