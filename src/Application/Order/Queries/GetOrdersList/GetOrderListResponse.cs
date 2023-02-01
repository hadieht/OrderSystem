using Domain.Enums;

namespace Application.Order.Queries.GetOrdersList;

public class GetOrderListResponse
{
    public string OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public string CustomerName { get; set; }

    public string CustomerEmail { get; set; }

    public string PostalCode { get; set; }

    public int HouseNumber { get; set; }

    public string AddressExtra { get; set; }

    public OrderStatus Status { get; set; }

}
