using Application.Common.Interfaces;

namespace Application.Repositories;

public interface IReadOnlyProductRepository : IReadOnlyRepository<Domain.Entities.Product>
{
    Task ResetCache();
}
