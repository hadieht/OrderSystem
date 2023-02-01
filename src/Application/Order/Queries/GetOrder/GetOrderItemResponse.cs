using Domain.Enums;

namespace Application.Order.Queries.GetOrder;

public class GetOrderItemResponse
{
    public ProductType Product { get; set; }
    public int Quantity { get; set; }
}