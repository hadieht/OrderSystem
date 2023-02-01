using Application.Common.Models;
using MediatR;
using Address = Application.Common.Models.Address;

namespace Application.Order.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<CreateOrderResponse>
{
    public string CustomerName { get; set; }

    public string Email { get; set; }

    public Address Address { get; set; }

    public List<OrderItem> Items { get; set; }
}