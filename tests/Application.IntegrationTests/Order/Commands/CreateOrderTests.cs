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
        await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }



    [Test]
    public async Task ShouldCreateTodoItem()
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

        var item = await FindOrder(createOrderResult.OrderNumber);

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

}