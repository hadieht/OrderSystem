namespace Application.Common.Interfaces;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
  where TEntity : Domain.Common.BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IDatabaseTransaction> BeginTransactionAsync();
}
