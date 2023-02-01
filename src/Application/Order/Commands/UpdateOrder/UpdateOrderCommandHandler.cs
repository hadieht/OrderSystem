using Application.Common.Exceptions;
using Application.Repositories;
using Ardalis.GuardClauses;
using Domain.ValueObjects;
using MediatR;
using Address = Application.Common.Models.Address;
using NotFoundException = Application.Common.Exceptions.NotFoundException;

namespace Application.Order.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository orderRepository;
    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
    }
    public async Task<bool> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {

        var emailResult = Email.Create(command.Email);

        if (emailResult.IsFailure)
        {
            throw new ValidationException(emailResult.Error, nameof(Email));
        }

        var addressResult = Domain.ValueObjects.Address.Create(command.Address.PostalCode,
            command.Address.HouseNumber,
            command.Address.AddressExtra);

        if (addressResult.IsFailure)
        {
            throw new ValidationException(addressResult.Error, nameof(Address));
        }

        var order = await orderRepository.GetOrderWithItemAsync(command.OrderNumber);

        if (order== null)
        {
            throw new NotFoundException("Order not found!");
        }

        order.EditOrder(command.CustomerName, emailResult.Value, addressResult.Value);

        await orderRepository.UpdateAsync(order);

        return true;
    }

}