using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositry.Common;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Domain.Common.BaseEntity
{
    private readonly ApplicationDbContext context;
    private readonly DbSet<TEntity> entities;

    public Repository(ApplicationDbContext context)
    {
        this.context = context;
        entities = context.Set<TEntity>();
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return Task.FromResult(entities.AsEnumerable());
    }

    public virtual async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await entities.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));

        entities.Attach(entity);

        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));

        entities.Attach(entity);
        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));

        entities.Remove(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task<IDatabaseTransaction> BeginTransactionAsync()
    {
        var transaction = await context.Database.BeginTransactionAsync();

        return new DatabaseTransaction(transaction);
    }

    protected Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);

    public Task ResetCache()
    {
        return null;
    }
}