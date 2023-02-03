using Application.Product.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[Route("api/[controller]")]
public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<List<GetProductsResponse>> GetAll()
    {
        var result = await Mediator.Send(new GetAllProductsQuery());
        return result.Value;
    }
}
