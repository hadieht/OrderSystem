using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Order.Commands.CreateOrder;
using FluentAssertions;

using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Order.Commands;


public class CreateOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateOrderCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test, TestCaseSource(nameof(CreateCommands))]
    public async Task CreateCommand_ThrowsException(CreateOrderCommand command)
    {
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateOrder_PerformCorrectly()
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

        var createOrderResult = await SendAsync(command);

        var item = await FindOrder(createOrderResult.OrderID);

        item.Should().NotBeNull();

        item.CustomerEmail.Value.Should().Be(command.Email);
        item.CustomerName.Should().Be(command.CustomerName);
        item.Address.HouseNumber.Should().Be(command.Address.HouseNumber);
        item.Address.PostalCode.Should().Be(command.Address.PostalCode);

        item.Items.First().Should().NotBeNull();
        item.Items.First().Product.ProductType.Should().Be(command.Items.First().ProductType);
        item.Items.First().Quantity.Should().Be(command.Items.First().Quantity);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }


    private static IEnumerable<TestCaseData> CreateCommands()
    {
        var address = new Common.Models.Address
        {
            PostalCode ="1111AA",
            HouseNumber=1
        };

        var email = "e@mail.com";

        var orderItems = new List<OrderItem>
            {
                new OrderItem {Quantity = 1 , ProductType= Domain.Enums.ProductType.Cards}
            };

        var wrongItem = new List<OrderItem>
            {
                new OrderItem {Quantity = -1 , ProductType= Domain.Enums.ProductType.Cards}
            };

        yield return new TestCaseData(new CreateOrderCommand
        {
            Address = address,
            CustomerName = "",
            Email = email,
            Items= orderItems
        });

        yield return new TestCaseData(new CreateOrderCommand
        {
            Address = address,
            CustomerName = "name",
            Email = string.Empty,
            Items= orderItems
        });

        yield return new TestCaseData(new CreateOrderCommand
        {
            CustomerName = "name",
            Email = email,
            Items= orderItems
        });


        yield return new TestCaseData(new CreateOrderCommand
        {
            Address = address,
            CustomerName = "name",
            Email = "worngemail",
            Items= orderItems
        });

        yield return new TestCaseData(new CreateOrderCommand
        {
            Address = address,
            CustomerName = "name",
            Email = email,
            Items= wrongItem
        });
    }


}