using Application.Repositories;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class ProductRepository : Repository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
