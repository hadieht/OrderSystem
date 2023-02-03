using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace OrderSystem.Domain.UnitTests.Entities;

public class OrderTests
{

    [Test]
    [TestCase("orderID")]
    [TestCase("customerName")]
    [TestCase("customerEmail")]
    [TestCase("address")]
    public void Order_ThrowsException(string parameterName)
    {
        //Arrange
        var datetime = DateTime.Now;

        //Act
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            var order = new Order(parameterName == "orderID" ? null : "number",
                datetime,
                parameterName == "customerName" ? null : "name",
                parameterName == "customerEmail" ? null : new Mock<Email>().Object,
                parameterName == "address" ? null : new Mock<Address>().Object);
        });

        //Assert
        Assert.IsTrue(exception?.Message.Contains(parameterName));
    }


    [Test]
    public void Order_AddNewItem_PerformCorrectly()
    {
        //Arrange
        var order = GetSimpleOrder();
        var product = Product.Create(ProductType.Cards, 10);

        var orderItem = new OrderItem(1, product);

        //Act
        var result = order.AddOrderItem(orderItem);

        //Assert
        result.IsSuccess.Should().BeTrue();
        order.Items.Should().HaveCount(1);
        order.Items.FirstOrDefault()!.Product.Should().Be(product);

    }


    [Test]
    public void Order_AddNewItemToExistItem_PerformCorrectly()
    {
        //Arrange
        var order = GetSimpleOrder();
        var product = Product.Create(ProductType.Cards, 10);
        var orderItemFirst = new OrderItem(1, product);

        var orderItemSecond = new OrderItem(2, product);

        //Act
        var result1 = order.AddOrderItem(orderItemFirst);
        var result2 = order.AddOrderItem(orderItemSecond);

        //Assert
        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeTrue();
        order.Items.Should().HaveCount(1);
        order.Items.FirstOrDefault()!.Product.Should().Be(product);
        order.Items.FirstOrDefault()!.Quantity.Should().Be(3);

    }


    [Test]
    public void Order_EditOrder_PerformCorrectly()
    {
        //Arrange
        var order = GetSimpleOrder();
        var newAddress = Address.Create("2222BB", 1);
        var newEmail = Email.Create("mail@email.com");

        //Act
        var result = order.EditOrder("newName", newEmail.Value, newAddress.Value);

        //Assert
        result.IsSuccess.Should().BeTrue();
        order.Address.Should().Be(newAddress.Value);
        order.CustomerEmail.Should().Be(newEmail.Value);
        order.CustomerName.Should().Be("newName");
    }

    [Test]
    public void ThrowsExceptionGivenNullItem()
    {
        var result = GetSimpleOrder().AddOrderItem(null);
        result.IsFailure.Should().BeTrue();
    }

    private Order GetSimpleOrder()
    {
        var email = Email.Create("a@b.com");
        var address = Address.Create("1111AA", 1, string.Empty);
        return new Order("id", DateTime.Now, "name", email.Value, address.Value);
    }
}

