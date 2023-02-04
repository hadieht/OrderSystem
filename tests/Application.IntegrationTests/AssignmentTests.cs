using Application.Common.Models;
using Application.Order.Commands.CreateOrder;
using FluentAssertions;

using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests;

public class AssignmentTests : BaseTestFixture
{
    [Test] // 1 photo book (0), 2 calendars (|) and 1 mug (.)  Expected: 133 =  19+ 2*10 + 94
    public async Task CreateOrder__WithTwoMug_ReturnWidthAsExpected()
    {

        var command = new CreateOrderCommand
        {
            Address = new Common.Models.Address
            {
                PostalCode ="1111AA",
                HouseNumber=1
            },
            CustomerName= "name",
            Email= "email@email.com",
            Items = new List<OrderItem>
            {
                new() {Quantity = 1 , ProductType= Domain.Enums.ProductType.PhotoBook},
                new() {Quantity = 2 , ProductType= Domain.Enums.ProductType.Calendar},
                new() {Quantity = 1 , ProductType= Domain.Enums.ProductType.Mug},
            }
        };

        var createOrderResult = await SendAsync(command);

        createOrderResult.Should().NotBeNull();
        createOrderResult.RequiredBinWidth.Should().Be("133mm");
    }


    [Test] // 1 photo book (0), 2 calendars (|) and 5 mug (.)  Expected: 227 =  19+ 2*10 + 94 + 94
    public async Task CreateOrder__WithFiveMug_ReturnWidthAsExpected()
    {

        var command = new CreateOrderCommand
        {
            Address = new Common.Models.Address
            {
                PostalCode ="1111AA",
                HouseNumber=1
            },
            CustomerName= "name",
            Email= "email@email.com",
            Items = new List<OrderItem>
            {
                new() {Quantity = 1 , ProductType= Domain.Enums.ProductType.PhotoBook},
                new() {Quantity = 2 , ProductType= Domain.Enums.ProductType.Calendar},
                new() {Quantity = 5 , ProductType= Domain.Enums.ProductType.Mug},
            }
        };

        var createOrderResult = await SendAsync(command);

        createOrderResult.Should().NotBeNull();
        createOrderResult.RequiredBinWidth.Should().Be("227mm");
    }
}

