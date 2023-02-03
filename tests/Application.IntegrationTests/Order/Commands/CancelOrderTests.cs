using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Order.Commands.CancelOrder;
using Application.Order.Commands.CreateOrder;
using Domain.Enums;
using FluentAssertions;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Order.Commands;

public class CancelOrderTests : BaseTestFixture
{

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CancelOrderCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task CancelEmptyCommand_ThrowsException()
    {
        var command = new CancelOrderCommand()
        {
            OrderID = ""
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CancelOrder_PerformCorrectly()
    {

        var createCommand = new CreateOrderCommand
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
                new OrderItem {Quantity = 1 , ProductType= Domain.Enums.ProductType.Cards}
            }
        };

        var createOrderResult = await SendAsync(createCommand);

        var cancelCommand = new CancelOrderCommand()
        {
            OrderID = createOrderResult.OrderID
        };

        var cancelOrderResult = await SendAsync(cancelCommand);


        var item = await FindOrder(createOrderResult.OrderID);

        item.Should().NotBeNull();

        item.Status.Should().Be(OrderStatus.Canceled);
    }
}

