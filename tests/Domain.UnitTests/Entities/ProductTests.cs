using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
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
        result.Should().NotBeNull();
        result.ProductType.Should().Be(productType);
        result.Width.Should().Be(width);

    }
}

