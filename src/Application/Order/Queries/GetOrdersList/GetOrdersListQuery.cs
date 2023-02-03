using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrdersList;

public record GetOrdersListQuery : IRequest<Result<List<GetOrderListResponse>>>;
 