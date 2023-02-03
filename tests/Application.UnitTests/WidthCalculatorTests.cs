using Application.Calculator;
using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace OrderSystem.Application.UnitTests;

public class WidthCalculatorTests
{

    [Test] //1 photo book (0), 2 calendars (|) and 2 mug (.)  [0||:]
    public void Calculator_WithFewItem()
    {
        //Arrange

        var listOrderItems = new List<OrderItem>()
        {
           new OrderItem(1 , Product.Create(ProductType.PhotoBook, 1)),
           new OrderItem(2 , Product.Create(ProductType.Calendar, 1)),
           new OrderItem(2 , Product.Create(ProductType.Mug, 1)),
        };


        //Act
        var calculator = new WidthCalculator();

        var result = calculator.BinWidthCalculator(listOrderItems);

        //Assert

        Assert.AreEqual(result, 4);
    }


    [Test] //1 photo book (0), 2 calendars (|) and 5 mug (.)  [0||:.]
    public void Calculator_WithMoreMugs()
    {
        //Arrange

        var listOrderItems = new List<OrderItem>()
        {
           new OrderItem(1 , Product.Create(ProductType.PhotoBook, 1)),
           new OrderItem(2 , Product.Create(ProductType.Calendar, 1)),
           new OrderItem(5 , Product.Create(ProductType.Mug, 1)),
        };


        //Act
        var calculator = new WidthCalculator();

        var result = calculator.BinWidthCalculator(listOrderItems);

        //Assert

        Assert.AreEqual(result, 5);
    }
}
