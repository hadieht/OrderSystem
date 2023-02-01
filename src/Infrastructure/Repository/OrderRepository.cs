using Application.Repositories;
using Domain.Entities;
using Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class OrderRepository : Repository<Domain.Entities.Order>, IOrderRepository
{
    private readonly ApplicationDbContext dbContext;
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        this.dbContext= context;
    }

    public async Task<Order> GetOrderWithItemAsync(string orderID)
    {
        return await dbContext.Orders
                        .Where(a => a.OrderID== orderID)
                        .Include(a => a.Items)
                        .ThenInclude(a => a.Product)
                        .FirstOrDefaultAsync();
    }
}
