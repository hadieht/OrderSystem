using Domain.Enums;

namespace Application.Common.Models;

public record OrderItem
{
    public ProductType ProductType { get; set; }
    public int Quantity { get; set; }
}
