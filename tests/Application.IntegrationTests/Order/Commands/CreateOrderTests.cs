using Application.Common.Exceptions;
using Application.Order.Commands.CreateOrder;
using FluentAssertions;

namespace Application.IntegrationTests.Order.Commands;


public class CreateOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateOrderCommand();
        await FluentActions.Invoking(() => Testing. SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

   
}