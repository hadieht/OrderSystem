using Domain.ValueObjects;
using NUnit.Framework;

namespace OrderSystem.Domain.UnitTests.ValueObjects;


public class AddressTests
{

    [Test]
    public void Address_CreateNew_PerformCorrectly()
    {
        //Arrange
        var postCode = "1111AA";
        var houseNumber = 1;
        var addressExtra = "MN";
        //Act
        var address = Address.Create(postCode, houseNumber, addressExtra);

        //Assert

        Assert.IsTrue(address.IsSuccess);
        Assert.AreEqual(address.Value.PostalCode , postCode);
        Assert.AreEqual(address.Value.HouseNumber, houseNumber);
        Assert.AreEqual(address.Value.Extra, addressExtra);
    }

 
    [TestCase("1111AA", -1, "extra")]
    [TestCase("" ,1 , "extra")]
    public void Address_NotValidInpute_ReturnFailed(string postCode,int  houseNumber,string  addressExtra)
    {
        //Act

        var address = Address.Create(postCode, houseNumber, addressExtra);

        //Assert
        Assert.IsTrue(address.IsFailure);
        Assert.IsFalse(string.IsNullOrEmpty(address.Error));
    }
}

