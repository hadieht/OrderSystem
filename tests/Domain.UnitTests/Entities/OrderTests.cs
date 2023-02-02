using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
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
        var email = Email.Create("a@b.com");
        var address = Address.Create("1111AA", 1, string.Empty);
        var order = new Order("id", DateTime.Now, "name", email.Value, address.Value);
        var product = Product.Create(ProductType.Cards, 10);

        var orderItem = new OrderItem(1, product);

        //Act
        var result = order.AddOrderItem(orderItem);

        //Assert

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(order.Items.Count()== 1);
        Assert.AreEqual(order.Items.FirstOrDefault()!.Product, product);

    }


    [Test]
    public void Order_AddNewItemToExistItem_PerformCorrectly()
    {
        //Arrange
        var email = Email.Create("a@b.com");
        var address = Address.Create("1111AA", 1, string.Empty);
        var order = new Order("id", DateTime.Now, "name", email.Value, address.Value);

        var product = Product.Create(ProductType.Cards, 10);

        var orderItemFirst = new OrderItem(1, product);


        var orderItemSecond = new OrderItem(2, product);

        //Act
        var result1 = order.AddOrderItem(orderItemFirst);
        var result2 = order.AddOrderItem(orderItemSecond);
        //Assert

        Assert.IsTrue(result1.IsSuccess);
        Assert.IsTrue(result2.IsSuccess);
        Assert.IsTrue(order.Items.Count()== 1);
        Assert.AreEqual(order.Items.FirstOrDefault()!.Product, product);
        Assert.AreEqual(order.Items.FirstOrDefault()!.Quantity, 3);

    }


    [Test]
    public void Order_EditOrder_PerformCorrectly()
    {
        //Arrange
        var email = Email.Create("a@b.com");
        var address = Address.Create("1111AA", 1, "A");
        var order = new Order("id", DateTime.Now, "name", email.Value, address.Value);


        var newAddress = Address.Create("2222BB", 1);
        var newEmail = Email.Create("mail@email.com");


        //Act

        var result = order.EditOrder("newName", newEmail.Value, newAddress.Value);

        //Assert

        Assert.IsTrue(result.IsSuccess);

        Assert.AreEqual(order.Address, newAddress.Value);
        Assert.AreEqual(order.CustomerEmail, newEmail.Value);
        Assert.AreEqual(order.CustomerName, "newName");

    }
}

