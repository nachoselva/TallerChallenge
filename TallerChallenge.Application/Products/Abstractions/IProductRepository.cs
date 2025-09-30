namespace TallerChallenge.Application.Products.Abstractions
{
    using System.Collections.Generic;
    using TallerChallenge.Domain;

    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllByFilter(string? name, string? description, decimal? priceFrom, decimal? priceTo);
    }
}
