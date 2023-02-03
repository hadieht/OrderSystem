using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Order.Commands.CreateOrder;
using Application.Order.Queries.GetOrder;
using FluentAssertions;

using static Application.IntegrationTests.Testing;


namespace Application.IntegrationTests.Order.Query;
public class GetOrderTests : BaseTestFixture
{

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var query = new GetOrdersQuery();

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task GetOrderQuery_WithEmptyOrderID_ThrowsException()
    {
        var command = new GetOrdersQuery()
        {
            OrderID = ""
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task GetOrder_ShouldPerformCorrectly()
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
                new OrderItem {Quantity = 1 , ProductType= Domain.Enums.ProductType.Cards}
            }
        };

        var createOrderResponse = await SendAsync(command);

        var query = new GetOrdersQuery()
        {
            OrderID = createOrderResponse.OrderID
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.OrderID.Should().Be(createOrderResponse.OrderID);
        result.Value.Items.Should().HaveCount(1);

    }
}

