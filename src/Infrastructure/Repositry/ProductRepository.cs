using Application.Repositories;
using Infrastructure.Repositry.Common;

namespace Infrastructure.Repositry;

public class ProductRepository : Repository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
