using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace OrderSystem.Domain.UnitTests.Entities;

public class ProductTests
{

    [Test]
    public void Product_CreateNewProduct_PerformCorrectly()
    {
        //Arrange
        var productType = ProductType.Canvas;
        var width = 1;

        //Act
        var result = Product.Create(productType, width);

        //Assert

        Assert.NotNull(result);

        Assert.AreEqual(result.ProductType, productType);
        Assert.AreEqual(result.Width, width);

    }
}

