using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Order.Commands.CreateOrder;
using Application.Order.Commands.UpdateOrder;
using FluentAssertions;

using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Order.Commands;
public class UpdateOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdateOrderCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test, TestCaseSource(nameof(UpdateCommands))]
    public async Task UpdateCommand_ThrowsException(UpdateOrderCommand command)
    {
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateOrder_PerformCorrectly()
    {

        var createOrderCommand = new CreateOrderCommand
        {
            Address = new Common.Models.Address
            {
                PostalCode ="1111AA",
                HouseNumber = 1
            },
            CustomerName = "name",
            Email = "email@email.com",
            Items = new List<OrderItem>
            {
                new OrderItem {Quantity = 1 , ProductType= Domain.Enums.ProductType.Cards}
            }
        };

        var createOrderResult = await SendAsync(createOrderCommand);


        var updateCommand = new UpdateOrderCommand
        {
            OrderNumber = createOrderResult.OrderNumber,
            Address = new Common.Models.Address
            {
                PostalCode = "2222BB",
                HouseNumber = 2
            },
            CustomerName= "nameEdit",
            Email= "edit@email.com"

        };

        var updateOrderResult = await SendAsync(updateCommand);


        updateOrderResult.Should().BeTrue();

        var item = await FindOrder(createOrderResult.OrderNumber);

        item.Should().NotBeNull();

        item.CustomerEmail.Value.Should().Be(updateCommand.Email);
        item.CustomerName.Should().Be(updateCommand.CustomerName);
        item.Address.HouseNumber.Should().Be(updateCommand.Address.HouseNumber);
        item.Address.PostalCode.Should().Be(updateCommand.Address.PostalCode);

        item.Items.First().Should().NotBeNull();
        item.Items.First().Product.ProductType.Should().Be(createOrderCommand.Items.First().ProductType);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }


    private static IEnumerable<TestCaseData> UpdateCommands()
    {
        var address = new Common.Models.Address
        {
            PostalCode ="1111AA",
            HouseNumber=1
        };

        var email = "e@mail.com";

        yield return new TestCaseData(new UpdateOrderCommand
        {
            OrderNumber = "abcd123",
            Address = address,
            CustomerName = "",
            Email = email
        });

        yield return new TestCaseData(new UpdateOrderCommand
        {
            OrderNumber = "abcd123",
            Address = address,
            CustomerName = "name",
            Email = string.Empty
        });

        yield return new TestCaseData(new UpdateOrderCommand
        {
            OrderNumber = "abcd123",
            CustomerName = "name",
            Email = email
        });


        yield return new TestCaseData(new UpdateOrderCommand
        {
            OrderNumber = "abcd123",
            Address = address,
            CustomerName = "name",
            Email = "worngemail"
        });

        yield return new TestCaseData(new UpdateOrderCommand
        {
            OrderNumber = "",
            Address = address,
            CustomerName = "name",
            Email = email
        });

        yield return new TestCaseData(new UpdateOrderCommand
        {
            Address = address,
            CustomerName = "name",
            Email = email
        });
    }
}

