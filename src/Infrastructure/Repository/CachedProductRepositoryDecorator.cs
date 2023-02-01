using Application.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repository
{
    public class CachedProductRepositoryDecorator : IReadOnlyProductRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IDistributedCache cache;
        private const string CacheKey = "Products";
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

            if (cache.TryGetValue(CacheKey, out result))
            {
                return result;
            }
            else
            {
                result = (await productRepository.GetAllAsync()).ToList();

                await cache.SetAsync(CacheKey, result, cacheEntryOptions);
            }

            return result;
        }

        public async Task<Product> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            string key = $"{CacheKey}-{id}";

            Product product;
            if (cache.TryGetValue(CacheKey, out product))
            {
                return product;
            }

            var result = await productRepository.GetAsync(id);

            await cache.SetAsync(key, result, cacheEntryOptions);

            return result;

        }

        public async Task ResetCache()
        {
            await cache.RefreshAsync(CacheKey);
        }

    }
}
