namespace TallerChallenge.Repository.Products
{
    using TallerChallenge.Application.Products.Abstractions;
    using TallerChallenge.Domain;

    public class ProductRepository : IProductRepository
    {
        private static readonly IEnumerable<Product> _products = [
                new(1, "Product 1", "Description 1", 10),
                new(2, "Product 2", "Description 2", 20),
                new(3, "Product 3", "Description 3", 30)
            ];

        public Task<IEnumerable<Product>> GetAllByFilter(string? name, string? description, decimal? priceFrom, decimal? priceTo)
        {
            var products = _products;

            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(description))
                products = products.Where(p => p.Description.Contains(description, StringComparison.OrdinalIgnoreCase));

            if (priceFrom.HasValue)
                products = products.Where(p => p.Price >= priceFrom);

            if (priceTo.HasValue)
                products = products.Where(p => p.Price < priceTo);


            return Task.FromResult(products);
        }   
    }
}
