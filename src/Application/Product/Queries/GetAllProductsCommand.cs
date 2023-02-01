using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Product.Queries;

public record GetAllProductsCommand : IRequest<Result<List<GetProductsResponse>>>
{
}