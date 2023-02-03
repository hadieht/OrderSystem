using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrder;

public record GetOrdersQuery : IRequest<Result<GetOrderResponse>>
{
    public string OrderID { get; set; }
}