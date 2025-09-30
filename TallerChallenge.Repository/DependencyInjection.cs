namespace TallerChallenge.Repository
{
    using Microsoft.Extensions.DependencyInjection;
    using TallerChallenge.Application.Products.Abstractions;
    using TallerChallenge.Repository.Products;

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
