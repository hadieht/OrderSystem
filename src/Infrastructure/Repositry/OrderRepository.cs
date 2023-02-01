using Application.Repositories;
using Domain.Entities;
using Infrastructure.Repositry.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositry;

public class OrderRepository : Repository<Domain.Entities.Order>, IOrderRepository
{
    private readonly ApplicationDbContext dbContext;
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        this.dbContext= context;
    }

    public async Task<Order> GetOrderWithItemAsync(string orderNumber)
    {
        return await dbContext.Orders
                        .Where(a => a.OrderNumber== orderNumber)
                        .Include(a => a.Items)
                        .ThenInclude(a => a.Product)
                        .FirstOrDefaultAsync();
    }
}
