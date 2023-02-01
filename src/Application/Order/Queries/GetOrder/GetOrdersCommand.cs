using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Order.Queries.GetOrder;

public record GetOrdersCommand : IRequest<Result<GetOrderResponse>>
{
    public string OrderNumber { get; set; }
}