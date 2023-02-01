using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrdersList;

public record GetOrdersListCommand : IRequest<Result<List<GetOrderListResponse>>>
{
}