namespace TallerChallenge.Application.Products
{
    public record GetProductsRequest(string? Name, string? Description, decimal? PriceFrom, decimal? PriceTo);
}
