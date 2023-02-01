using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositry
{
    public class CachedProductRepositoryDecorator : IReadOnlyProductRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IDistributedCache cache;
        private const string cacheKey = "Products";
        private readonly DistributedCacheEntryOptions cacheEntryOptions;
        public CachedProductRepositoryDecorator(IProductRepository productRepository,
            IDistributedCache cache)
        {
            this.productRepository = productRepository;
            this.cache = cache;
            cacheEntryOptions = new DistributedCacheEntryOptions()
                       .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                       .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            List<Product> result;

            if (cache.TryGetValue(cacheKey, out result))
            {
                return result;
            }
            else
            {
                result = (await productRepository.GetAllAsync()).ToList();

                await cache.SetAsync(cacheKey, result, cacheEntryOptions);
            }

            return result;
        }

        public async Task<Product> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            string key = $"{cacheKey}-{id}";

            Product product;
            if (cache.TryGetValue(cacheKey, out product))
            {
                return product;
            }

            var result = await productRepository.GetAsync(id);

            await cache.SetAsync(key, result, cacheEntryOptions);

            return result;

        }

        public async Task ResetCache()
        {
            await cache.RefreshAsync(cacheKey);
        }

    }
}
