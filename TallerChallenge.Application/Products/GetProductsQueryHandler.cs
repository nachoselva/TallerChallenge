namespace TallerChallenge.Application.Products
{
    using TallerChallenge.Api.Products.Abstractions;
    using TallerChallenge.Application.Products.Abstractions;

    public class GetProductsQueryHandler(IProductRepository productRepository) : IGetProductQueryHandler
    {
        public async Task<IEnumerable<GetProductResponse>> Handle(GetProductsRequest request)
        {
            var products = await productRepository.GetAllByFilter(request.Name, request.Description, request.PriceFrom, request.PriceTo);
            return products.ToResponse();
        }
    }
}
