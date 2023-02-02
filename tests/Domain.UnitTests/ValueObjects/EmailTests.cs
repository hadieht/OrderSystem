using Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(result.Value, emailABCom);
 
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
        Assert.IsTrue(result.IsFailure);
        Assert.IsFalse(string.IsNullOrEmpty(result.Error));
    }
}

