using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Repositories;
using Ardalis.GuardClauses;
using Domain.ValueObjects;
using MediatR;
using Address = Application.Common.Models.Address;

namespace Application.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IOrderRepository orderRepository;
    private readonly IReadOnlyProductRepository productRepository;
    private readonly IWidthCalculator widthCalculator;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
        IReadOnlyProductRepository productRepository,
        IWidthCalculator widthCalculator)
    {
        this.orderRepository= Guard.Against.Null(orderRepository, nameof(IOrderRepository));
        this.productRepository=Guard.Against.Null(productRepository, nameof(IReadOnlyProductRepository));
        this.widthCalculator=Guard.Against.Null(widthCalculator, nameof(IWidthCalculator));
    }
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
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

        var order = new Domain.Entities.Order(GetOrderNumber(),
            DateTime.UtcNow,
            command.CustomerName,
            emailResult.Value,
            addressResult.Value
        );


        AddOrderItems(command, order);

        var orderInserted = await orderRepository.AddAsync(order, cancellationToken);

        var result = new CreateOrderResponse
        {
            OrderNumber = orderInserted.OrderNumber,
            BinWidth = widthCalculator.BinWidthDisplay(order.Items)
        };

        return result;
    }

    private async void AddOrderItems(CreateOrderCommand request, Domain.Entities.Order order)
    {
        var products = await productRepository.GetAllAsync();

        foreach (var item in request.Items)
        {
            var product = products.FirstOrDefault(a => a.ProductType== item.ProductType);

            if (product == null)
            {
                throw new ValidationException("Defined product not found", nameof(Product));
            }

            var orderItem = new Domain.Entities.OrderItem(item.Quantity, product);

            order.AddOrderItem(orderItem);
        }
    }

    private string GetOrderNumber()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}