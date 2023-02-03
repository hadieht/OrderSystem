using Application.Common.Models;
using Application.Order.Commands.CreateOrder;
using Application.Order.Queries.GetOrdersList;
using FluentAssertions;

using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Order.Query;

public class GetOrderListTests : BaseTestFixture
{
    [Test]
    public async Task GetOrderList_ShouldNotNull()
    {
        var query = new GetOrdersListQuery();

        var result = await SendAsync(query);

        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
    }

    [Test]
    public async Task GetOrderList_ShouldReturnExactValue()
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

        await SendAsync(command);

        var query = new GetOrdersListQuery();

        var result = await SendAsync(query);

        result.Value.Should().HaveCountGreaterOrEqualTo(1);

    }
}

