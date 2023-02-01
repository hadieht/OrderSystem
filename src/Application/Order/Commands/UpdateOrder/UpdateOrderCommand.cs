using MediatR;
using Address = Application.Common.Models.Address;

namespace Application.Order.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest<bool>
{
    public string OrderNumber { get; set; }

    public string CustomerName { get; set; }

    public string Email { get; set; }

    public Address Address { get; set; }

}