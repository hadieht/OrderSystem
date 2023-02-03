using Domain.ValueObjects;
using FluentAssertions;
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
        address.IsSuccess.Should().BeTrue();
        address.Value.PostalCode.Should().Be(postCode);
        address.Value.HouseNumber.Should().Be(houseNumber);
        address.Value.Extra.Should().Be(addressExtra);
    }


    [TestCase("1111AA", -1, "extra")]
    [TestCase("", 1, "extra")]
    public void Address_NotValidInpute_ReturnFailed(string postCode, int houseNumber, string addressExtra)
    {
        //Act

        var address = Address.Create(postCode, houseNumber, addressExtra);

        //Assert
        address.IsFailure.Should().BeTrue();
        string.IsNullOrEmpty(address.Error).Should().BeFalse();
    }
}

