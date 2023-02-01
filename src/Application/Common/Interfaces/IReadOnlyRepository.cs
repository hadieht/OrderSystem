using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IReadOnlyRepository<TEntity> where TEntity :  BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default);

    }
}
