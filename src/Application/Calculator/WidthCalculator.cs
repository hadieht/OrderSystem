
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Calculator
{
    public class WidthCalculator : IWidthCalculator
    {
        private static string Measurement = "mm";
        public double BinWidthCalculator(IEnumerable<OrderItem> orderItems)
        {
            double width = 0;

            foreach (OrderItem item in orderItems)
            {
                if (item.Product.ProductType == Domain.Enums.ProductType.Mug)
                {
                    width += (Math.Ceiling((item.Quantity/4D)) * item.Product.Width);
                }
                else
                {
                    width +=  (item.Quantity * item.Product.Width);
                }

            }

            return width;
        }

        public string BinWidthDisplay(IEnumerable<OrderItem> orderItems)
        {
            var width = BinWidthCalculator(orderItems);
            return $"{width}{Measurement}";
        }

    }
}
