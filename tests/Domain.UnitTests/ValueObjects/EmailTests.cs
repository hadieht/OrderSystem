using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace OrderSystem.Domain.UnitTests.ValueObjects;

public class EmailTests
{
    [Test]
    public void Address_CreateNew_PerformCorrectly()
    {
        //Arrange
        var emailABCom = "a@b.com";

        //Act
        var result = Email.Create(emailABCom);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(emailABCom);

    }

    [TestCase("aaa@b")]
    [TestCase("a#b")]
    [TestCase("test")]
    [TestCase("")]
    public void Address_NotValidInpute_ReturnFailed(string email)
    {
        //Act
        var result = Email.Create(email);

        //Assert
        result.IsFailure.Should().BeTrue();
        string.IsNullOrEmpty(result.Error).Should().BeFalse();
    }
}

