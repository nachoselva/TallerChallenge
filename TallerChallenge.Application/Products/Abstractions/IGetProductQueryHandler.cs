namespace TallerChallenge.Api.Products.Abstractions
{
    using TallerChallenge.Application.Products;

    public interface IGetProductQueryHandler
    {
        Task<IEnumerable<GetProductResponse>> Handle(GetProductsRequest request);
    }
}
