using Application.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repository
{
    public class CachedProductRepository : IReadOnlyProductRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IDistributedCache cache;
        private const string CacheKey = "Products";
        private readonly DistributedCacheEntryOptions cacheEntryOptions;
        public CachedProductRepository(IProductRepository productRepository,
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
            if (cache.TryGetValue(CacheKey, out List<Product> result))
            {
                return result;
            }

            result = (await productRepository.GetAllAsync()).ToList();

            await cache.SetAsync(CacheKey, result, cacheEntryOptions);

            return result;
        }

        public async Task<Product> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var key = $"{CacheKey}-{id}";

            if (cache.TryGetValue(CacheKey, out Product product))
            {
                return product;
            }

            var result = await productRepository.GetAsync(id, cancellationToken);

            await cache.SetAsync(key, result, cacheEntryOptions);

            return result;

        }

        public async Task ResetCache()
        {
            await cache.RefreshAsync(CacheKey);
        }

    }
}
