using API.Requests;
using Application.Order.Commands.CancelOrder;
using Application.Order.Commands.CreateOrder;
using Application.Order.Commands.DeleteOrder;
using Application.Order.Queries.GetOrder;
using Application.Order.Queries.GetOrdersList;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace API.Controllers;

[Route("api/[controller]")]
public class OrderController : ApiControllerBase
{
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<GetOrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await Mediator.Send(new GetOrdersListCommand());

        if (result.IsSuccess && (result.Value == null || !result.Value.Any()))
        {
            return NoContent();
        }

        return Ok(result.Value);
    }


    [HttpPost()]
    [ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CreateOrderCommand request)
    {
        var result = await Mediator.Send(request);

        return Ok(result);
    }

    [ProducesResponseType(typeof(GetOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{orderNumber}")] // GET api/order/9333f0a39ab7488ca006b2bd8f8ff740
    public async Task<IActionResult> Get([FromRoute][NotNull] string orderNumber)
    {
        var request = new GetOrdersCommand
        {
            OrderNumber = orderNumber,
        };
        var result = await Mediator.Send(request);

        if (result.IsSuccess && result.Value == null)
        {
            return NoContent();
        }

        return Ok(result.Value);
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{orderNumber}")]    // DELETE api/order/9333f0a39ab7488ca006b2bd8f8ff740
    public async Task<IActionResult> Delete([FromRoute][NotNull] string orderNumber)
    {
        var request = new DeleteOrderCommand
        {
            OrderNumber = orderNumber,
        };
        await Mediator.Send(request);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update/{orderNumber}")] // PUT api/order/9333f0a39ab7488ca006b2bd8f8ff740

    public async Task<IActionResult> UpdateOrder([FromRoute][NotNull] string orderNumber, [FromBody] UpdateOrderRequest request)
    {
        var command = new UpdateOrderCommand
        {
            OrderNumber = orderNumber,
            CustomerName= request.CustomerName,
            Address= request.Address,
            Email = request.Email
        };

        await Mediator.Send(command);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("cancel/{orderNumber}")]   // PUT api/order/9333f0a39ab7488ca006b2bd8f8ff740
    public async Task<IActionResult> CancelOrder([FromRoute][NotNull] string orderNumber)
    {
        var request = new CancelOrderCommand
        {
            OrderNumber = orderNumber,
        };
        await Mediator.Send(request);

        return Ok();
    }

}
