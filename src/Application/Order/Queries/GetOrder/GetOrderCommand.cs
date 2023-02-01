using Application.Common.Interfaces;
using Application.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrder;

public record GetOrdersCommand : IRequest<Result<GetOrderResponse>>
{
    public string OrderNumber { get; set; }
}

public class GetOrdersCommandHandler : IRequestHandler<GetOrdersCommand, Result<GetOrderResponse>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IWidthCalculator widthCalculator;
    private readonly IMapper mapper;

    public GetOrdersCommandHandler(IOrderRepository orderRepository,
            IWidthCalculator widthCalculator,
            IMapper mapper)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
        this.widthCalculator=Guard.Against.Null(widthCalculator, nameof(IWidthCalculator));
        this.mapper = Guard.Against.Null(mapper, nameof(IMapper)); ;
    }
    public async Task<Result<GetOrderResponse>> Handle(GetOrdersCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderWithItemAsync(request.OrderNumber);

        var result = mapper.Map<GetOrderResponse>(order);

        result.BinWidth = widthCalculator.BinWidthDisplay(order.Items);

        return Result.Success(result);
    }
}
