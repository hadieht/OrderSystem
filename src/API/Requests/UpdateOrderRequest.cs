using Application.Common.Models;

namespace API.Requests;

public class UpdateOrderRequest
{
    public string CustomerName { get; set; }

    public string Email { get; set; }

    public Address Address { get; set; }
}
