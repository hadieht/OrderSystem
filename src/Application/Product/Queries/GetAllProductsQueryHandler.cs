using Application.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Product.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<GetProductsResponse>>>
{
    private readonly IReadOnlyProductRepository productRepository;
    private readonly IMapper mapper;

    public GetAllProductsQueryHandler(IReadOnlyProductRepository productRepository,
        IMapper mapper)
    {
        this.productRepository = Guard.Against.Null(productRepository, nameof(IReadOnlyProductRepository)); ;
        this.mapper = Guard.Against.Null(mapper, nameof(IMapper));
    }
    public async Task<Result<List<GetProductsResponse>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var allProducts = await productRepository.GetAllAsync();

        var result = mapper.Map<List<GetProductsResponse>>(allProducts);

        return Result.Success(result);
    }
}