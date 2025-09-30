namespace TallerChallenge.Api.Products.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TallerChallenge.Api.Products.Abstractions;
    using TallerChallenge.Application.Products;

    [ApiController]
    [Route("[controller]")]
    [ApiKeyAuth]
    public class ProductController(IGetProductQueryHandler getProductQueryHandler) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<GetProductResponse>> Get([FromQuery] GetProductsRequest request)
        {
            return await getProductQueryHandler.Handle(request);
        }
    }
}
