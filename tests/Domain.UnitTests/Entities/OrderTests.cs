using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace OrderSystem.Domain.UnitTests.Entities
{
    public class OrderTests
    { 

        [Test]
        [TestCase("orderNumber")]
        [TestCase("orderDate")]
        [TestCase("customerName")]
        [TestCase("customerEmail")]
        [TestCase("address")]
        public void Order_ThrowsException(string parameterName)
        {
            var datetime = DateTime.Now;
            var exception = Assert.Throws<NotSupportedException>(() =>
                            new Order(parameterName == "orderNumber" ? null : "number",
                                               datetime,
                                               parameterName == "customerName" ? null : "name",
                                               parameterName == "customerEmail" ? null : new Mock<Email>().Object,
                                               parameterName == "address" ? null : new Mock<Address>().Object));

            Assert.AreEqual(parameterName, exception?.Message);
        }
    }
}
