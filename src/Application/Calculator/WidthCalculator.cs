
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Calculator
{
    public class WidthCalculator : IWidthCalculator
    {
        private static readonly string Measurement = "mm";
        private static readonly double MaxMugCountInSlide = 4D;
        public double BinWidthCalculator(IEnumerable<OrderItem> orderItems)
        {
            double width = 0;

            foreach (var item in orderItems)
            {
                if (item.Product.ProductType == Domain.Enums.ProductType.Mug)
                {
                    width += (Math.Ceiling((item.Quantity/MaxMugCountInSlide)) * item.Product.Width);
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
