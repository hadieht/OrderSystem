using API.Requests;
using Application.Order.Commands.CancelOrder;
using Application.Order.Commands.CreateOrder;
using Application.Order.Commands.DeleteOrder;
using Application.Order.Queries.GetOrder;
using Application.Order.Queries.GetOrdersList;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Application.Order.Commands.UpdateOrder;

namespace API.Controllers;

[Route("api/[controller]")]
public class OrderController : ApiControllerBase
{
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<GetOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CreateOrderCommand request)
    {
        var result = await Mediator.Send(request);

        return Ok(result);
    }

    [ProducesResponseType(typeof(GetOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{orderID}")] // GET api/order/9333f0a39ab7488ca006b2bd8f8ff740
    public async Task<IActionResult> Get([FromRoute][NotNull] string orderID)
    {
        var request = new GetOrdersCommand
        {
            OrderID = orderID,
        };
        var result = await Mediator.Send(request);

        if (result is { IsSuccess: true, Value: null })
        {
            return NoContent();
        }

        return Ok(result.Value);
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{orderID}")]    // DELETE api/order/9333f0a39ab7488ca006b2bd8f8ff740
    public async Task<IActionResult> Delete([FromRoute][NotNull] string orderID)
    {
        var request = new DeleteOrderCommand
        {
            OrderID = orderID,
        };
        await Mediator.Send(request);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("update/{orderID}")] // PUT api/order/9333f0a39ab7488ca006b2bd8f8ff740

    public async Task<IActionResult> UpdateOrder([FromRoute][NotNull] string orderID, [FromBody] UpdateOrderRequest request)
    {
        var command = new UpdateOrderCommand
        {
            OrderID = orderID,
            CustomerName= request.CustomerName,
            Address= request.Address,
            Email = request.Email
        };

        await Mediator.Send(command);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPatch("cancel/{orderID}")]   // PUT api/order/9333f0a39ab7488ca006b2bd8f8ff740
    public async Task<IActionResult> CancelOrder([FromRoute][NotNull] string orderID)
    {
        var request = new CancelOrderCommand
        {
            OrderID = orderID,
        };
        await Mediator.Send(request);

        return Ok();
    }

}
