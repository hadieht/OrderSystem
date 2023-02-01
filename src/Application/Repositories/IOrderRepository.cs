using Application.Common.Interfaces;

namespace Application.Repositories;

public interface IOrderRepository : IRepository<Domain.Entities.Order>
{
    Task<Domain.Entities.Order> GetOrderWithItemAsync(string orderNumber);

}
