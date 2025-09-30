namespace TallerChallenge.Application.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using TallerChallenge.Domain;

    internal static class GetProductMapper
    {
        internal static GetProductResponse ToResponse(this Product product)
        {
            return new GetProductResponse(product.Id, product.Name, product.Description, product.Price);
        }

        internal static IEnumerable<GetProductResponse> ToResponse(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToResponse());
        }
    }
}
