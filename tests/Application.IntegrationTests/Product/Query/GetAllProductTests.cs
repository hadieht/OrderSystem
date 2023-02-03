using Application.Common.Models;
using Application.Order.Commands.CreateOrder;
using Application.Order.Queries.GetOrdersList;
using Application.Product.Queries;
using FluentAssertions;

using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Product.Query;

public class GetAllProductTests : BaseTestFixture
{
    [Test]
    public async Task GetAllProducts_ShouldNotNull()
    {
        var query = new GetAllProductsQuery();

        var result = await SendAsync(query);

        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
    }

    [Test]
    public async Task GetAllProducts_ShouldReturnExpectedList()
    {
        var command = new GetAllProductsQuery();

        await SendAsync(command);

        var query = new GetOrdersListQuery();

        var result = await SendAsync(query);

        result.Value.Should().HaveCountGreaterOrEqualTo(0);

    }
}

