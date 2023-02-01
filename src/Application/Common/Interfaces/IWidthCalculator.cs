using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IWidthCalculator
    {
        double BinWidthCalculator(IEnumerable<OrderItem> orderItems);
        string BinWidthDisplay(IEnumerable<OrderItem> orderItems);
    }
}
