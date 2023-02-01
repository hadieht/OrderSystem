using Application.Repositories;
using Ardalis.GuardClauses;
using Domain.Events;
using MediatR;

namespace Application.Product.EventHandlers
{
    public class ProductChangeEventHandler : INotificationHandler<ProductChangeEvent>
    {
        private readonly IReadOnlyProductRepository productRepository;

        public ProductChangeEventHandler(IReadOnlyProductRepository productRepository)
        {
            this.productRepository = Guard.Against.Null(productRepository, nameof(IReadOnlyProductRepository)); ;
        }

        public async Task Handle(ProductChangeEvent notification, CancellationToken cancellationToken)
        {
            await productRepository.ResetCache();
        }
    }
}
