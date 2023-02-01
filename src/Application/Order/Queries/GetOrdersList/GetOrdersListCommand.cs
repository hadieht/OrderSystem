using Application.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrdersList;

public record GetOrdersListCommand : IRequest<Result<List<GetOrderListResponse>>>
{
}

public class GetAllOrdersCommandHandler : IRequestHandler<GetOrdersListCommand, Result<List<GetOrderListResponse>>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetAllOrdersCommandHandler(IOrderRepository orderRepository,
            IMapper mapper)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
        this.mapper = Guard.Against.Null(mapper, nameof(IMapper));
    }
    public async Task<Result<List<GetOrderListResponse>>> Handle(GetOrdersListCommand request, CancellationToken cancellationToken)
    {
        var allProducts = await orderRepository.GetAllAsync();

        var result = mapper.Map<List<GetOrderListResponse>>(allProducts);

        return Result.Success(result);
    }
}