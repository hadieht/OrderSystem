using Application.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrdersList;

public class GetAllOrdersQueryHandler : IRequestHandler<GetOrdersListQuery, Result<List<GetOrderListResponse>>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetAllOrdersQueryHandler(IOrderRepository orderRepository,
        IMapper mapper)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
        this.mapper = Guard.Against.Null(mapper, nameof(IMapper));
    }
    public async Task<Result<List<GetOrderListResponse>>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var allProducts = await orderRepository.GetAllAsync();

        var result = mapper.Map<List<GetOrderListResponse>>(allProducts);

        return Result.Success(result);
    }
}