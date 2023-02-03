using Application.Common.Models;

namespace API.Requests;

public class CreateOrderRequest
{
    public string CustomerName { get; set; }

    public string Email { get; set; }

    public Address Address { get; set; }

    public List<OrderItem> Items { get; set; }
}

