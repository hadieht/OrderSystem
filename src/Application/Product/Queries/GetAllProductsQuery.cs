using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Product.Queries;

public record GetAllProductsQuery : IRequest<Result<List<GetProductsResponse>>>;
